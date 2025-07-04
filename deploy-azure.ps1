
# Variables
$resourceGroup = "rg-horasextras"
$location = "eastus"
$templateFile = "main.bicep"
$adminPassword = $env:SQL_ADMIN_PASSWORD

# Login con el Service Principal
az login --service-principal -u $env:AZURE_CLIENT_ID -p $env:AZURE_CLIENT_SECRET --tenant $env:AZURE_TENANT_ID

# Crear grupo de recursos
az group create --name $resourceGroup --location $location

# Desplegar recursos desde Bicep
az deployment group create `
  --resource-group $resourceGroup `
  --template-file $templateFile `
  --parameters adminPassword=$adminPassword
