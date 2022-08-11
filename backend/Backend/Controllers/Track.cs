using Backend.Modals;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Track : ControllerBase
    {
        private readonly MongoCrud Mongo = new();

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var res = await Mongo.GetManyByFilter<LinkInfoDbModal>(
                "tracking_id",
                id
                );

            if (res == null || res.Count == 0)
                return NotFound();

            var item = res.First();

            var trackRes = new TrackRes
            {
                LinkInfo = item,
                Clicks = 0
            };

            var clicks = item.Clicks;

            if (clicks == null || clicks.Count == 0)
                return Ok(trackRes);

            trackRes.Clicks = clicks.Count;

            var byDevice = trackRes.ByDevice ?? new();
            var byCountry = trackRes.ByCountry ?? new();
            var byDate = trackRes.ByDate ?? new();

            foreach (var i in clicks)
            {
                CountAll(i.Device, ref byDevice);
                CountAll(i.Country, ref byCountry);
                CountAll(i.AccessedAt.ToString("d"), ref byDate);
            }

            trackRes.ByDevice = byDevice;
            trackRes.ByCountry = byCountry;
            trackRes.ByDate = byDate;

            return Ok(trackRes);
        }

        private static void CountAll(
            string? cap,
            ref Dictionary<string, int> info
            )
        {
            if (string.IsNullOrEmpty(cap))
                return;
            else if (info.ContainsKey(cap))
                info[cap]++;
            else info[cap] = 1;
        }
    }
}