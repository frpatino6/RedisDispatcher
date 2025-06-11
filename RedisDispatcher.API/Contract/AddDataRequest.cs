namespace RedisDispatcher.API.Contract
{
    public record AddDataRequest(string Client, string Key, string Value, int Environment);
}