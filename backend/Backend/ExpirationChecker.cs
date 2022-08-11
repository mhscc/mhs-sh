using Backend.Modals;
using MongoDB.Bson;

namespace Backend
{
    public class ExpirationChecker
    {
        private static readonly MongoCrud Mongo = new();

        public static void Checker()
        {
            while (true)
            {
                Thread.Sleep(
                    TimeSpan.FromHours(1)
                    );

                try { CheckAllAsync().Wait(); }
                catch { }
            }
        }

        private static async Task CheckAllAsync()
        {
            var allInfos = await Mongo.GetAll<LinkInfoDbModal>();

            if (allInfos == null || allInfos.Count == 0)
                return;

            var filtered = allInfos.Where(
                i => i != null &&
                i.Expiration != Enums.ExpType.Never &&
                DateTime.UtcNow >= i.ExpirationRaw
                );

            if (!filtered.Any())
                return;

            foreach (var i in filtered)
            {
                var objId = ObjectId.Parse(
                    i.Id
                    );

                try { await Mongo.DeleteOne<LinkInfoDbModal>(objId); }
                catch { }
            }
        }
    }
}