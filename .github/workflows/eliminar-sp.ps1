
param(
    [string]$spName = "gh-deploy-horasextras"
)

Write-Host "🔐 Revocando y eliminando Service Principal: $spName"

$sp = az ad sp list --display-name $spName --query "[0].appId" -o tsv

if (!$sp) {
    Write-Host "❌ No se encontró ningún Service Principal con ese nombre."
    exit 1
}

az ad sp delete --id $sp
Write-Host "✅ Service Principal eliminado exitosamente."
