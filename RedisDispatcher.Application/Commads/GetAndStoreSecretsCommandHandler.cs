using MediatR;
using RedisDispatcher.Domain.Interfaces;

namespace RedisDispatcher.Application.Commands;

public class GetAndStoreSecretsCommandHandler : IRequestHandler<GetAndStoreSecretsCommand, bool>
{
    private readonly IRedisService _redisService;
    private readonly ISecretProvider _secretProvider;
    public GetAndStoreSecretsCommandHandler(IRedisService redisService, ISecretProvider secretProvider)
    {
        _redisService = redisService;
        _secretProvider = secretProvider;
    }

    public async Task<bool> Handle(GetAndStoreSecretsCommand request, CancellationToken cancellationToken)
    {
        var secretos = _secretProvider.GetSecrets(request.ClientId);

        foreach (var kvp in secretos)
        {
            await _redisService.SetAsync(request.ClientId, kvp.Key, kvp.Value);
        }

        return true;
    }
}
