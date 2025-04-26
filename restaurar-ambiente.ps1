# Inicia OpenShift Local (CRC)
Write-Host "🔄 Iniciando CRC..."
crc start

# Espera a que esté completamente arriba (opcional: agregar validación de estado)
Start-Sleep -Seconds 10

# Cargar variables de entorno de oc (solo si estás en PowerShell)
Write-Host "🔐 Configurando entorno para oc..."
crc oc-env | Invoke-Expression

# Loguearse en OpenShift
Write-Host "🔑 Autenticando en OpenShift..."
oc login -u developer -p developer https://api.crc.testing:6443

# Cambiar al namespace del proyecto
Write-Host "📦 Cambiando al proyecto redis-syc-project..."
oc project redis-syc-project

# Verificar estado de pods
Write-Host "📡 Estado actual de los pods:"
oc get pods

# Probar conexión al endpoint principal
Write-Host "🌐 Probar request a la API REST:"
curl -k https://redis-dispatcher-route-redis-syc-project.apps-crc.testing/api/data/acme/acme
