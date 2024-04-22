using MongoDB.Bson;
using Sample.Domain.Commands.Customer.Validations;

namespace Sample.Domain.Commands.Customer
{
    public class UpdateCustomerCommand : CustomerCommand
    {
        public UpdateCustomerCommand(ObjectId id, string name, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateCustomerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
