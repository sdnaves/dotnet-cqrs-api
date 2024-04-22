using MongoDB.Bson;
using Sample.Domain.Commands.Customer.Validations;

namespace Sample.Domain.Commands.Customer
{
    public class DeleteCustomerCommand : CustomerCommand
    {
        public DeleteCustomerCommand(ObjectId id)
        {
            Id = id;
            AggregateId = id.ToString();
        }

        public override bool IsValid()
        {
            ValidationResult = new DeleteCustomerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
