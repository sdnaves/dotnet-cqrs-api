using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Sample.Infra.CrossCutting.Attributes;
using System.Reflection;

namespace Sample.Infra.Data.Contexts
{
    public class EventStoreContext : IDisposable
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;

        public EventStoreContext(IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);

            _client = new MongoClient(configuration.GetConnectionString("Default"));

            if (_client == null) throw new Exception($"{nameof(_client)} must not be null");

            // set the databaseName
            _database = _client.GetDatabase("sample-db");
        }

        public IMongoCollection<TDocument> GetCollection<TDocument>()
        {
            var attribute = typeof(TDocument).GetCustomAttribute<BsonCollectionAttribute>(true);

            return attribute switch
            {
                null => throw new Exception($"The {nameof(BsonCollectionAttribute)} is required"),
                _ => _database.GetCollection<TDocument>(attribute.CollectionName),
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
