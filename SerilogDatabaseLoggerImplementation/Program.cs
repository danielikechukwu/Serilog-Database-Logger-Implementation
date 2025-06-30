using Serilog;
using SerilogDatabaseLoggerImplementation.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the HostBuilder to use Serilog as the logging provider.
builder.Host.UseSerilog((context, services, configuration) =>
{
    // Reads Serilog settings from appsettings.json (or other configuration sources)
    configuration.ReadFrom.Configuration(context.Configuration);
});

//Registering the Background service
builder.Services.AddHostedService<LogCleanupService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
