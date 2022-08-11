using Backend.Helpers;
using Backend.Modals;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Redirect : ControllerBase
    {
        private readonly MongoCrud Mongo = new();

        [HttpGet]
        public async Task<RedirectResult> Get(string slug)
        {
            RedirectResult NotFound() =>
                Redirect("https://mhs.sh/404");

            int len = slug.Length;

            if (len < 3 || len > 32)
                return NotFound();

            var res = await Mongo.GetManyByFilter<LinkInfoDbModal>(
                "slug",
                slug
                );

            if (res == null || res.Count == 0)
                return NotFound();

            var item = res.First();
            string? link = item.Link;

            if (string.IsNullOrEmpty(link))
                return NotFound();

            var headers = Request.Headers;

            var reportInfo = new AccessReportInfo
            {
                Id = item.Id,
                UserAgent = headers.UserAgent,
                IpAddress = HttpUtils.GetIpAddr(headers)
            };

            AccessReporter.Report(reportInfo);
            return Redirect(link);
        }
    }
}