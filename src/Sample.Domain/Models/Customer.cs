using MongoDB.Bson;
using Sample.Domain.Core.Models;
using Sample.Infra.CrossCutting.Attributes;

namespace Sample.Domain.Models
{
    [BsonCollection("customers")]
    public class Customer : Entity
    {
        public Customer(ObjectId id, string name, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate.Date;
        }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }
    }
}
