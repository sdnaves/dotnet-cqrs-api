
using Sample.Services.Api.Configurations;

namespace Sample.Services.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                   .SetBasePath(builder.Environment.ContentRootPath)
                   .AddJsonFile("appsettings.json", false, true)
                   .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                   .AddEnvironmentVariables();

            // AutoMapper Config
            builder.Services.AddAutoMapperConfiguration();

            // Swagger Config
            builder.Services.AddSwaggerConfiguration();

            // MediatR Config
            builder.Services.AddMediatRConfiguration();

            // .NET Native DI Abstraction
            builder.Services.AddDependencyInjectionConfiguration();

            // Controllers
            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwaggerSetup();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
