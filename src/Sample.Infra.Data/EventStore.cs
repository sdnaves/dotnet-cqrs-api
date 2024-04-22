using Newtonsoft.Json;
using Sample.Domain.Core.Models;
using Sample.Domain.Interfaces;
using Sample.Infra.CrossCutting.Mediator.Interfaces;
using Sample.Infra.CrossCutting.Mediator.Models;

namespace Sample.Infra.Data
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public EventStore(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public void Save<T>(T @event) where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(@event);

            var storedEvent = new StoredEvent(@event, serializedData, "default");

            _eventStoreRepository.Create(storedEvent);
        }
    }
}
