using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Restaurant.Application
{
    public static class RegisterServices
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            return services;
        }
    }
}
