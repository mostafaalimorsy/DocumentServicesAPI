//using DocumentService.Infrastructure.Authentication;
//using DocumentService.Interface;
//using DocumentService.Models;

//using DocumentServices.Interface.GetFIleServiceInterface;
////using Microsoft.AspNetCore.Authentication;
////using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
////using Microsoft.OpenApi.Models;

//namespace DocumentService.Extensions
//{
//    public static class DependencyInjection
//    {
//        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
//        {
//            // Register strongly-typed JwtSettings
//            // services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

//            // Register services
//            //services.AddScoped<IAuthService, Services.AuthService>();
//            services.AddHttpClient();
            
//            services.AddControllers();
//            services.AddEndpointsApiExplorer();
//            services.AddAuthentication("IamScheme")
//            .AddScheme<AuthenticationSchemeOptions, IamTokenAuthenticationHandler>("IamScheme", options => { });

//            services.AddAuthorization();

//            services.AddSwaggerGen(c =>
//            {
//                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//                {
//                    Description = "Enter token received from IAM. Example: Bearer {token}",
//                    Name = "Authorization",
//                    In = ParameterLocation.Header,
//                    Type = SecuritySchemeType.ApiKey,
//                    Scheme = "Bearer"
//                });

//                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
//        {
//            new OpenApiSecurityScheme{
//                Reference = new OpenApiReference{
//                    Type=ReferenceType.SecurityScheme,
//                    Id="Bearer"
//                }
//            },
//            Array.Empty<string>()
//        }
//    });
//            });

//            services.Configure<IAMSettings>(configuration.GetSection("IAM"));
//            services.Configure<AuthConfiguration>(configuration.GetSection("AuthConfiguration"));

//            //services.Configure<JwtSettings>(
//            //configuration.GetSection("Jwt"));

//            //var jwtSettings = configuration
//            //    .GetSection("Jwt")
//            //    .Get<JwtSettings>();

//            //services.AddAuthentication(options =>
//            //{
//            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//            //})
//            //.AddJwtBearer(options =>
//            //{
//            //    options.RequireHttpsMetadata = false;
//            //    options.TokenValidationParameters = new TokenValidationParameters
//            //    {
//            //        ValidateIssuer = true,
//            //        ValidateAudience = true,
//            //        ValidateLifetime = true,
//            //        ValidateIssuerSigningKey = true,
//            //        ValidIssuer = jwtSettings.Issuer,
//            //        ValidAudience = jwtSettings.Audience,
//            //        IssuerSigningKey = new SymmetricSecurityKey(
//            //            Encoding.UTF8.GetBytes(jwtSettings.Key))
//            //    };
//            //});



//            return services;
//        }
//    }
//}
