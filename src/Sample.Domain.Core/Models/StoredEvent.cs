using MongoDB.Bson;
using Sample.Infra.CrossCutting.Attributes;
using Sample.Infra.CrossCutting.Mediator.Models;

namespace Sample.Domain.Core.Models
{
    [BsonCollection("events")]
    public class StoredEvent : Event
    {
        public ObjectId Id { get; private set; }

        public string Data { get; private set; }

        public string User { get; private set; }

        public StoredEvent(Event @event, string data, string user)
        {
            Id = ObjectId.GenerateNewId();
            AggregateId = @event.AggregateId;
            MessageType = @event.MessageType;
            Data = data;
            User = user;
        }
    }
}
