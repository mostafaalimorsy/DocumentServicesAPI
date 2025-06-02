using DocumentService.Interface;
using DocumentService.Models;
using DocumentService.Service;
using DocumentService.Services;
using DocumentServices.Interface.GetFIleServiceInterface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            services.AddScoped<IAnnotatedFileService, AnnotatedFileService>();

            //services.Configure<JwtSettings>(
            //configuration.GetSection("Jwt"));

            //var jwtSettings = configuration
            //    .GetSection("Jwt")
            //    .Get<JwtSettings>();

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.RequireHttpsMetadata = false;
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = jwtSettings.Issuer,
            //        ValidAudience = jwtSettings.Audience,
            //        IssuerSigningKey = new SymmetricSecurityKey(
            //            Encoding.UTF8.GetBytes(jwtSettings.Key))
            //    };
            //});



            return services;
        }
    }
}
