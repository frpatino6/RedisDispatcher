using StackExchange.Redis;
using RedisDispatcher.Domain.Interfaces;

namespace RedisDispatcher.Infrastructure.Services;

public class RedisService : IRedisService
{
    private readonly IClientConfigurationResolver _resolver;
    private readonly Dictionary<string, ConnectionMultiplexer> _connections = new();

    public RedisService(IClientConfigurationResolver resolver)
    {
        _resolver = resolver;
    }

    public async Task<string?> GetValueAsync(string client, string key)
    {
        var db = await GetDatabaseAsync(client);
        return await db.StringGetAsync(key);
    }

    public async Task SetAsync(string client, string key, string value)
    {
        var db = await GetDatabaseAsync(client);
        await db.StringSetAsync(key, value);
    }

    public async Task SetValueAsync(string client, string key, string value)
    {
        var db = await GetDatabaseAsync(client);
        await db.StringSetAsync(key, value);
    }


    private async Task<IDatabase> GetDatabaseAsync(string client)
    {
        if (!_connections.TryGetValue(client, out var connection))
        {
            var connString = _resolver.GetRedisConnectionString(client);
            connection = await ConnectionMultiplexer.ConnectAsync(connString);
            _connections[client] = connection;
        }

        return connection.GetDatabase();
    }
}
