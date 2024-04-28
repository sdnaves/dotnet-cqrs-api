using MongoDB.Bson;
using Sample.Domain.Commands.Account.Validations;

namespace Sample.Domain.Commands.Account
{
    public class UpdateAccountCommand : AccountCommand
    {
        public UpdateAccountCommand(ObjectId id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateAccountCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
