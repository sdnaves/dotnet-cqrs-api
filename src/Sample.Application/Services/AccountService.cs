using AutoMapper;
using FluentValidation.Results;
using MongoDB.Bson;
using Sample.Application.Interfaces;
using Sample.Application.ViewModels;
using Sample.Domain.Commands.Account;
using Sample.Domain.Core.Models;
using Sample.Domain.Interfaces;
using Sample.Infra.CrossCutting.Mediator.Interfaces;
using Sample.Infra.CrossCutting.Security.Utilities;

namespace Sample.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        private readonly IHistoryService _historyService;

        private readonly IAccountRepository _accountRepository;

        public AccountService(IMapper mapper, IMediatorHandler mediator, IHistoryService historyService, IAccountRepository accountRepository)
        {
            _mapper = mapper;
            _mediator = mediator;

            _historyService = historyService;

            _accountRepository = accountRepository;
        }

        public async Task<IEnumerable<AccountViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<AccountViewModel>>(await _accountRepository.GetAllAsync());
        }

        public async Task<AccountViewModel> GetById(string id)
        {
            var success = ObjectId.TryParse(id, out var objectId);

            if (!success)
                return default!;

            return _mapper.Map<AccountViewModel>(await _accountRepository.GetByIdAsync(objectId));
        }

        public async Task<AccountViewModel> GetByEmail(string email)
        {
            return _mapper.Map<AccountViewModel>(await _accountRepository.FindOneAsync(f => f.Email.Equals(email.Trim())));
        }

        public async Task<ValidationResult> Create(CreateAccountViewModel createAccountViewModel)
        {
            var createCommand = _mapper.Map<CreateAccountCommand>(createAccountViewModel);
            return await _mediator.SendCommand(createCommand);
        }

        public async Task<ValidationResult> Update(AccountViewModel accountViewModel)
        {
            var updateCommand = _mapper.Map<UpdateAccountCommand>(accountViewModel);
            return await _mediator.SendCommand(updateCommand);
        }

        public async Task<ValidationResult> Delete(string id)
        {
            var success = ObjectId.TryParse(id, out var objectId);

            if (!success)
            {
                var validationResult = new ValidationResult();
                validationResult.Errors.Add(new ValidationFailure(string.Empty, "The account doesn't exists."));
                return validationResult;
            }

            var deleteCommand = new DeleteAccountCommand(objectId);
            return await _mediator.SendCommand(deleteCommand);
        }

        public async Task<IList<HistoryData<AccountViewModel>>> GetAllHistory(string id)
        {
            return await _historyService.GetHistoryData<AccountViewModel>(id);
        }
    }
}
