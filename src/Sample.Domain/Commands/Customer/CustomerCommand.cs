using MongoDB.Bson;
using Sample.Infra.CrossCutting.Mediator.Models;

namespace Sample.Domain.Commands.Customer
{
    public abstract class CustomerCommand : Command
    {
        private string _name = default!;
        private string _email = default!;

        public ObjectId Id { get; protected set; }

        public string Name { get => _name; protected set => _name = value; }

        public string Email { get => _email; protected set => _email = value; }

        public DateTime BirthDate { get; protected set; }
    }
}
