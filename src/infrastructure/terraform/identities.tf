resource "azurerm_user_assigned_identity" "cl-identity" {
  name                = "${local.service_prefix}mid-uks-cl"
  location            = local.location
  resource_group_name = azurerm_resource_group.core-rg.name

  tags = local.common_tags
}