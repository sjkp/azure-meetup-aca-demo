param appName string
param location string

// create the azure container registry
resource acr 'Microsoft.ContainerRegistry/registries@2021-09-01' = {
  name: toLower('${appName}acr')
  location: location
  sku: {
    name: 'Basic'
  }
  properties: {
    adminUserEnabled: true
  }
}


output password string =  acr.listCredentials().passwords[0].value
output user string =  acr.listCredentials().username
