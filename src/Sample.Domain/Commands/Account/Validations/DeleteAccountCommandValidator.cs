namespace Sample.Domain.Commands.Account.Validations
{
    public class DeleteAccountCommandValidator : AccountCommandValidator<DeleteAccountCommand>
    {
        public DeleteAccountCommandValidator()
        {
            ValidateId();
        }
    }
}
