using Sample.Domain.Commands.Account.Validations;

namespace Sample.Domain.Commands.Account
{
    public class CreateAccountCommand : AccountCommand
    {
        public CreateAccountCommand(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateAccountCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
