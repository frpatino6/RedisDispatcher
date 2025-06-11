using MediatR;
using RedisDispatcher.Domain.Interfaces;

namespace RedisDispatcher.Application.Commands;

public class GetAndStoreSecretsCommandHandler : IRequestHandler<GetAndStoreSecretsCommand, List<KeyValuePair<string, bool>>>
{
    private readonly IRedisService _redisService;
    private readonly ISecretProvider _secretProvider;
    public GetAndStoreSecretsCommandHandler(IRedisService redisService, ISecretProvider secretProvider)
    {
        _redisService = redisService;
        _secretProvider = secretProvider;
    }

    public async Task<List<KeyValuePair<string, bool>>> Handle(GetAndStoreSecretsCommand request, CancellationToken cancellationToken)
    {
        var secretos = _secretProvider.GetSecrets(request.ClientId, request.Environment);
        var results = new List<KeyValuePair<string, bool>>();

        foreach (var kvp in secretos)
        {
            try
            {
                await _redisService.SetValueAsync(request.ClientId, kvp.Key, kvp.Value, request.Environment);
                results.Add(new KeyValuePair<string, bool>(kvp.Key, true));
            }
            catch
            {
                results.Add(new KeyValuePair<string, bool>(kvp.Key, false));
            }
        }

        return results;
    }
}
