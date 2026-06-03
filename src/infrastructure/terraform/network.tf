resource "azurerm_virtual_network" "careleavers-web-vnet" {
  name                = "${local.service_prefix}-web-vnet"
  resource_group_name = azurerm_resource_group.web-rg.name
  location            = local.location
  address_space       = ["10.0.0.0/16"]

  tags = local.common_tags
}

resource "azapi_resource" "web-subnet" {
  type      = "Microsoft.Network/virtualNetworks/subnets@2024-05-01"
  name      = "${local.service_prefix}-webapp-subnet"
  parent_id = azurerm_virtual_network.careleavers-web-vnet.id

  body = {
    properties = {
      addressPrefixes = ["10.0.1.0/24"]
      delegations = [{
        name = "asp-delegation"
        properties = {
          serviceName = "Microsoft.Web/serverFarms"
        }
      }]
      networkSecurityGroup = {
        id = azurerm_network_security_group.web-nsg.id
      }
    }
  }

  depends_on = [
    azurerm_network_security_group.web-nsg,
    azurerm_virtual_network.careleavers-web-vnet
  ]
}

resource "azapi_resource" "private-endpoint-subnet" {
  type      = "Microsoft.Network/virtualNetworks/subnets@2024-05-01"
  name      = "${local.service_prefix}-private-endpoint-subnet"
  parent_id = azurerm_virtual_network.careleavers-web-vnet.id

  body = {
    properties = {
      addressPrefixes = ["10.0.2.0/24"]
      networkSecurityGroup = {
        id = azurerm_network_security_group.private-endpoint-nsg.id
      }
      privateEndpointNetworkPolicies = "NetworkSecurityGroupEnabled"
    }
  }

  depends_on = [
    azurerm_network_security_group.private-endpoint-nsg,
    azurerm_virtual_network.careleavers-web-vnet
  ]
}
