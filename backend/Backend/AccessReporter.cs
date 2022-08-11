using Backend.Helpers;
using Backend.Modals;
using MongoDB.Bson;
using System.Collections.Concurrent;

namespace Backend
{
    public class AccessReporter
    {
        private static readonly MongoCrud Mongo = new();
        private static readonly HttpClient Client = new();
        private static readonly ConcurrentQueue<AccessReportInfo> ReportQueue = new();

        public static void Report(AccessReportInfo info) =>
            ReportQueue.Enqueue(info);

        public static void Worker()
        {
            while (true)
            {
                Thread.Sleep(1000);

                if (ReportQueue.IsEmpty)
                    continue;

                var list = new List<AccessReportInfo>();

                while (!ReportQueue.IsEmpty)
                {
                    if (
                        !ReportQueue.TryDequeue(out var info) ||
                        info == null
                        ) continue;

                    list.Add(info);
                }

                foreach (var i in list)
                {
                    try { Process(i).Wait(); }
                    catch { Report(i); }
                }
            }
        }

        private static async Task Process(AccessReportInfo info)
        {
            var objId = ObjectId.Parse(info.Id);

            var linkInfo = await Mongo.GetOneById<LinkInfoDbModal>(
                objId
                );

            if (linkInfo == null)
                return;

            string? uaStr = info.UserAgent;

            if (string.IsNullOrEmpty(uaStr))
                return;

            var uaInfo = Program.UAService?.Parse(uaStr);

            if (
                uaInfo == null ||
                uaInfo.IsRobot ||
                !uaInfo.IsBrowser
                ) return;

            var accessInfo = new UserAccessInfo
            {
                Device = $"{uaInfo.Browser}; {uaInfo.Platform}",
                Country = "MP",
                AccessedAt = info.AccessedAt
            };

            var res = await IpApi.GetIpInfo(Client, info.IpAddress);

            if (res != null)
            {
                string? cc = res.CountryCode;

                if (!string.IsNullOrEmpty(cc))
                    accessInfo.Country = cc;
            }

            var clicks = linkInfo.Clicks ?? new();

            clicks.Add(accessInfo);
            linkInfo.Clicks = clicks;

            await Mongo.Upsert(objId, linkInfo);
        }
    }
}