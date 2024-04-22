namespace Sample.Domain.Commands.Customer.Validations
{
    public class CreateCustomerCommandValidation : CustomerValidation<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidation()
        {
            ValidateName();
            ValidateBirthDate();
            ValidateEmail();
        }
    }
}
