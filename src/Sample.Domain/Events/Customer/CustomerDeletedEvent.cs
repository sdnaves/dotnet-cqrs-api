using MongoDB.Bson;
using Sample.Infra.CrossCutting.Mediator.Models;

namespace Sample.Domain.Events.Customer
{
    public class CustomerDeletedEvent : Event
    {
        public CustomerDeletedEvent(ObjectId id)
        {
            Id = id;
            AggregateId = id.ToString();
        }

        public ObjectId Id { get; set; }
    }
}
