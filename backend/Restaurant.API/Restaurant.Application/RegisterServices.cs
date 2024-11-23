using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.IdentityServices;
using Restaurant.Application.Models.Identities.JWTSettings;

namespace Restaurant.Application
{
    public static class RegisterServices
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IIdentityService, IdentityService>();

            services.

            return services;
        }
    }
}
