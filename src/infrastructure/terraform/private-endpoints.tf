resource "azurerm_private_endpoint" "webapp-private-endpoint" {
  name                = "${local.service_prefix}-webapp-pe"
  resource_group_name = azurerm_resource_group.web-rg.name
  location            = local.location
  subnet_id           = azurerm_subnet.private-endpoint-subnet.id

  private_service_connection {
    name                           = "${local.service_prefix}-webapp-psc"
    is_manual_connection           = false
    private_connection_resource_id = azurerm_storage_account.web_storage_account.id
    subresource_names              = ["sites"]
  }
}