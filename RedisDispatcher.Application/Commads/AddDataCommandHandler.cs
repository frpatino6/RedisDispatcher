using MediatR;
using Microsoft.Extensions.Logging;
using RedisDispatcher.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisDispatcher.Application.Commads
{
    public class AddDataCommandHandler : IRequestHandler<AddDataCommand>
    {
        private readonly IRedisService _redisService;
        private readonly ILogger<AddDataCommandHandler> _logger;

        public AddDataCommandHandler(IRedisService redisService, ILogger<AddDataCommandHandler> logger )
        {
            _redisService = redisService;
            _logger = logger;   
        }

        public async Task Handle(AddDataCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Executing command logic with client: {Client}, key: {Key}, value: {Value}", request.Client, request.Key, request.Value);
                await _redisService.SetValueAsync(request.Client, request.Key, request.Value, request.environment);
                _logger.LogInformation("Successfully set value for client: {Client}, key: {Key}", request.Client, request.Key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing AddDataCommand for client: {Client}, key: {Key}", request.Client, request.Key);
                throw;
            }
        }
    }

}
