#https://docs.microsoft.com/en-us/powershell/azure/authenticate-azureps?view=azps-5.7.0
Connect-AzAccount


#https://docs.microsoft.com/en-us/azure/azure-resource-manager/templates/deploy-powershell
New-AzResourceGroupDeployment -Name DemoDeployment -ResourceGroupName rg-or-az204 `
  -TemplateFile "D:\Google Drive\02-O'Reilly\07-AZ-204\05-demos\VM\template.json" `
  -TemplateParameterFile "D:\Google Drive\02-O'Reilly\07-AZ-204\05-demos\VM\parameters.json"