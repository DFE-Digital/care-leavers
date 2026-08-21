locals {
  environment_character_limits = {
    d01 = 1000000
    t01 = 1000000
    p01 = 15000000
  }

  web_app_settings = {
    "ASPNETCORE_ENVIRONMENT"                = var.aspnetcore_environment
    "ContentfulOptions__DeliveryApiKey"     = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.contentful-delivery-api-key.versionless_id})"
    "ContentfulOptions__PreviewApiKey"      = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.contentful-preview-api-key.versionless_id})"
    "ContentfulOptions__ManagementApiKey"   = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.contentful-management-api-key.versionless_id})"
    "ContentfulOptions__SpaceId"            = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.contentful-space-id.versionless_id})"
    "ContentfulOptions__Environment"        = var.contentful_environment
    "ContentfulOptions__UsePreviewApi"      = var.contentful_use_preview_api
    "ApplicationInsights__ConnectionString" = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.application-insights-connection-string.versionless_id})"
    "Caching__Type"                         = var.caching_type
    "Caching__ConnectionString"             = lower(var.caching_type) == "redis" ? "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.redis-enterprise-connection-string[0].versionless_id})" : ""
    "Scripts__Clarity"                      = var.scripts_clarity
    "AzureTranslation__AccessKey"           = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.azure-translation-access-key.versionless_id})"
    "AzureTranslation__Endpoint"            = "https://${local.service_prefix}.cognitiveservices.azure.com"
    "AzureTranslation__CharacterLimit"      = local.environment_character_limits[var.environment_prefix]
    "BlobStorage__ConnectionString"         = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.blob-storage-connection-string.versionless_id})"
    "BlobStorage__ContainerName"            = azurerm_storage_container.translator_storage_container.name
    "Rebrand"                               = var.rebrand
    "GetToAnAnswer__BaseUrl"                = var.gtaa_base_url
    "BasicAuth__EncodedCreds"               = var.basic_auth_credentials
    "Scripts__GA4"                          = var.scripts_ga4
    "Scripts__GTM"                          = var.scripts_gtm
    "SPLUNK_PORT"                           = var.splunk_port
    "SPLUNK_REALM"                          = var.splunk_realm
    "SPLUNK_ACCESS_TOKEN"                   = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.splunk-access-token.versionless_id})"
    "SPLUNK_SERVICE_NAME"                   = "${local.prefix}-cl"
    "OTEL_EXPORTER_OTLP_ENDPOINT"           = "http://localhost:4318"
    "OTEL_EXPORTER_OTLP_PROTOCOL"           = "http/protobuf"
    "OTEL_SERVICE_NAME"                     = "${local.prefix}-cl"
    "OTEL_RESOURCE_ATTRIBUTES"              = "deployment.environment=${var.elz_environment},service.version=1.0.0"
  }

  managed_identity = {
    type = "UserAssigned"
    identity_ids = [
      azurerm_user_assigned_identity.cl-identity-reader.id
    ]
  }
}

resource "azurerm_resource_group" "web-rg" {
  name     = "${local.prefix}rg-uks-cl-web"
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

  #checkov:skip=CKV_AZURE_212: Single instance is sufficient for this service
  #checkov:skip=CKV_AZURE_225: Zone redundancy not required for this service
}

resource "azurerm_linux_web_app_slot" "web-app-service-staging" {
  count = var.elz_environment == "Prod" ? 1 : 0

  app_service_id = azurerm_linux_web_app.web-app-service.id
  name           = "staging"
  https_only     = true

  site_config {
    always_on = true

    ip_restriction_default_action = "Deny"

    ip_restriction {
      name        = "Access from Front Door"
      service_tag = "AzureFrontDoor.Backend"
    }

    health_check_path                 = "/health"
    health_check_eviction_time_in_min = 5

    minimum_tls_version     = "1.3"
    scm_minimum_tls_version = "1.3"
  }

  identity {
    type         = local.managed_identity.type
    identity_ids = local.managed_identity.identity_ids
  }

  key_vault_reference_identity_id = local.managed_identity.identity_ids[0]

  app_settings = local.web_app_settings

  tags = local.common_tags
}

