
# Variables necesarias
$spName = "github-horasextras"
$subscriptionId = "<TU_SUBSCRIPTION_ID>"  # <-- Sustituye por tu ID real

# Obtener el AppId del Service Principal
$sp = az ad sp list --display-name $spName --query "[0].appId" -o tsv

# Asignar rol 'Contributor' si no está asignado
az role assignment create --assignee $sp --role "Contributor" --scope "/subscriptions/$subscriptionId"

# Asignar rol 'Network Contributor' para redes y endpoints privados
az role assignment create --assignee $sp --role "Network Contributor" --scope "/subscriptions/$subscriptionId"

# Asignar rol 'Private DNS Zone Contributor' para administrar zonas DNS privadas
az role assignment create --assignee $sp --role "Private DNS Zone Contributor" --scope "/subscriptions/$subscriptionId"

Write-Output "✅ Roles 'Contributor', 'Network Contributor' y 'Private DNS Zone Contributor' asignados correctamente al SP '$spName'"
