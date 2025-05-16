using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedisDispatcher.Application.Commads;
using RedisDispatcher.Application.Queries;

namespace RedisDispatcher.API.Controllers;

[ApiController]
[Route("api/data")]
public class DataController : ControllerBase
{
    private readonly IMediator _mediator;

    public DataController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{client}/{key}")]
    public async Task<IActionResult> GetValue(string client, string key)
    {
        var value = await _mediator.Send(new GetRedisValueQuery(client, key));
        return value is not null ? Ok(new { value }) : NotFound();
    }

    [HttpPost("{client}")]
    public async Task<IActionResult> PostAsync(string client, [FromBody] AddDataRequest request)
    {
        var command = new AddDataCommand(client, request.Key, request.Value);
        await _mediator.Send(command);
        return Ok(new { message = "Value added successfully." });
    }
    [HttpDelete("{client}/{key}")]
    public async Task<IActionResult> DeleteAsync(string client,string key)
    {
        var command = new DeleteDataCommand(client, key);
        await _mediator.Send(command);
        return Ok(new { message = "Value deleted successfully." });
    }
}
