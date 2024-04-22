using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sample.Application.Interfaces;
using Sample.Application.Services;
using Sample.Domain.Commands.Customer;
using Sample.Domain.Events.Customer;
using Sample.Domain.Interfaces;
using Sample.Infra.CrossCutting.Mediator;
using Sample.Infra.CrossCutting.Mediator.Interfaces;
using Sample.Infra.Data;
using Sample.Infra.Data.Contexts;
using Sample.Infra.Data.Repositories;

namespace Sample.Infra.CrossCutting.Ioc
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Mediator
            services.RegisterMeediator();

            // Application
            services.RegisterApplicationServices();

            // Domain - Events
            services.RegisterDomainEvents();

            // Domain - Commands
            services.RegisterDomainCommands();

            // Infra - Data
            services.RegisterInfraData();

            // Infra - Data EventSourcing
            services.RegisterInfraDataEventSourcing();
        }

        private static void RegisterMeediator(this IServiceCollection services)
        {
            services.AddScoped<IEventStore, EventStore>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();
        }

        private static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
        }

        private static void RegisterDomainEvents(this IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<CustomerCreatedEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerUpdatedEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerDeletedEvent>, CustomerEventHandler>();
        }

        private static void RegisterDomainCommands(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CreateCustomerCommand, ValidationResult>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand, ValidationResult>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteCustomerCommand, ValidationResult>, CustomerCommandHandler>();
        }

        private static void RegisterInfraData(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            // Context
            services.AddScoped<SampleContext>();
        }

        private static void RegisterInfraDataEventSourcing(this IServiceCollection services)
        {
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();

            // Context
            services.AddScoped<EventStoreContext>();
        }
    }
}
