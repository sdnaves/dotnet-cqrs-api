using MongoDB.Bson;
using Sample.Domain.Commands.Account.Validations;

namespace Sample.Domain.Commands.Account
{
    public class DeleteAccountCommand : AccountCommand
    {
        public DeleteAccountCommand(ObjectId id)
        {
            Id = id;
            AggregateId = id.ToString();
        }

        public override bool IsValid()
        {
            ValidationResult = new DeleteAccountCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
