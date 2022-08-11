using MongoDB.Bson;
using MongoDB.Driver;

namespace Backend
{
    public class MongoCrud
    {
        private IMongoDatabase Database { get; set; }

        public const string CollectionName = "shortened_urls";

        public MongoCrud()
        {
            var client = new MongoClient();
            Database = client.GetDatabase("mhs_short");
        }

        public async Task InsertOne<T>(T document) where T : class =>
            await GetCollection<T>().InsertOneAsync(document);

        public async Task<List<T>> GetAll<T>()
        {
            var res = await GetCollection<T>().FindAsync(
                new BsonDocument()
                );

            var list = await res.ToListAsync();
            return list;
        }

        public async Task<T?> GetOneById<T>(ObjectId? id)
        {
            var filter = Builders<T>.Filter.Eq(
                "_id",
                id
                );

            var res = await GetCollection<T>().FindAsync(filter);
            return res.First();
        }

        public async Task<List<T>> GetManyByFilter<T>(
            string? key,
            string? value
            )
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            else if (value == null)
                throw new ArgumentNullException(nameof(value));

            var collection = GetCollection<T>();
            var filter = Builders<T>.Filter.Eq(key, value);

            var res = await collection.FindAsync(filter);
            var resList = await res.ToListAsync();

            return resList;
        }

        public async Task Upsert<T>(ObjectId id, T document)
        {
            var collection = GetCollection<T>();

            var options = new ReplaceOptions
            {
                IsUpsert = true
            };

            await collection.ReplaceOneAsync(
                new BsonDocument("_id", id),
                document,
                options
                );
        }

        public async Task DeleteOne<T>(ObjectId id)
        {
            var collection = GetCollection<T>();

            await collection.DeleteOneAsync(
                new BsonDocument("_id", id)
                );
        }

        private IMongoCollection<T> GetCollection<T>() =>
            Database.GetCollection<T>(CollectionName);
    }
}