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
    public  class DeleteDataCommandHandler: IRequestHandler<DeleteDataCommand>
    {
        private readonly IRedisService _redisService;
        private readonly ILogger<AddDataCommandHandler> _logger;

        public DeleteDataCommandHandler(IRedisService redisService, ILogger<AddDataCommandHandler> logger)
        {
            _redisService = redisService;
            _logger = logger;
        }

        public async Task Handle(DeleteDataCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Executing command logic with client: {Client}, key: {Key}", request.Client, request.Key);
                await _redisService.DeleteAsync(request.Client, request.Key);
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
