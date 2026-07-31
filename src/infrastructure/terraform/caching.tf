resource "azurerm_resource_group" "redis-rg" {
  count    = lower(var.caching_type) == "redis" ? 1 : 0
  name     = "${local.prefix}rg-uks-cl-redis-rg"
  location = local.location
  tags     = local.common_tags
}

resource "azurerm_managed_redis" "redis-enterprise" {
  #checkov:skip=DFE_CMN_0001: Tags applied by policy rather than set to prevent overriding
  #checkov:skip=DFE_CMN_0002: Tags applied by policy rather than set to prevent overriding
  #checkov:skip=DFE_CMN_0003: Tags applied by policy rather than set to prevent overriding
  count                     = lower(var.caching_type) == "redis" ? 1 : 0
  name                      = "${local.service_prefix}-redis"
  location                  = local.location
  resource_group_name       = azurerm_resource_group.redis-rg[0].name
  sku_name                  = "Balanced_B0"
  high_availability_enabled = false

  default_database {
    access_keys_authentication_enabled = true
  }

  public_network_access = "Disabled"

  tags = local.common_tags

  lifecycle {
    ignore_changes = [
      # Ignore changes to the 'tags' attribute
      tags,
    ]
  }
}