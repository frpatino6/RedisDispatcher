namespace RedisDispatcher.Domain.Interfaces;

public interface IRedisService
{
    Task<string?> GetValueAsync(string client, string key);
    Task SetValueAsync(string client, string key, string value);
    Task SetAsync(string client, string key, string value);
}
