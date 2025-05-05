using MediatR;

namespace RedisDispatcher.Application.Commands;

public class GetAndStoreSecretsCommand : IRequest<bool>
{
    public string ClientId { get; set; } = default!;
}
