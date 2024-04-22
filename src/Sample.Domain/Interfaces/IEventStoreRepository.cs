using Sample.Domain.Core.Models;

namespace Sample.Domain.Interfaces
{
    public interface IEventStoreRepository
    {
        Task Create(StoredEvent @event);
        Task<List<StoredEvent>> GetAllAsync(string aggregateId);
    }
}
