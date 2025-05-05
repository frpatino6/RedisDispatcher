using MediatR;

namespace RedisDispatcher.Application.Queries.GetRedisValue;

public record GetRedisValueQuery(string Client, string Key) : IRequest<string?>;
