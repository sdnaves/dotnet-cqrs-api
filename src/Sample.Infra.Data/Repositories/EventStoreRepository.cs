using MongoDB.Driver;
using Sample.Domain.Core.Models;
using Sample.Domain.Interfaces;
using Sample.Infra.Data.Contexts;

namespace Sample.Infra.Data.Repositories
{
    public sealed class EventStoreRepository : IEventStoreRepository
    {
        private readonly EventStoreContext Db;
        private readonly IMongoCollection<StoredEvent> DbSet;

        public EventStoreRepository(EventStoreContext db)
        {
            Db = db ?? throw new ArgumentNullException(nameof(db));
            DbSet = db.GetCollection<StoredEvent>();
        }

        public async Task<List<StoredEvent>> GetAllAsync(string aggregateId)
        {
            return await DbSet.Find(f => f.AggregateId == aggregateId).ToListAsync();
        }

        public async Task Create(StoredEvent @event)
        {
            await DbSet.InsertOneAsync(@event);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
