using MediatR;
using Microsoft.Extensions.Logging;
using RedisDispatcher.Domain.Interfaces;
using StackExchange.Redis;

namespace RedisDispatcher.Application.Queries;

public class GetAllRedisValuesQueryHandler : IRequestHandler<GetAllRedisValuesQuery, IEnumerable<string>>
{
    private readonly IRedisService _redisService;
    private readonly ILogger<GetAllRedisValuesQueryHandler> _logger;

    public GetAllRedisValuesQueryHandler(IRedisService redisService, ILogger<GetAllRedisValuesQueryHandler> logger)
    {
        _redisService = redisService;
        _logger = logger;
    }

    public async Task<IEnumerable<string>> Handle(GetAllRedisValuesQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.ClientId) || string.IsNullOrWhiteSpace(request.Pattern))
        {
            _logger.LogWarning("Invalid request: Client or Pattern is null or empty.");
            return Enumerable.Empty<string>();
        }

        var keys = await _redisService.GetAllKeysAsync(request.ClientId, request.Pattern);

        if (!keys.Any())
        {
            _logger.LogInformation("No keys found for Client: {Client}, Pattern: {Pattern}.", request.ClientId, request.Pattern);
        }
        else
        {
            _logger.LogInformation("Keys retrieved for Client: {Client}, Pattern: {Pattern}.", request.ClientId, request.Pattern);
        }

        return keys;
    }
}
