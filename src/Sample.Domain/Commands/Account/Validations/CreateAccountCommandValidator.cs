namespace Sample.Domain.Commands.Account.Validations
{
    public class CreateAccountCommandValidator : AccountCommandValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            ValidateName();
            ValidateEmail();
        }
    }
}
