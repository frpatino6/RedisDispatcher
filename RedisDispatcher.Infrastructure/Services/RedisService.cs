using StackExchange.Redis;
using RedisDispatcher.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using RedisDispatcher.Infrastructure.Common;
using System.Collections.Generic;

namespace RedisDispatcher.Infrastructure.Services;

public class RedisService : IRedisService
{
    private readonly IClientConfigurationResolver _resolver;
    private readonly Dictionary<string, ConnectionMultiplexer> _connections = new();
    private readonly ILogger<RedisService> _logger;

    public RedisService(IClientConfigurationResolver resolver, ILogger<RedisService> logger)
    {
        _resolver = resolver;
        _logger = logger;
    }


    public async Task<string?> GetValueAsync(string client, string key, int environment)
    {
        var db = await GetDatabaseAsync(client);
        var fullKey = $"{environment}_{key}";
        return await db.StringGetAsync(fullKey);
    }


    public async Task<IEnumerable<string>> GetAllKeysAsync(string client, string pattern = "*")
    {
        var connection = await EnsureConnectionAsync(client);
        var keys = new List<string>();

        foreach (var endpoint in connection.GetEndPoints())
        {
            var server = connection.GetServer(endpoint);
            keys.AddRange(server.Keys(pattern: pattern).Select(k => k.ToString()));
        }

        return keys;
    }


    public async Task SetValueAsync(string client, string key, string value, int environment)
    {
        var db = await GetDatabaseAsync(client);
        await db.StringSetAsync($"{environment}_{key}", value);
    }

    private async Task<IDatabase> GetDatabaseAsync(string client)
    {
        try
        {
            if (!_connections.TryGetValue(client, out var connection))
            {
                var connString = _resolver.GetRedisConnectionString(client);
                connection = await ConnectionMultiplexer.ConnectAsync(connString);
                _connections[client] = connection;
            }

            return connection.GetDatabase();
        }
        catch (RedisConnectionException ex)
        {
            _logger.LogError(ex, "Error connecting to Redis for client: {Client}", client);
            throw new InfrastructureException($"No se pudo conectar a Redis para el cliente {client}.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error getting Redis database for client: {Client}", client);
            throw new InfrastructureException(ex?.Message ?? "An unexpected error occurred.", ex);
        }
    }

    private async Task<ConnectionMultiplexer> EnsureConnectionAsync(string client)
    {
        try
        {
            if (!_connections.TryGetValue(client, out var connection))
            {
                var connString = _resolver.GetRedisConnectionString(client);
                connection = await ConnectionMultiplexer.ConnectAsync(connString);
                _connections[client] = connection;
            }

            return _connections[client];
        }
        catch (Exception ex) when (ex is RedisConnectionException)
        {
            _logger.LogError(ex, "Error connecting to Redis for client: {Client}", client);
            throw new InfrastructureException($"No se pudo conectar a Redis para el cliente {client}.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error getting Redis connection for client: {Client}", client);
            throw new InfrastructureException(ex.Message, ex);
        }
    }

    public async Task DeleteAsync(string client, string key, int environment)
    {
        var db = await GetDatabaseAsync(client);
        var fullKey = $"{environment}_{key}";
        await db.KeyDeleteAsync(fullKey);
    }

}
