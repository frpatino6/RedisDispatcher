using MediatR;

namespace RedisDispatcher.Application.Commands;

public class GetAndStoreSecretsCommand : IRequest<List<KeyValuePair<string, bool>>>
{
    public string ClientId { get; set; } = default!;
    public int Environment { get; set; }
}
