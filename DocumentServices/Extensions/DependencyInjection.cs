using DocumentService.Interface;
using DocumentService.Models;
using DocumentService.Services;

namespace DocumentService.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register strongly-typed JwtSettings
           // services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            // Register services
            //services.AddScoped<IAuthService, Services.AuthService>();
            services.AddHttpClient();
            services.AddScoped<IIAMAuthService, IAMAuthService>();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.Configure<IAMSettings>(configuration.GetSection("IAM"));
            services.Configure<AuthConfiguration>(configuration.GetSection("AuthConfiguration"));
            




            return services;
        }
    }
}
