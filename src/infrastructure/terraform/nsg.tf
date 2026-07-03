resource "azurerm_network_security_group" "web-nsg" {
  name                = "${local.service_prefix}-web-nsg"
  resource_group_name = azurerm_resource_group.web-rg.name
  location            = local.location

  # Compliant: no inbound from Internet; add only specific allows if required (e.g., from corporate IPs/VNet)
  security_rule {
    name                       = "Allow-HTTPS-From-VNet"
    priority                   = 200
    direction                  = "Inbound"
    access                     = "Allow"
    protocol                   = "Tcp"
    source_address_prefix      = "VirtualNetwork"
    destination_address_prefix = "VirtualNetwork"
    destination_port_ranges    = ["443"]
    source_port_range          = "*"
  }

  security_rule {
    name                       = "Allow_Outbound_Integration"
    priority                   = 200
    direction                  = "Outbound"
    access                     = "Allow"
    protocol                   = "Tcp"
    source_port_range          = "*"
    destination_port_range     = "443"
    source_address_prefix      = "VirtualNetwork"
    destination_address_prefix = "Internet"
  }

  security_rule {
    name                       = "Deny_Internet_Outbound"
    priority                   = 1000
    direction                  = "Outbound"
    access                     = "Deny"
    protocol                   = "*"
    source_port_range          = "*"
    destination_port_range     = "*"
    source_address_prefix      = "VirtualNetwork"
    destination_address_prefix = "Internet"
  }

  tags = local.common_tags
}

resource "azurerm_network_security_group" "private-endpoint-nsg" {
  name                = "${local.service_prefix}-pe-nsg"
  resource_group_name = azurerm_resource_group.web-rg.name
  location            = local.location

  security_rule {
    name                       = "Deny_Internet_Outbound"
    priority                   = 1000
    direction                  = "Outbound"
    access                     = "Deny"
    protocol                   = "*"
    source_port_range          = "*"
    destination_port_range     = "*"
    source_address_prefix      = "VirtualNetwork"
    destination_address_prefix = "Internet"
  }

  tags = local.common_tags
}

resource "azurerm_network_security_group" "redis-nsg" {
  count               = lower(var.caching_type) == "redis" ? 1 : 0
  name                = "${local.service_prefix}-redis-nsg"
  resource_group_name = azurerm_resource_group.redis-rg[0].name
  location            = local.location

  security_rule {
    name                       = "Deny_Internet_Outbound"
    priority                   = 1000
    direction                  = "Outbound"
    access                     = "Deny"
    protocol                   = "*"
    source_port_range          = "*"
    destination_port_range     = "*"
    source_address_prefix      = "VirtualNetwork"
    destination_address_prefix = "Internet"
  }

  tags = local.common_tags
}
