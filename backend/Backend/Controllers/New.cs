using Backend.Enums;
using Backend.Helpers;
using Backend.Helpers.Blacklist;
using Backend.Modals;
using Microsoft.AspNetCore.Mvc;
using shortid;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class New : ControllerBase
    {
        private readonly MongoCrud Mongo;
        private readonly ILogger<New> Logger;

        public New(ILogger<New> logger)
        {
            Mongo = new();
            Logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewLinkReqBody body)
        {
            if (body == null)
                return BadRequest(
                    HttpUtils.GetErr("Invalid request body!")
                    );

            string? link = body.Link;

            if (Strings.IsValidURL(link))
            {
                bool isDominBlacklisted = DomainBlacklist.IsBlacklisted(link);

                if (isDominBlacklisted)
                    return BadRequest(
                        HttpUtils.GetErr(
                            "You are not allowed to shorten URLs from the domain provided!"
                            )
                        );
            }
            else return BadRequest(HttpUtils.GetErr("Invalid link!"));

            string? slug = body.Slug;
            var exp = body.Expiration;

            if (string.IsNullOrEmpty(slug))
            {
                if (exp != null && exp == ExpType.Never)
                {
                    var res = await Mongo.GetManyByFilter<LinkInfoDbModal>(
                        "link",
                        link
                        );

                    if (res != null && res.Count > 0)
                        foreach (var i in res)
                        {
                            if (i.Expiration != ExpType.Never)
                                continue;

                            i.TrackingId = null;
                            i.Clicks = null;
                            i.PreviouslyCreated = true;

                            return Ok(i);
                        }
                }

                do { slug = ShortId.Generate(new(true, false, 8)).ToLower(); }
                while (await ShrugExists(slug));
            }
            else
            {
                try { slug = Strings.Slugify(slug); }
                catch { return StatusCode(500, HttpUtils.GetErr("Some unknown error occurred!")); }

                if (slug.Length < 3 || slug.Length > 32)
                    return BadRequest(HttpUtils.GetErr("Invalid shrug!"));
                else if (SlugBlacklist.IsBlacklisted(slug))
                    return BadRequest(
                        HttpUtils.GetErr(
                            "You are not allowed to use this slug!" +
                            " Please contact the MHS Coding Club's officers if you have any questions."
                            )
                        );
                else if (await ShrugExists(slug))
                    return BadRequest(HttpUtils.GetErr("Slug already exists!"));
            }

            bool isValidCaptcha = await ReCaptcha.Validate(
                body.CaptchaResponse
                );

            if (!isValidCaptcha)
                return BadRequest(HttpUtils.GetErr("Invalid captcha!"));

            var linkInfo = new LinkInfoDbModal
            {
                Link = link,
                Slug = slug,
                Expiration = exp,
                Creator = GetCreatorInfo(Request.Headers)
            };

            if (exp != null && exp != ExpType.Never)
                linkInfo.ExpirationRaw = ConvertExp(exp);

            try
            {
                await Mongo.InsertOne(linkInfo);
                Logger.LogInformation($"Shortened link with slug: {slug}", linkInfo);

                return Ok(linkInfo);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return StatusCode(500, HttpUtils.Get500());
            }
        }

        private static CreatorInfo GetCreatorInfo(IHeaderDictionary headers) => new()
        {
            IpAddress = HttpUtils.GetIpAddr(headers),
            UserAgent = headers.UserAgent
        };

        private static DateTime? ConvertExp(ExpType? type)
        {
            var res = DateTime.UtcNow;

            switch (type)
            {
                case ExpType.OneDay:
                    res.AddDays(1);
                    break;

                case ExpType.OneWeek:
                    res.AddDays(7);
                    break;

                case ExpType.OneMonth:
                    res.AddMonths(1);
                    break;

                case ExpType.ThreeMonths:
                    res.AddMonths(3);
                    break;

                case ExpType.OneYear:
                    res.AddYears(1);
                    break;
            }

            return res;
        }

        private async Task<bool> ShrugExists(string shrug)
        {
            var res = await Mongo.GetManyByFilter<LinkInfoDbModal>(
                "slug",
                shrug
                );

            return !(res == null || res.Count == 0);
        }
    }
}