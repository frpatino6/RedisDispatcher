namespace RedisDispatcher.Domain.Interfaces;

public interface IClientConfigurationResolver
{
    string GetRedisConnectionString(string client);
}
