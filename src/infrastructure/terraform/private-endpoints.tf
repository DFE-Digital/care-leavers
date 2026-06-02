resource "azurerm_private_endpoint" "web-storage-private-endpoint" {
  name                = "${local.service_prefix}-webapp-pe"
  resource_group_name = azurerm_resource_group.web-rg.name
  location            = local.location
  subnet_id           = azapi_resource.private-endpoint-subnet.id

  private_service_connection {
    name                           = "${local.service_prefix}-web-storage-psc"
    is_manual_connection           = false
    private_connection_resource_id = azurerm_storage_account.web_storage_account.id
    subresource_names              = ["blob"]
  }
  private_dns_zone_group {
    name                 = "${local.service_prefix}-web-storage-dns-group"
    private_dns_zone_ids = [azurerm_private_dns_zone.web-sa-dns-zone.id]
  }

  tags = local.common_tags
}

resource "azurerm_private_dns_zone" "web-sa-dns-zone" {
  name                = "${local.service_prefix}-privatelink.blob.core.windows.net"
  resource_group_name = azurerm_resource_group.web-rg.name

  tags = local.common_tags

  depends_on = [
    azurerm_storage_account.web_storage_account
  ]
}

resource "azurerm_private_dns_zone_virtual_network_link" "web-sa-dns-link" {
  name                  = "${local.service_prefix}-web-sa-dns-link"
  resource_group_name   = azurerm_resource_group.web-rg.name
  private_dns_zone_name = azurerm_private_dns_zone.web-sa-dns-zone.name
  virtual_network_id    = azurerm_virtual_network.careleavers-web-vnet.id

  tags = local.common_tags
}