namespace RedisDispatcher.Domain.Interfaces
{
    public interface ISecretProvider
    {
        Dictionary<string, string> GetSecrets(string clientId);
    }
}
