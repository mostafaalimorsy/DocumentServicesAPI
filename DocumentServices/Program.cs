using DocumentService.Extensions;
using DocumentService.Interface.FileService;
using DocumentService.Licensing;
using DocumentService.Services.FileService;

var builder = WebApplication.CreateBuilder(args);
// Register services
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddScoped<IFileService, FileService>();
AsposeLicenseHelper.ApplyLicenses();
builder.WebHost.UseUrls("http://0.0.0.0:5000");
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthentication();
app.UseAuthorization();


app.UseStaticFiles();

app.MapControllers();

app.Run();
