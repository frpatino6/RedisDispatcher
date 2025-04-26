using MediatR;
using RedisDispatcher.Domain.Interfaces;

namespace RedisDispatcher.Infrastructure.Services
{
    public class UtilSycSecretProvider : ISecretProvider
    {
        public Dictionary<string, string> GetSecrets(string clientId)
        {
            var secrets = UtilSyc.Secretos.Utilidades.obtenerSecretos(clientId, UtilSyc.Secretos.Utilidades.TipoConexion.Desarrollo);

            return secrets;
        }
    }
}
