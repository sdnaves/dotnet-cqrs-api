using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Sample.Infra.CrossCutting.Mediator.Models;

namespace Sample.Domain.Core.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = ObjectId.GenerateNewId();

            _domainEvents = [];
        }

        [BsonId]
        [JsonProperty("_id")]
        public ObjectId Id { get; set; }

        private readonly List<Event> _domainEvents;
        public IReadOnlyCollection<Event> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(Event domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(Event domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
