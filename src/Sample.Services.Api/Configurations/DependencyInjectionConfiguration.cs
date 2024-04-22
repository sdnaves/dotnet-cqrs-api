using Sample.Infra.CrossCutting.Ioc;

namespace Sample.Services.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
