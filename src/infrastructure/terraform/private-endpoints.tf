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

  lifecycle {
    ignore_changes = [
      tags
    ]
  }
}

resource "azurerm_private_dns_zone" "web-sa-dns-zone" {
  name                = "privatelink.blob.core.windows.net"
  resource_group_name = azurerm_resource_group.web-rg.name

  lifecycle {
    ignore_changes = [
      tags
    ]
  }

  depends_on = [
    azurerm_storage_account.web_storage_account,
    azapi_resource.web-subnet,
    azapi_resource.private-endpoint-subnet
  ]
}

resource "azurerm_private_dns_zone_virtual_network_link" "web-sa-dns-link" {
  name                  = "${local.service_prefix}-web-sa-dns-link"
  resource_group_name   = azurerm_resource_group.web-rg.name
  private_dns_zone_name = azurerm_private_dns_zone.web-sa-dns-zone.name
  virtual_network_id    = azurerm_virtual_network.careleavers-web-vnet.id

  lifecycle {
    ignore_changes = [
      tags
    ]
  }

  depends_on = [
    azapi_resource.web-subnet,
    azapi_resource.private-endpoint-subnet
  ]
}

resource "azurerm_private_endpoint" "translation-private-endpoint" {
  name                = "${local.service_prefix}-translation-pe"
  resource_group_name = azurerm_resource_group.translator-rg.name
  location            = local.location
  subnet_id           = azapi_resource.private-endpoint-subnet.id

  private_service_connection {
    name                           = "${local.service_prefix}-translation-psc"
    is_manual_connection           = false
    private_connection_resource_id = azurerm_cognitive_account.ai-translator.id
    subresource_names              = ["account"]
  }
  private_dns_zone_group {
    name                 = "${local.service_prefix}-translation-dns-group"
    private_dns_zone_ids = [azurerm_private_dns_zone.translator-dns-zone.id]
  }

  lifecycle {
    ignore_changes = [
      tags
    ]
  }
}

resource "azurerm_private_dns_zone" "translator-dns-zone" {
  name                = "privatelink.cognitiveservices.azure.com"
  resource_group_name = azurerm_resource_group.translator-rg.name

  lifecycle {
    ignore_changes = [
      tags
    ]
  }

  depends_on = [
    azurerm_cognitive_account.ai-translator,
    azapi_resource.web-subnet,
    azapi_resource.private-endpoint-subnet
  ]
}

resource "azurerm_private_dns_zone_virtual_network_link" "translator-dns-link" {
  name                  = "${local.service_prefix}-translator-dns-link"
  resource_group_name   = azurerm_resource_group.translator-rg.name
  private_dns_zone_name = azurerm_private_dns_zone.translator-dns-zone.name
  virtual_network_id    = azurerm_virtual_network.careleavers-web-vnet.id

  lifecycle {
    ignore_changes = [
      tags
    ]
  }

  depends_on = [
    azapi_resource.web-subnet,
    azapi_resource.private-endpoint-subnet
  ]
}

resource "azurerm_private_endpoint" "kv-private-endpoint" {
  name                = "${local.service_prefix}-kv-pe"
  resource_group_name = azurerm_resource_group.web-rg.name
  location            = local.location
  subnet_id           = azapi_resource.private-endpoint-subnet.id

  private_service_connection {
    name                           = "${local.service_prefix}-kv-psc"
    is_manual_connection           = false
    private_connection_resource_id = azurerm_key_vault.kv.id
    subresource_names              = ["vault"]
  }
  private_dns_zone_group {
    name                 = "${local.service_prefix}-kv-dns-group"
    private_dns_zone_ids = [azurerm_private_dns_zone.kv-dns-zone.id]
  }

  lifecycle {
    ignore_changes = [
      tags
    ]
  }
}

resource "azurerm_private_dns_zone" "kv-dns-zone" {
  name                = "privatelink.vaultcore.azure.net"
  resource_group_name = azurerm_resource_group.web-rg.name

  lifecycle {
    ignore_changes = [
      tags
    ]
  }

  depends_on = [
    azurerm_key_vault.kv,
    azapi_resource.web-subnet,
    azapi_resource.private-endpoint-subnet
  ]
}

resource "azurerm_private_dns_zone_virtual_network_link" "kv-dns-link" {
  name                  = "${local.service_prefix}-kv-dns-link"
  resource_group_name   = azurerm_resource_group.web-rg.name
  private_dns_zone_name = azurerm_private_dns_zone.kv-dns-zone.name
  virtual_network_id    = azurerm_virtual_network.careleavers-web-vnet.id

  lifecycle {
    ignore_changes = [
      tags
    ]
  }

  depends_on = [
    azapi_resource.web-subnet,
    azapi_resource.private-endpoint-subnet
  ]
}

resource "azurerm_private_endpoint" "redis-private-endpoint" {
  count               = lower(var.caching_type) == "redis" ? 1 : 0
  name                = "${local.service_prefix}-redis-pe"
  resource_group_name = azurerm_resource_group.redis-rg[0].name
  location            = local.location
  subnet_id           = azapi_resource.private-endpoint-subnet.id

  private_service_connection {
    name                           = "${local.service_prefix}-redis-psc"
    is_manual_connection           = false
    private_connection_resource_id = azurerm_managed_redis.redis-enterprise[0].id
    subresource_names              = ["redisEnterprise"]
  }
  private_dns_zone_group {
    name                 = "${local.service_prefix}-redis-dns-group"
    private_dns_zone_ids = [azurerm_private_dns_zone.redis-dns-zone[0].id]
  }

  lifecycle {
    ignore_changes = [
      tags
    ]
  }
}

resource "azurerm_private_dns_zone" "redis-dns-zone" {
  count               = lower(var.caching_type) == "redis" ? 1 : 0
  name                = "privatelink.redisenterprise.azure.net"
  resource_group_name = azurerm_resource_group.redis-rg[0].name

  lifecycle {
    ignore_changes = [
      tags
    ]
  }

  depends_on = [
    azurerm_managed_redis.redis-enterprise,
    azapi_resource.web-subnet,
    azapi_resource.private-endpoint-subnet
  ]
}

resource "azurerm_private_dns_zone_virtual_network_link" "redis-dns-link" {
  count                 = lower(var.caching_type) == "redis" ? 1 : 0
  name                  = "${local.service_prefix}-redis-dns-link"
  resource_group_name   = azurerm_resource_group.redis-rg[0].name
  private_dns_zone_name = azurerm_private_dns_zone.redis-dns-zone[0].name
  virtual_network_id    = azurerm_virtual_network.careleavers-web-vnet.id

  lifecycle {
    ignore_changes = [
      tags
    ]
  }

  depends_on = [
    azapi_resource.web-subnet,
    azapi_resource.private-endpoint-subnet
  ]
}