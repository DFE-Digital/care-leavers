# resource "azurerm_resource_group" "caching-rg" {
#   count    = lower(var.caching_type) == "redis" ? 1 : 0
#   name     = "${local.prefix}rg-uks-cl-caching"
#   location = local.location
#   tags     = local.common_tags
# }

# resource "azurerm_redis_cache" "redis-cache" {
#   count               = lower(var.caching_type) == "redis" ? 1 : 0
#   capacity            = 2
#   family              = "C"
#   location            = local.location
#   name                = "${local.service_prefix}-redis-cache"
#   resource_group_name = azurerm_resource_group.caching-rg[0].name
#   sku_name            = "Standard"
#   minimum_tls_version = "1.2"

#   tags = local.common_tags
# }

# resource "azurerm_key_vault_secret" "redis-cache-connection-string" {
#   count        = lower(var.caching_type) == "redis" ? 1 : 0
#   key_vault_id = azurerm_key_vault.key-vault.id
#   name         = "redis-cache-connection-string"
#   value        = azurerm_redis_cache.redis-cache[0].primary_connection_string
# }

resource "azurerm_resource_group" "redis-rg" {
  count    = lower(var.caching_type) == "redis" ? 1 : 0
  name     = "${local.prefix}rg-uks-cl-redis-rg"
  location = local.location
  tags     = local.common_tags
}

resource "azurerm_managed_redis" "redis-enterprise" {
  count                     = lower(var.caching_type) == "redis" ? 1 : 0
  name                      = "${local.service_prefix}-redis"
  location                  = local.location
  resource_group_name       = azurerm_resource_group.redis-rg[0].name
  sku_name                  = "Balanced_B0"
  high_availability_enabled = false

  default_database {
    access_keys_authentication_enabled = true
  }

  tags = local.common_tags

  lifecycle {
    ignore_changes = [
      # Ignore changes to the 'tags' attribute
      tags,
    ]
  }
}

resource "azurerm_key_vault_secret" "redis-enterprise-connection-string" {
  count        = lower(var.caching_type) == "redis" ? 1 : 0
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "redis-enterprise-connection-string"
  value        = "${azurerm_managed_redis.redis-enterprise[0].hostname}:${azurerm_managed_redis.redis-enterprise[0].default_database[0].port},ssl=true,password=${azurerm_managed_redis.redis-enterprise[0].default_database[0].primary_access_key}"
}