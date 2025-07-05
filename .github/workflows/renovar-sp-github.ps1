
param(
    [Parameter(Mandatory = $true)]
    [string]$AppName,

    [Parameter(Mandatory = $false)]
    [int]$Years = 1
)

# Obtener el AppId
$appId = az ad sp list --display-name $AppName --query "[0].appId" -o tsv
if (-not $appId) {
    Write-Error "❌ No se encontró ningún SP con el nombre '$AppName'"
    exit 1
}

Write-Host "🔁 Renovando secret para AppId: $appId..."

# Crear nuevo secreto
$secret = az ad sp credential reset --name $appId --years $Years --only-show-errors

if (-not $secret) {
    Write-Error "❌ Falló la renovación del secreto"
    exit 1
}

# Obtener información adicional
$tenantId = az account show --query tenantId -o tsv
$subscriptionId = az account show --query id -o tsv

# Crear objeto JSON estilo GitHub
$githubJson = @{
    clientId = $secret.appId
    clientSecret = $secret.password
    subscriptionId = $subscriptionId
    tenantId = $tenantId
    activeDirectoryEndpointUrl = "https://login.microsoftonline.com"
    resourceManagerEndpointUrl = "https://management.azure.com/"
    activeDirectoryGraphResourceId = "https://graph.windows.net/"
    sqlManagementEndpointUrl = "https://management.core.windows.net:8443/"
    galleryEndpointUrl = "https://gallery.azure.com/"
    managementEndpointUrl = "https://management.core.windows.net/"
} | ConvertTo-Json -Depth 3

Write-Output "`n✅ Copia el siguiente JSON en GitHub como secreto 'AZURE_CREDENTIALS':`n"
Write-Output $githubJson
