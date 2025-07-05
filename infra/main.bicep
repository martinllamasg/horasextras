
resource storageAccount 'Microsoft.Storage/storageAccounts@2022-09-01' = {
  name: 'overtimedeploytest${uniqueString(resourceGroup().id)}'
  location: 'eastus'
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
  properties: {}
}
