using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RedisDispatcher.Application.Queries.GetRedisValue;
using RedisDispatcher.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

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

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
