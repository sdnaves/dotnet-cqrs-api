namespace Sample.Domain.Commands.Account.Validations
{
    public class UpdateAccountCommandValidator : AccountCommandValidator<UpdateAccountCommand>
    {
        public UpdateAccountCommandValidator()
        {
            ValidateId();
            ValidateName();
            ValidateEmail();
        }
    }
}
