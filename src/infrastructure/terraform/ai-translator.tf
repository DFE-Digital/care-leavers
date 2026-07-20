resource "azurerm_resource_group" "translator-rg" {
  name     = "${local.prefix}rg-uks-cl-translator"
  location = local.location
  tags     = local.common_tags
}

resource "azurerm_cognitive_account" "ai-translator" {
  #checkov:skip=CKV_AZURE_134: Can't disable public access as keys are required for authentication presently
  #checkov:skip=CKV2_AZURE_22: Do not need to use CMK
  name                  = "${local.service_prefix}-ai-translation"
  location              = azurerm_resource_group.translator-rg.location
  resource_group_name   = azurerm_resource_group.translator-rg.name
  kind                  = "TextTranslation" # Specifies the Translator service
  sku_name              = "S1"
  tags                  = local.common_tags
  custom_subdomain_name = local.service_prefix
}

resource "azurerm_monitor_diagnostic_setting" "ai-translator-logs" {
  name                       = "${local.service_prefix}-ai-translator-diagnostics"
  target_resource_id         = azurerm_cognitive_account.ai-translator.id
  log_analytics_workspace_id = azurerm_log_analytics_workspace.log-analytics-workspace.id

  enabled_log {
    category = "Audit"
  }

  enabled_log {
    category = "RequestResponse"
  }

  enabled_metric {
    category = "AllMetrics"
  }
}
