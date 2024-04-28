using FluentValidation;
using MongoDB.Bson;

namespace Sample.Domain.Commands.Account.Validations
{
    public abstract class AccountCommandValidator<T> : AbstractValidator<T> where T : AccountCommand
    {
        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(2, 150).WithMessage("The Name must have between 2 and 150 characters");
        }

        protected void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(ObjectId.Empty);
        }
    }
}
