using DocumentService.Infrastructure.Authentication;
using DocumentService.Interface;
using DocumentService.Interface.FileService;
using DocumentService.Licensing;
using DocumentService.Models;
using DocumentService.Service;
using DocumentService.Services;
using DocumentService.Services.FileService;
using DocumentServices.Interface.GetFIleServiceInterface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using System;



// Removed the incorrect namespace reference
// using DocumentServices.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);


// Register services

// Register strongly-typed JwtSettings
// services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

// Register services
//services.AddScoped<IAuthService, Services.AuthService>();
builder.Services.AddHttpClient();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<IAMSettings>(builder.Configuration.GetSection("IAM"));
builder.Services.Configure<AuthConfiguration>(builder.Configuration.GetSection("AuthConfiguration"));
builder.Services.AddAuthentication("IamScheme")
.AddScheme<AuthenticationSchemeOptions, IamTokenAuthenticationHandler>("IamScheme", static options => { });

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter token received from IAM. Example: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference{
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});



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
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IIAMAuthService, IAMAuthService>();
builder.Services.AddScoped<IAnnotatedFileService, AnnotatedFileService>();

AsposeLicenseHelper.ApplyLicenses();
builder.WebHost.UseUrls("http://0.0.0.0:5000");
var app = builder.Build();
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthentication();
app.UseAuthorization();




app.MapControllers();

app.Run();
