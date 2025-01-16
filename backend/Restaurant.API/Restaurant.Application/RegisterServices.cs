using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Application.Services.BasketServices;
using Restaurant.Application.Services.FileServices;
using Restaurant.Application.Services.IdentityServices;
using Restaurant.Application.Services.MealServices;
using Restaurant.Application.Services.OrderServices;
using Restaurant.Application.Services.ProductServices;
using System.Text;

namespace Restaurant.Application
{
    public static class RegisterServices
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IMealService, MealService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();

            // add Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // add JWT Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = configuration["JWTSetting:JWT:ValidAudience"],
                    ValidIssuer = configuration["JWTSetting:JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSetting:JWT:Secret"]!))
                };
            });


            return services;
        }
    }
}