resource "azurerm_linux_web_app" "web-app-service" {
  service_plan_id     = azurerm_service_plan.web-app-service-plan.id
  location            = local.location
  name                = "${local.service_prefix}-web-app-service"
  resource_group_name = azurerm_resource_group.web-rg.name
  https_only          = true

  virtual_network_subnet_id = azapi_resource.web-subnet.id

  site_config {
    always_on = true

    ip_restriction_default_action = "Deny"
    ftps_state                    = "Disabled"
    http2_enabled                 = true
    remote_debugging_enabled      = false
    minimum_tls_version           = "1.2"

    ip_restriction {
      name        = "Access from Front Door"
      service_tag = "AzureFrontDoor.Backend"
    }

    health_check_path                 = "/health"
    health_check_eviction_time_in_min = 5
  }

  identity {
    type         = local.managed_identity.type
    identity_ids = local.managed_identity.identity_ids
  }

  key_vault_reference_identity_id = local.managed_identity.identity_ids[0]

  app_settings = local.web_app_settings

  tags = local.common_tags

  #checkov:skip=CKV_AZURE_88: Application is completely stateless
  #checkov:skip=CKV_AZURE_17: Standalone app with no dependencies on other Azure services
  #checkov:skip=CKV_AZURE_13: Authentication is handled by .net code base
  #checkov:skip=CKV_AZURE_63: Public API protected by Azure Front Door
  #checkov:skip=CKV_AZURE_222: Public network access is only enabled for Front Door
  #checkov:skip=CKV_AZURE_65: Detailed errors are captured via Application Insights
  #checkov:skip=CKV_AZURE_66: Inbound HTTP access logging is captured and centralised at the Azure Front Door WAF layer
}

resource "azurerm_monitor_diagnostic_setting" "webapp_logs" {
  name                       = "${var.environment_prefix}-web-app-diagnostics"
  target_resource_id         = azurerm_linux_web_app.web-app-service.id
  log_analytics_workspace_id = azurerm_log_analytics_workspace.log-analytics-workspace.id

  # Capture common runtime and diagnostic logs
  enabled_log {
    category = "AppServiceConsoleLogs"
  }

  enabled_log {
    category = "AppServiceHTTPLogs"
  }

  enabled_log {
    category = "AppServicePlatformLogs"
  }

  enabled_log {
    category = "AppServiceAppLogs"
  }

  # All Metrics
  enabled_metric {
    category = "AllMetrics"
  }
}

resource "azurerm_storage_account" "web_storage_account" {
  #checkov:skip=CKV2_AZURE_1: Do not need to use CMK
  #checkov:skip=CKV2_AZURE_18: Do not need to use CMK
  name                     = "${local.prefix}webstorage"
  resource_group_name      = azurerm_resource_group.web-rg.name
  location                 = local.location
  account_tier             = "Standard"
  account_replication_type = "LRS"

  public_network_access_enabled   = false
  allow_nested_items_to_be_public = false
  https_traffic_only_enabled      = true

  min_tls_version = "TLS1_2"

  tags = local.common_tags

  identity {
    type = "SystemAssigned"
  }

  blob_properties {
    delete_retention_policy { days = 30 }
    container_delete_retention_policy { days = 30 }
  }

  #checkov:skip=CKV_AZURE_33: Queue service is not utilized on this storage account.
  #checkov:skip=CKV_AZURE_35: Public network access is explicitly disabled via public_network_access_enabled = false
  #checkov:skip=CKV2_AZURE_8: Container access type is private, set within other resources
  #checkov:skip=CKV_AZURE_206: Data residency mandate restricts all data to UKSouth region (GRS/geo-replication not permitted).
  #checkov:skip=CKV2_AZURE_40: Business constraint.
  #checkov:skip=CKV2_AZURE_41: Business constraint.
}

resource "azurerm_storage_container" "translator_storage_container" {
  #checkov:skip=CKV2_AZURE_8: Does not look at container access type in this version of checkov - will update with image longer term
  name                  = "${local.service_prefix}-char-container"
  storage_account_id    = azurerm_storage_account.web_storage_account.id
  container_access_type = "private"

  #checkov:skip=CKV2_AZURE_21: Logging omitted to minimize ingestion costs on non-sensitive data; network controls mitigate access risk.
}

resource "azurerm_storage_container" "backup_storage_container" {
  #checkov:skip=CKV2_AZURE_8: Does not look at container access type in this version of checkov - will update with image longer term
  count = var.elz_environment == "Dev" ? 1 : 0

  name                  = "${local.service_prefix}-backup-container"
  storage_account_id    = azurerm_storage_account.web_storage_account.id
  container_access_type = "private"

  #checkov:skip=CKV2_AZURE_21: Logging omitted to minimize ingestion costs on non-sensitive data; network controls mitigate access risk.
}

resource "azurerm_storage_management_policy" "backup_storage_policy" {
  count = var.elz_environment == "Dev" ? 1 : 0

  storage_account_id = azurerm_storage_account.web_storage_account.id
  rule {
    name    = "delete-backups-after-14-days"
    enabled = true
    filters {
      prefix_match = ["${local.service_prefix}-backup-container"]
      blob_types   = ["blockBlob"]
    }
    actions {
      base_blob {
        delete_after_days_since_creation_greater_than = 14
      }
    }
  }
}
