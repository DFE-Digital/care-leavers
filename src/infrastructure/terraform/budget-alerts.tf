locals {
  environment_subscription_budgets = {
    d01 = 100
    t01 = 50
    p01 = 300
  }
  environment_translator_budgets = {
    d01 = 10
    t01 = 10
    p01 = 100
  }
}

resource "azurerm_monitor_action_group" "budget-alert-action-group" {
  name                = "${local.service_prefix}-ai-budget-alert-action-group"
  resource_group_name = azurerm_resource_group.core-rg.name
  short_name          = "ALERT"

  email_receiver {
    name          = "CLBudgetAlertEmail"
    email_address = var.support_alert_email
  }
}

# Budget for the entire subscription
data "azurerm_subscription" "current" {}

resource "azurerm_consumption_budget_subscription" "subscription-budget" {
  name            = "${local.service_prefix}-subscription-budget"
  subscription_id = data.azurerm_subscription.current.id
  amount          = local.environment_subscription_budgets[var.environment_prefix]
  time_grain      = "Monthly"

  time_period {
    # Start date must be the first of a month, end date defaults to +10 years when not specified
    start_date = "2026-05-01T00:00:00Z"
  }

  notification {
    enabled        = true
    threshold      = 80.0
    operator       = "GreaterThan"
    threshold_type = "Actual"
    contact_groups = [azurerm_monitor_action_group.budget-alert-action-group.id]
  }

  notification {
    enabled        = true
    threshold      = 100.0
    operator       = "GreaterThan"
    threshold_type = "Forecasted"
    contact_groups = [azurerm_monitor_action_group.budget-alert-action-group.id]
  }

  notification {
    enabled        = true
    threshold      = 110.0
    operator       = "GreaterThan"
    threshold_type = "Forecasted"
    contact_groups = [azurerm_monitor_action_group.budget-alert-action-group.id]
  }
}

# Budget alerts for AI translator services
resource "azurerm_consumption_budget_subscription" "translator-budget" {
  name            = "${local.service_prefix}-translator-budget"
  subscription_id = data.azurerm_subscription.current.id
  amount          = local.environment_translator_budgets[var.environment_prefix]
  time_grain      = "Monthly"

  time_period {
    # Start date must be the first of a month, end date defaults to +10 years when not specified
    start_date = "2026-05-01T00:00:00Z"
  }

  filter {
    dimension {
      name   = "ResourceType"
      values = ["microsoft.cognitiveservices/accounts"]
    }
  }

  notification {
    enabled        = true
    threshold      = 80.0
    operator       = "GreaterThan"
    threshold_type = "Actual"
    contact_groups = [azurerm_monitor_action_group.budget-alert-action-group.id]
  }

  notification {
    enabled        = true
    threshold      = 100.0
    operator       = "GreaterThan"
    threshold_type = "Forecasted"
    contact_groups = [azurerm_monitor_action_group.budget-alert-action-group.id]
  }

  notification {
    enabled        = true
    threshold      = 110.0
    operator       = "GreaterThan"
    threshold_type = "Forecasted"
    contact_groups = [azurerm_monitor_action_group.budget-alert-action-group.id]
  }
}