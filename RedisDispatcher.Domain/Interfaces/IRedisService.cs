using StackExchange.Redis;

namespace RedisDispatcher.Domain.Interfaces;

public interface IRedisService
{
    Task<string?> GetValueAsync(string client, string key, int environment);
    Task SetValueAsync(string client, string key, string value, int environment);
    Task<IEnumerable<string>> GetAllKeysAsync(string client, string pattern = "*");
    Task DeleteAsync(string client, string key, int environment);
}
