using Microsoft.Extensions.DependencyInjection;
using RedisDispatcher.Domain.Interfaces;
using RedisDispatcher.Infrastructure.Services;

namespace RedisDispatcher.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IClientSecretProvider, ClientSecretProvider>();
        services.AddScoped<IClientConfigurationResolver, ClientConfigurationResolver>();
        services.AddScoped<IRedisService, RedisService>();
        services.AddScoped<ISecretProvider, UtilSycSecretProvider>();
        return services;
    }
}
