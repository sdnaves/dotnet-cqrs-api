using MongoDB.Bson;
using Sample.Infra.CrossCutting.Mediator.Models;

namespace Sample.Domain.Events.Account
{
    public class AccountCreatedEvent : Event
    {
        public AccountCreatedEvent(ObjectId id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            AggregateId = id.ToString();
        }

        public ObjectId Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
    }
}
