using RedisDispatcher.Domain.Models;

namespace RedisDispatcher.Domain.Interfaces;

public interface IClientSecretProvider
{
    RedisSecretConfig GetSecret(string client);
}
