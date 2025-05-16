using StackExchange.Redis;

namespace RedisDispatcher.Domain.Interfaces;

public interface IRedisService
{
    Task<string?> GetValueAsync(string client, string key);
    Task SetValueAsync(string client, string key, string value);
    Task SetAsync(string client, string key, string value);
    Task<IEnumerable<string>> GetAllKeysAsync(string client, string pattern = "*");
    Task DeleteAsync(string client, string key);
}
