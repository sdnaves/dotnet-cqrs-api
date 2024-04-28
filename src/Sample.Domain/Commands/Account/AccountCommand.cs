using MongoDB.Bson;
using Sample.Infra.CrossCutting.Mediator.Models;

namespace Sample.Domain.Commands.Account
{
    public abstract class AccountCommand : Command
    {
        public ObjectId Id { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
    }
}
