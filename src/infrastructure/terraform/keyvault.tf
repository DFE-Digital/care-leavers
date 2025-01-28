resource "azurerm_key_vault" "key-vault" {
  location            = local.location
  name                = "${local.service_prefix}-keyvault"
  resource_group_name = azurerm_resource_group.core-rg.name
  sku_name            = "standard"
  tenant_id           = data.azurerm_client_config.client.tenant_id

  # GitHub action accessing secrets for Terraform
  access_policy {
    object_id          = var.github_principal
    tenant_id          = data.azurerm_client_config.client.tenant_id
    secret_permissions = ["Get", "List", "Set", "Delete", "Purge"]
    key_permissions    = ["Get", "List", "Create", "Purge", "Delete"]
  }

  # Web app accessing config secrets
  access_policy {
    object_id          = azurerm_user_assigned_identity.web-app-identity.principal_id
    tenant_id          = azurerm_user_assigned_identity.web-app-identity.tenant_id
    secret_permissions = ["Get"]
  }
  access_policy {
    object_id          = azurerm_user_assigned_identity.web-app-staging-identity.principal_id
    tenant_id          = azurerm_user_assigned_identity.web-app-staging-identity.tenant_id
    secret_permissions = ["Get"]
  }

  network_acls {
    default_action = "Allow"
    bypass         = "AzureServices"
  }

  tags = local.common_tags
}