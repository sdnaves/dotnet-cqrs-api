using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sample.Application.Interfaces;
using Sample.Application.Services;
using Sample.Domain.Commands.Account;
using Sample.Domain.Events.Account;
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
            services.RegisterMediator();

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

        private static void RegisterMediator(this IServiceCollection services)
        {
            services.AddScoped<IEventStore, EventStore>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();
        }

        private static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IHistoryService, HistoryService>();
        }

        private static void RegisterDomainEvents(this IServiceCollection services)
        {
            // Account
            services.AddScoped<INotificationHandler<AccountCreatedEvent>, AccountEventHandler>();
            services.AddScoped<INotificationHandler<AccountUpdatedEvent>, AccountEventHandler>();
            services.AddScoped<INotificationHandler<AccountDeletedEvent>, AccountEventHandler>();
        }

        private static void RegisterDomainCommands(this IServiceCollection services)
        {
            // Account
            services.AddScoped<IRequestHandler<CreateAccountCommand, ValidationResult>, AccountCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateAccountCommand, ValidationResult>, AccountCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteAccountCommand, ValidationResult>, AccountCommandHandler>();
        }

        private static void RegisterInfraData(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();

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
