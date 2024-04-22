using Microsoft.Extensions.DependencyInjection;
using Sample.Application.Interfaces;
using Sample.Application.Services;
using Sample.Domain.Interfaces;
using Sample.Infra.CrossCutting.Mediator;
using Sample.Infra.Data.Contexts;
using Sample.Infra.Data.Repositories;

namespace Sample.Infra.CrossCutting.Ioc
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

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

        private static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
        }

        private static void RegisterDomainEvents(this IServiceCollection services) { }

        private static void RegisterDomainCommands(this IServiceCollection services) { }

        private static void RegisterInfraData(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            // Context
            services.AddScoped<SampleContext>();
        }

        private static void RegisterInfraDataEventSourcing(this IServiceCollection services) { }
    }
}
