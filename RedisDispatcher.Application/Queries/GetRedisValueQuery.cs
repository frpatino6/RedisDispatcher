using MediatR;

namespace RedisDispatcher.Application.Queries;

public record GetRedisValueQuery(string Client, string Key) : IRequest<string?>;
