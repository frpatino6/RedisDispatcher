using RedisDispatcher.Domain.Interfaces;

public class ClientConfigurationResolver : IClientConfigurationResolver
{
    private readonly IClientSecretProvider _secretProvider;

    public ClientConfigurationResolver(IClientSecretProvider secretProvider)
    {
        _secretProvider = secretProvider;
    }

    public string GetRedisConnectionString(string client)
    {
        var config = _secretProvider.GetSecret(client);
        return $"{config.Host}:{config.Port},password={config.Password}";
    }
}
