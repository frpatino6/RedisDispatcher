using MediatR;
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

        public AddDataCommandHandler(IRedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task Handle(AddDataCommand request, CancellationToken cancellationToken)
        {
            await _redisService.SetValueAsync(request.Client, request.Key, request.Value);
        }
    }

}
