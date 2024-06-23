using PersonalBloggingPlatform.Web.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

//create endpoints for swagger.json
app.UseSwagger();

//create swagger UI for testing all web API endpoints/action methods
app.UseSwaggerUI();

app.MapControllers();

app.Run();
