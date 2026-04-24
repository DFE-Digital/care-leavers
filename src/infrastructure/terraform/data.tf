data "azurerm_client_config" "client" {}

data "azuread_group" "cl_dev_team" {
  display_name     = var.cl_dev_team_group_name
  security_enabled = true
}