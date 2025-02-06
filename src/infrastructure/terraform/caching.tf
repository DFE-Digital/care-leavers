resource "azurerm_resource_group" "caching-rg" {
  count    = lower(var.caching_type) == "redis" ? 1 : 0
  name     = "${local.service_prefix}-caching-rg"
  location = local.location
  tags     = local.common_tags
}

resource "azurerm_redis_cache" "redis-cache" {
  count               = lower(var.caching_type) == "redis" ? 1 : 0
  capacity            = 2
  family              = "C"
  location            = local.location
  name                = "${local.service_prefix}-redis-cache"
  resource_group_name = azurerm_resource_group.caching-rg[0].name
  sku_name            = "Standard"
  minimum_tls_version = "1.2"

  tags = local.common_tags
}

resource "azurerm_key_vault_secret" "redis-cache-connection-string" {
  count        = lower(var.caching_type) == "redis" ? 1 : 0
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "redis-cache-connection-string"
  value        = azurerm_redis_cache.redis-cache[0].primary_connection_string
}