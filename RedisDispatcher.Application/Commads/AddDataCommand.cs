using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisDispatcher.Application.Commads
{
    public record AddDataCommand(string Client, string Key, string Value) : IRequest;

}
