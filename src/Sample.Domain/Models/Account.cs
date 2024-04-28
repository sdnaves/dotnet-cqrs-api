using MongoDB.Bson;
using Sample.Domain.Core.Models;
using Sample.Infra.CrossCutting.Attributes;

namespace Sample.Domain.Models
{
    [BsonCollection("accounts")]
    public class Account : Entity
    {
        public Account(ObjectId id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
