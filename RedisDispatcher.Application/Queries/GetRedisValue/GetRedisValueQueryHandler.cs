using MediatR;
using Microsoft.Extensions.Logging;
using RedisDispatcher.Domain.Exceptions;
using RedisDispatcher.Domain.Interfaces;

namespace RedisDispatcher.Application.Queries.GetRedisValue;

public class GetRedisValueQueryHandler : IRequestHandler<GetRedisValueQuery, string?>
{
    private readonly IRedisService _redisService;
    private readonly ILogger<GetRedisValueQueryHandler> _logger;

    public GetRedisValueQueryHandler(IRedisService redisService, ILogger<GetRedisValueQueryHandler> logger)
    {
        _redisService = redisService;
        _logger = logger;
    }

    public async Task<string?> Handle(GetRedisValueQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Client) || string.IsNullOrWhiteSpace(request.Key))
        {
            _logger.LogWarning("Invalid request: Client or Key is null or empty.");
            return null;
        }

        var result = await _redisService.GetValueAsync(request.Client, request.Key);

        if (result == null)
        {
            _logger.LogInformation("No value found for Client: {Client}, Key: {Key}.", request.Client, request.Key);
            throw new NotFoundException(nameof(request), request);
        }
        else
        {
            _logger.LogInformation("Value retrieved for Client: {Client}, Key: {Key}.", request.Client, request.Key);
        }

        return result;
    }
}
