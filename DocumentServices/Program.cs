using DocumentService.Extensions;
//using DocumentService.Interface.FileService;
//using DocumentService.Services.FileService;

var builder = WebApplication.CreateBuilder(args);
// Register services
builder.Services.AddApplicationServices(builder.Configuration);
//builder.Services.AddScoped<IFileService, FileService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
