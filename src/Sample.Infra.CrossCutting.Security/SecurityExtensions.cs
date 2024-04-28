using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sample.Infra.CrossCutting.Security.Interfaces;
using Sample.Infra.CrossCutting.Security.Jwt;
using System.Text;

namespace Sample.Infra.CrossCutting.Security
{
    public static class SecurityExtensions
    {
        public static void AddAuthConfiguration(this IServiceCollection services, IConfiguration configuration, string key = "Token")
        {
            ArgumentNullException.ThrowIfNull(services);

            var securitySettingsSection = configuration.GetSection(key);

            services.Configure<SecuritySettings>(options =>
            {
                securitySettingsSection.Bind(options);
            });

            var settings = securitySettingsSection.Get<SecuritySettings>();

            SecurityKeyConfiguration(services, settings!);

            services.AddHttpContextAccessor();

            services.AddScoped<IJwtBuilder, JwtBuilder>();
        }

        private static void SecurityKeyConfiguration(IServiceCollection services, SecuritySettings settings)
        {
            ArgumentNullException.ThrowIfNull(settings);

            var key = Encoding.ASCII.GetBytes(settings.SecurityKey);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = true;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidAudience = settings.Audience,
                            ValidIssuer = settings.Issuer
                        };
                    });
        }

        public static void UseAuthConfiguration(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app);

            app.UseAuthentication();

            app.UseAuthorization();
        }
    }
}
