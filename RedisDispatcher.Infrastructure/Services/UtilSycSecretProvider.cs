using MediatR;
using RedisDispatcher.Domain.Interfaces;

namespace RedisDispatcher.Infrastructure.Services
{
    public class UtilSycSecretProvider : ISecretProvider
    {
        public Dictionary<string, string> GetSecrets(string clientId, int environment)
        {
            var tipoConexion = MapEnvironmentToTipoConexion(environment);
            var secrets = UtilSyc.Secretos.Utilidades.obtenerSecretos(clientId, tipoConexion);
            return secrets;
        }

        private UtilSyc.Secretos.Utilidades.TipoConexion MapEnvironmentToTipoConexion(int environment)
        {
            return environment switch
            {
                0 => UtilSyc.Secretos.Utilidades.TipoConexion.Desarrollo,
                1 => UtilSyc.Secretos.Utilidades.TipoConexion.Pruebas,
                2 => UtilSyc.Secretos.Utilidades.TipoConexion.Produccion,
                9 => UtilSyc.Secretos.Utilidades.TipoConexion.QA,
                _ => throw new ArgumentOutOfRangeException(nameof(environment), $"Valor inválido para ambiente: {environment}")
            };
        }
    }

}
