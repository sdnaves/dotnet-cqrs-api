using FluentValidation.Results;
using MediatR;
using MongoDB.Bson;
using Sample.Domain.Events.Account;
using Sample.Domain.Interfaces;
using Sample.Infra.CrossCutting.Security.Utilities;

namespace Sample.Domain.Commands.Account
{
    public class AccountCommandHandler : CommandHandler,
                                         IRequestHandler<CreateAccountCommand, ValidationResult>,
                                         IRequestHandler<UpdateAccountCommand, ValidationResult>,
                                         IRequestHandler<DeleteAccountCommand, ValidationResult>
    {
        private readonly IAccountRepository _accountRepository;

        public AccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<ValidationResult> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var account = new Models.Account(ObjectId.GenerateNewId(), request.Name, request.Email, request.Password);

            account.Password = SecurityUtility.ComputeSha256Hash(account.Password);

            if (await _accountRepository.FindOneAsync(f => f.Email.Equals(account.Email)) != null)
            {
                AddError("This account e-mail has already been taken.");
                return ValidationResult;
            }

            account.AddDomainEvent(new AccountCreatedEvent(account.Id, account.Name, account.Email, account.Password));

            _accountRepository.Add(account);

            return await Commit(_accountRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var account = new Models.Account(request.Id, request.Name, request.Email, request.Password);

            var existingAccount = await _accountRepository.FindOneAsync(f => f.Email.Equals(account.Email));

            if (existingAccount != null && existingAccount.Id != account.Id)
            {
                if (!existingAccount.Equals(account))
                {
                    AddError("This account e-mail has already been taken.");
                    return ValidationResult;
                }
            }

            account.AddDomainEvent(new AccountUpdatedEvent(account.Id, account.Name, account.Email, account.Password));

            _accountRepository.Update(account);

            return await Commit(_accountRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(DeleteAccountCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var account = await _accountRepository.GetByIdAsync(message.Id);

            if (account is null)
            {
                AddError("The account doesn't exists.");
                return ValidationResult;
            }

            account.AddDomainEvent(new AccountDeletedEvent(account.Id));

            _accountRepository.Delete(account);

            return await Commit(_accountRepository.UnitOfWork);
        }

        public void Dispose()
        {
            _accountRepository.Dispose();
        }
    }
}
