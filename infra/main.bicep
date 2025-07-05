param location string = 'eastus'
param environment string = 'prod'

var rgName = 'rg-horasextras'
var vnetName = 'vnet-${environment}'
var subnetName = 'snet-${environment}'
var webAppName = 'webapp-horasextras'
var appServicePlanName = 'asp-horasextras'
var sqlServerName = 'sqlhorasextras${uniqueString(resourceGroup().id)}'
var sqlDbName = 'OvertimeDB'

resource vnet 'Microsoft.Network/virtualNetworks@2023-02-01' = {
  name: vnetName
  location: location
  properties: {
    addressSpace: {
      addressPrefixes: [
        '10.0.0.0/16'
      ]
    }
    subnets: [
      {
        name: subnetName
        properties: {
          addressPrefix: '10.0.0.0/24'
        }
      }
    ]
  }
}

resource appServicePlan 'Microsoft.Web/serverfarms@2023-01-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: 'P1v2'
    tier: 'PremiumV2'
    size: 'P1v2'
    capacity: 1
  }
  properties: {
    reserved: false
  }
}

resource webApp 'Microsoft.Web/sites@2023-01-01' = {
  name: webAppName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      linuxFxVersion: 'DOTNETCORE|8.0'
    }
  }
  kind: 'app,linux'
}

resource sqlServer 'Microsoft.Sql/servers@2022-11-01' = {
  name: sqlServerName
  location: location
  properties: {
    administratorLogin: 'sqladminuser'
    administratorLoginPassword: 'P@ssw0rd1234!'
  }
}

resource sqlDb 'Microsoft.Sql/servers/databases@2022-11-01' = {
  name: '${sqlServer.name}/${sqlDbName}'
  properties: {
    collation: 'SQL_Latin1_General_CP1_CI_AS'
    maxSizeBytes: 2147483648
    sampleName: 'AdventureWorksLT'
    sku: {
      name: 'Basic'
      tier: 'Basic'
      capacity: 5
    }
  }
  dependsOn: [
    sqlServer
  ]
}

resource webAppSettings 'Microsoft.Web/sites/config@2022-03-01' = {
  name: '${webApp.name}/appsettings'
  properties: {
    'ConnectionStrings__DefaultConnection': 'Server=tcp:${sqlServer.name}.database.windows.net,1433;Initial Catalog=${sqlDbName};Persist Security Info=False;User ID=sqladminuser;Password=P@ssw0rd1234!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'
  }
  dependsOn: [
    webApp
    sqlDb
  ]
}
