resource "azurerm_security_group" "web-nsg" {
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

  tags = local.common_tags
}

resource "azurerm_subnet_network_security_group_association" "web-subnet-nsg-association" {
  subnet_id                 = azurerm_subnet.web-subnet.id
  network_security_group_id = azurerm_security_group.web-nsg.id
}