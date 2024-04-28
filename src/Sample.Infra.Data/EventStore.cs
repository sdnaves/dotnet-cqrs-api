using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Sample.Domain.Interfaces;
using Sample.Domain.Models;
using Sample.Infra.CrossCutting.Mediator.Interfaces;
using Sample.Infra.CrossCutting.Mediator.Models;

namespace Sample.Infra.Data
{
    public class EventStore : IEventStore
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IEventStoreRepository _eventStoreRepository;

        public EventStore(IHttpContextAccessor httpContextAccessor, IEventStoreRepository eventStoreRepository)
        {
            _httpContextAccessor = httpContextAccessor;

            _eventStoreRepository = eventStoreRepository;
        }

        public void Save<T>(T @event) where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(@event);

            var user = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "no user informed";

            var storedEvent = new StoredEvent(@event, serializedData, user);

            _eventStoreRepository.Create(storedEvent);
        }
    }
}
