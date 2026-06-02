resource "azurerm_virtual_network" "careleavers-web-vnet" {
  name                = "${local.service_prefix}-web-vnet"
  resource_group_name = azurerm_resource_group.web-rg.name
  location            = local.location
  address_space       = ["10.0.0.0/16"]

  tags = local.common_tags
}

resource "azurerm_subnet" "web-subnet" {
  name                 = "${local.service_prefix}-webapp-subnet"
  resource_group_name  = azurerm_resource_group.web-rg.name
  virtual_network_name = azurerm_virtual_network.careleavers-web-vnet.name
  address_prefixes     = ["10.0.1.0/24"]

  service_endpoints = ["Microsoft.Storage", "Microsoft.Web"]

  delegation {
    name = "delegation"
    service_delegation {
      name = "Microsoft.Web/serverFarms"
      actions = [
        "Microsoft.Network/virtualNetworks/subnets/join/action"
      ]
    }
  }
}

resource "azurerm_subnet" "private-endpoint-subnet" {
  name                 = "${local.service_prefix}-private-endpoint-subnet"
  resource_group_name  = azurerm_resource_group.web-rg.name
  virtual_network_name = azurerm_virtual_network.careleavers-web-vnet.name
  address_prefixes     = ["10.0.2.0/24"]

  private_endpoint_network_policies = "NetworkSecurityGroupEnabled"
}
