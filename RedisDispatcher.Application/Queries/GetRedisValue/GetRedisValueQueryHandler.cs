using MediatR;
using RedisDispatcher.Domain.Interfaces;

namespace RedisDispatcher.Application.Queries.GetRedisValue;

public class GetRedisValueQueryHandler : IRequestHandler<GetRedisValueQuery, string?>
{
    private readonly IRedisService _redisService;

    public GetRedisValueQueryHandler(IRedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task<string?> Handle(GetRedisValueQuery request, CancellationToken cancellationToken)
    {
        return await _redisService.GetValueAsync(request.Client, request.Key, request.environment);
    }
}
