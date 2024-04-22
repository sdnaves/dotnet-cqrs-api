using AutoMapper;
using FluentValidation.Results;
using MongoDB.Bson;
using Sample.Application.Interfaces;
using Sample.Application.ViewModels;
using Sample.Domain.Commands.Customer;
using Sample.Domain.Interfaces;
using Sample.Infra.CrossCutting.Mediator.Interfaces;

namespace Sample.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        private readonly ICustomerRepository _customerRepository;

        public CustomerService(IMapper mapper, IMediatorHandler mediator, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _mediator = mediator;

            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<CustomerViewModel>>(await _customerRepository.GetAllAsync());
        }

        public async Task<CustomerViewModel> GetById(string id)
        {
            var success = ObjectId.TryParse(id, out var objectId);

            if (!success)
                return default!;

            return _mapper.Map<CustomerViewModel>(await _customerRepository.GetByIdAsync(objectId));
        }

        public async Task<ValidationResult> Create(CustomerViewModel customerViewModel)
        {
            var createCommand = _mapper.Map<CreateCustomerCommand>(customerViewModel);
            return await _mediator.SendCommand(createCommand);
        }

        public async Task<ValidationResult> Update(CustomerViewModel customerViewModel)
        {
            var updateCommand = _mapper.Map<UpdateCustomerCommand>(customerViewModel);
            return await _mediator.SendCommand(updateCommand);
        }

        public async Task<ValidationResult> Delete(string id)
        {
            var success = ObjectId.TryParse(id, out var objectId);

            if (!success)
            {
                var validationResult = new ValidationResult();
                validationResult.Errors.Add(new ValidationFailure(string.Empty, "The customer doesn't exists."));
                return validationResult;
            }

            var deleteCommand = new DeleteCustomerCommand(objectId);
            return await _mediator.SendCommand(deleteCommand);
        }
    }
}
