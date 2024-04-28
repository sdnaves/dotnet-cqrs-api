namespace Sample.Services.Api.Configurations
{
    public static class MediatorConfiguration
    {
        public static void AddMediatorConfiguration(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });
        }
    }
}
