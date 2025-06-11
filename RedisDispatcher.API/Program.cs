using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RedisDispatcher.Application.Queries;
using RedisDispatcher.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


var assembly = Assembly.GetExecutingAssembly();
var version = assembly.GetName().Version?.ToString() ?? "Unknown";
Console.WriteLine($"Assembly Version: {version}");

if (builder.Environment.IsDevelopment())
{
    var envPath = Path.Combine("..", ".env");
    var fullPath = Path.GetFullPath(envPath);
    Console.WriteLine("Cargando .env desde: " + fullPath);
    DotNetEnv.Env.Load(fullPath);
}

// Framework services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Console.WriteLine("Archivo .env existe? " + File.Exists(Path.Combine(Directory.GetCurrentDirectory(), ".env")));
// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetRedisValueQuery).Assembly);
});

builder.Services.AddInfrastructure();

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
