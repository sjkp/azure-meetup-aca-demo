param name string

resource comm 'Microsoft.Communication/communicationServices@2020-08-20' = {
  name: 'cs-${name}'
  properties: {
    dataLocation: 'Europe'
  }
}

resource email 'Microsoft.Communication/emailServices@2021-10-01-preview' = {
  name: 'email-${name}'
  properties: {
    dataLocation: 'United States'
  }
}
