using PersonalBloggingPlatform.Web.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureServices(builder.Configuration, builder.Environment);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

if (!app.Environment.IsEnvironment("Test"))
{
    // Create endpoints for swagger.json
    app.UseSwagger();

    // Create swagger UI for testing all web API endpoints/action methods
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

public partial class Program { }