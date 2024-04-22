using FluentValidation.Results;
using MediatR;
using MongoDB.Bson;
using Sample.Domain.Events.Customer;
using Sample.Domain.Interfaces;

namespace Sample.Domain.Commands.Customer
{
    public class CustomerCommandHandler : CommandHandler,
                                          IRequestHandler<CreateCustomerCommand, ValidationResult>,
                                          IRequestHandler<UpdateCustomerCommand, ValidationResult>,
                                          IRequestHandler<DeleteCustomerCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ValidationResult> Handle(CreateCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var customer = new Models.Customer(ObjectId.GenerateNewId(), message.Name, message.Email, message.BirthDate);

            if (await _customerRepository.FindOneAsync(f => f.Email.Equals(customer.Email)) != null)
            {
                AddError("The customer e-mail has already been taken.");
                return ValidationResult;
            }

            customer.AddDomainEvent(new CustomerCreatedEvent(customer.Id, customer.Name, customer.Email, customer.BirthDate));

            _customerRepository.Add(customer);

            return await Commit(_customerRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdateCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var customer = new Models.Customer(message.Id, message.Name, message.Email, message.BirthDate);

            var existingCustomer = await _customerRepository.FindOneAsync(f => f.Email.Equals(customer.Email));

            if (existingCustomer != null && existingCustomer.Id != customer.Id)
            {
                if (!existingCustomer.Equals(customer))
                {
                    AddError("The customer e-mail has already been taken.");
                    return ValidationResult;
                }
            }

            customer.AddDomainEvent(new CustomerUpdatedEvent(customer.Id, customer.Name, customer.Email, customer.BirthDate));

            _customerRepository.Update(customer);

            return await Commit(_customerRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(DeleteCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var customer = await _customerRepository.GetByIdAsync(message.Id);

            if (customer is null)
            {
                AddError("The customer doesn't exists.");
                return ValidationResult;
            }

            customer.AddDomainEvent(new CustomerDeletedEvent(message.Id));

            _customerRepository.Delete(customer);

            return await Commit(_customerRepository.UnitOfWork);
        }

        public void Dispose()
        {
            _customerRepository.Dispose();
        }
    }
}
