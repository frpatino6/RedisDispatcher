using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedisDispatcher.API.Contract;
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

    [HttpGet("{client}/{key}/{environment}")]
    public async Task<IActionResult> GetValue(string client, string key, int environment)
    {
        var value = await _mediator.Send(new GetRedisValueQuery(client, key, environment));
        return value is not null ? Ok(new { value }) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] AddDataRequest request)
    {
        var command = new AddDataCommand(request.Client, request.Key, request.Value, request.Environment);
        await _mediator.Send(command);
        return Ok(new { message = "Value added successfully." });
    }
    [HttpDelete("{client}/{key}/{environment}")]
    public async Task<IActionResult> DeleteAsync(string client,string key, int environment)
    {
        var command = new DeleteDataCommand(client, key, environment);
        await _mediator.Send(command);
        return Ok(new { message = "Value deleted successfully." });
    }
}
