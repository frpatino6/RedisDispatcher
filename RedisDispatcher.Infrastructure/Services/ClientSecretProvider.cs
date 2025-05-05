using RedisDispatcher.Domain.Interfaces;
using RedisDispatcher.Domain.Models;
using System.Text.Json;

namespace RedisDispatcher.Infrastructure.Services;

public class ClientSecretProvider : IClientSecretProvider
{
    public RedisSecretConfig GetSecret(string client)
    {
        var envVarName = $"REDIS__{client.ToUpper()}";
        var base64 = Environment.GetEnvironmentVariable(envVarName);

        if (string.IsNullOrWhiteSpace(base64))
            throw new InvalidOperationException($"Missing config for {envVarName}");

        var json = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64));
        return JsonSerializer.Deserialize<RedisSecretConfig>(json)
               ?? throw new InvalidOperationException("Failed to deserialize Redis secret.");
    }

}
