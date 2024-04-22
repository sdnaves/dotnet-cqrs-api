using Sample.Domain.Commands.Customer.Validations;

namespace Sample.Domain.Commands.Customer
{
    public class CreateCustomerCommand : CustomerCommand
    {
        public CreateCustomerCommand(string name, string email, DateTime birthDate)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateCustomerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
