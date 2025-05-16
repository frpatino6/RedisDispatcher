using MediatR;
using StackExchange.Redis;

namespace RedisDispatcher.Application.Queries;

public record GetAllRedisValuesQuery(string ClientId, string Pattern = "*") : IRequest<IEnumerable<string>?>;
