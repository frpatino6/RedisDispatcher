using Microsoft.AspNetCore.Mvc;
using RedisDispatcher.Application.Commands;
using MediatR;
using RedisDispatcher.Application.Queries;

namespace RedisDispatcher.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GetSecretsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetSecretsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{clientId}")]
    public async Task<IActionResult> Post(string clientId)
    {
        var result = await _mediator.Send(new GetAndStoreSecretsCommand { ClientId = clientId });
        return Ok(result);
    }

    [HttpGet("{clientId}/{pattern}")]
    public async Task<IActionResult> Get(string clientId, string pattern = "*")
    {
        var result = await _mediator.Send(new GetAllRedisValuesQuery(clientId, pattern));
        return Ok(result);
    }
}
