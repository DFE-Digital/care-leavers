locals {
  web_app_settings = {
    "ASPNETCORE_ENVIRONMENT"                = var.aspnetcore_environment
    "ContentfulOptions__DeliveryApiKey"     = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.contentful-delivery-api-key.versionless_id})"
    "ContentfulOptions__PreviewApiKey"      = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.contentful-preview-api-key.versionless_id})"
    "ContentfulOptions__SpaceId"            = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.contentful-space-id.versionless_id})"
    "ContentfulOptions__Environment"        = var.contentful_environment
    "ContentfulOptions__Preview"            = var.contentful_preview
    "BaseUrl"                               = "https://${azurerm_cdn_frontdoor_endpoint.frontdoor-web-endpoint.host_name}"
    "ApplicationInsights__ConnectionString" = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.application-insights-connection-string.versionless_id})"
    "Caching__Type"                         = "None"
    "Caching__ConnectionString"             = lower(var.caching_type) == "redis" ? "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.redis-cache-connection-string[0].versionless_id})" : ""
    "Scripts__Clarity"                      = var.scripts_clarity,
    # "Translation__AzureApiKey"              = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.translation-access-key.versionless_id})"
  }
}

resource "azurerm_resource_group" "web-rg" {
  name     = "${local.service_prefix}-web-rg"
  location = local.location
  tags     = local.common_tags
}

resource "azurerm_service_plan" "web-app-service-plan" {
  location            = local.location
  name                = "${local.service_prefix}-web-app-service-plan"
  resource_group_name = azurerm_resource_group.web-rg.name
  os_type             = "Linux"
  sku_name            = "P0v3"

  tags = local.common_tags
}

resource "azurerm_linux_web_app_slot" "web-app-service-staging" {
  app_service_id                  = azurerm_linux_web_app.web-app-service.id
  name                            = "staging"
  key_vault_reference_identity_id = azurerm_user_assigned_identity.web-app-staging-identity.id

  site_config {
    always_on = true

    ip_restriction_default_action = "Deny"

    ip_restriction {
      name        = "Access from Front Door"
      service_tag = "AzureFrontDoor.Backend"
    }

    health_check_path                 = "/health"
    health_check_eviction_time_in_min = 5
  }

  identity {
    type = "UserAssigned"
    identity_ids = [
      azurerm_user_assigned_identity.web-app-staging-identity.id
    ]
  }

  app_settings = local.web_app_settings

  tags = local.common_tags
}

resource "azurerm_linux_web_app" "web-app-service" {
  service_plan_id                 = azurerm_service_plan.web-app-service-plan.id
  location                        = local.location
  name                            = "${local.service_prefix}-web-app-service"
  resource_group_name             = azurerm_resource_group.web-rg.name
  key_vault_reference_identity_id = azurerm_user_assigned_identity.web-app-identity.id
  https_only                      = true

  site_config {
    always_on = true

    ip_restriction_default_action = "Deny"

    ip_restriction {
      name        = "Access from Front Door"
      service_tag = "AzureFrontDoor.Backend"
    }

    health_check_path                 = "/health"
    health_check_eviction_time_in_min = 5
  }

  identity {
    type = "UserAssigned"
    identity_ids = [
      azurerm_user_assigned_identity.web-app-identity.id
    ]
  }

  app_settings = local.web_app_settings

  tags = local.common_tags
}

resource "azurerm_key_vault_secret" "contentful-delivery-api-key" {
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "contentful-delivery-api-key"
  value        = var.contentful_delivery_api_key
}

resource "azurerm_key_vault_secret" "contentful-preview-api-key" {
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "contentful-preview-api-key"
  value        = var.contentful_preview_api_key
}

resource "azurerm_key_vault_secret" "contentful-space-id" {
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "contentful-space-id"
  value        = var.contentful_space_id
}

resource "azurerm_user_assigned_identity" "web-app-identity" {
  location            = local.location
  name                = "${local.service_prefix}-web-app-identity"
  resource_group_name = azurerm_resource_group.web-rg.name
}

resource "azurerm_user_assigned_identity" "web-app-staging-identity" {
  location            = local.location
  name                = "${local.service_prefix}-web-app-staging-identity"
  resource_group_name = azurerm_resource_group.web-rg.name
}