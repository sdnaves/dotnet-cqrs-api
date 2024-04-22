namespace Sample.Domain.Commands.Customer.Validations
{
    public class DeleteCustomerCommandValidation : CustomerValidation<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidation()
        {
            ValidateId();
        }
    }
}
