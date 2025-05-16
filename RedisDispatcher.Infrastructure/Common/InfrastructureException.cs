using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisDispatcher.Infrastructure.Common
{
    public class InfrastructureException : Exception
    {
        public InfrastructureException(string message, Exception innerException = null)
            : base(message, innerException) { }

        public InfrastructureException(string message)
          : base(message) { }
    }
}
