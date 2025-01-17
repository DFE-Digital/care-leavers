resource "azurerm_resource_group" "core-rg" {
  name     = "${local.service_prefix}-core-rg"
  location = local.location
  tags     = local.common_tags
}

resource "azurerm_log_analytics_workspace" "log-analytics-workspace" {
  name                = "${local.service_prefix}-log-analytics-workspace"
  location            = azurerm_resource_group.core-rg.location
  resource_group_name = azurerm_resource_group.core-rg.name
  retention_in_days   = 180
  tags                = local.common_tags
}

resource "azurerm_application_insights" "application-insights" {
  name                = "${local.service_prefix}-app-insights"
  location            = azurerm_resource_group.core-rg.location
  resource_group_name = azurerm_resource_group.core-rg.name
  application_type    = "web"
  workspace_id        = azurerm_log_analytics_workspace.log-analytics-workspace.id
  tags                = local.common_tags
}

resource "azurerm_key_vault_secret" "application-insights-connection-string" {
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "application-insights-connection-string"
  value        = azurerm_application_insights.application-insights.connection_string
}