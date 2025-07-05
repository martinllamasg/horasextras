
param(
    [Parameter(Mandatory = $true)]
    [string]$AppName
)

# Obtener el SP
$sp = az ad sp list --display-name $AppName --query "[0].appId" -o tsv

if (-not $sp) {
    Write-Output "❌ No se encontró ningún Service Principal con el nombre '$AppName'."
    exit 1
}

Write-Output "🔍 Encontrado AppId: $sp"

# Eliminar asignaciones de rol
Write-Output "⛔ Revocando asignaciones de rol..."
az role assignment delete --assignee $sp

# Eliminar el Service Principal
Write-Output "🗑 Eliminando el Service Principal..."
az ad sp delete --id $sp

Write-Output "`n✅ El Service Principal '$AppName' ha sido revocado completamente."
