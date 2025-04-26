using System.Text.Json.Serialization;

namespace RedisDispatcher.Domain.Models;

public class RedisSecretConfig
{
    [JsonPropertyName("host")]
    public string Host { get; set; } = string.Empty;

    [JsonPropertyName("port")]
    public int Port { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}
