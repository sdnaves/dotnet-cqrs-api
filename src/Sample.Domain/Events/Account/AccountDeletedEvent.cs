using MongoDB.Bson;
using Sample.Infra.CrossCutting.Mediator.Models;

namespace Sample.Domain.Events.Account
{
    public class AccountDeletedEvent : Event
    {
        public AccountDeletedEvent(ObjectId id)
        {
            Id = id;
            AggregateId = id.ToString();
        }

        public ObjectId Id { get; set; }
    }
}
