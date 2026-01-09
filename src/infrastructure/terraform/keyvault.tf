resource "azurerm_key_vault" "key-vault" {
  location            = local.location
  name                = "${local.prefix}kv-uks-cl"
  resource_group_name = azurerm_resource_group.core-rg.name
  sku_name            = "standard"
  tenant_id           = data.azurerm_client_config.client.tenant_id

  network_acls {
    default_action = "Allow"
    bypass         = "AzureServices"
  }

  tags = local.common_tags
}

##### old changes
resource "azurerm_key_vault_access_policy" "github-kv-access" {
  key_vault_id       = azurerm_key_vault.key-vault.id
  tenant_id          = data.azurerm_client_config.client.tenant_id
  object_id          = data.azurerm_client_config.client.object_id
  secret_permissions = ["Get", "List", "Set", "Delete", "Purge", "Restore"]
}

resource "azurerm_key_vault_access_policy" "web-app-kv-access" {
  key_vault_id       = azurerm_key_vault.key-vault.id
  tenant_id          = azurerm_linux_web_app.web-app-service.identity[0].tenant_id
  object_id          = azurerm_linux_web_app.web-app-service.identity[0].principal_id
  secret_permissions = ["Get"]
}

resource "azurerm_key_vault_access_policy" "web-app-staging-kv-access" {
  key_vault_id       = azurerm_key_vault.key-vault.id
  tenant_id          = azurerm_linux_web_app_slot.web-app-service-staging.identity[0].tenant_id
  object_id          = azurerm_linux_web_app_slot.web-app-service-staging.identity[0].principal_id
  secret_permissions = ["Get"]
}


### provisional changes
# data "azurerm_role_definition" "kv_secrets_user" {
#   name  = "Key Vault Secrets User"
#   scope = azurerm_key_vault.kv.id
# }

# data "azurerm_role_definition" "kv_secrets_officer" {
#   name  = "Key Vault Secrets Officer"
#   scope = azurerm_key_vault.kv.id
# }

# data "azurerm_role_definition" "kv_admin" {
#   name  = "Key Vault Administrator"
#   scope = azurerm_key_vault.kv.id
# }

# resource "azurerm_role_assignment" "kv_officer" {
#   scope              = azurerm_key_vault.kv.id
#   role_definition_id = data.azurerm_role_definition.kv_secrets_officer.role_definition_id
#   principal_id       = azurerm_user_assigned_identity.cl-identity.principal_id
#   principal_type     = "ServicePrincipal"
# }

# resource "azurerm_role_assignment" "kv_user" {
#   scope              = azurerm_key_vault.kv.id
#   role_definition_id = data.azurerm_role_definition.kv_secrets_user.role_definition_id
#   principal_id       = azurerm_user_assigned_identity.cl-identity.principal_id
#   principal_type     = "ServicePrincipal"
# }

# resource "azurerm_role_assignment" "kv_administrator" {
#   scope              = azurerm_key_vault.kv.id
#   role_definition_id = data.azurerm_role_definition.kv_admin.role_definition_id
#   principal_id       = azurerm_user_assigned_identity.cl-identity.principal_id
#   principal_type     = "ServicePrincipal"
# }

# resource "azurerm_role_assignment" "kv_admin_sp" {
#   scope              = azurerm_key_vault.kv.id
#   role_definition_id = data.azurerm_role_definition.kv_admin.role_definition_id
#   principal_id       = data.azurerm_client_config.client.object_id
#   principal_type     = "ServicePrincipal"
# }




resource "azurerm_key_vault_secret" "contentful-delivery-api-key" {
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "contentful-delivery-api-key"
  value        = var.contentful_delivery_api_key

  depends_on = [azurerm_key_vault_access_policy.github-kv-access]
}

resource "azurerm_key_vault_secret" "contentful-preview-api-key" {
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "contentful-preview-api-key"
  value        = var.contentful_preview_api_key

  depends_on = [azurerm_key_vault_access_policy.github-kv-access]
}

resource "azurerm_key_vault_secret" "contentful-management-api-key" {
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "contentful-management-api-key"
  value        = var.contentful_management_api_key

  depends_on = [azurerm_key_vault_access_policy.github-kv-access]
}

resource "azurerm_key_vault_secret" "contentful-space-id" {
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "contentful-space-id"
  value        = var.contentful_space_id

  depends_on = [azurerm_key_vault_access_policy.github-kv-access]
}

resource "azurerm_key_vault_secret" "application-insights-connection-string" {
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "application-insights-connection-string"
  value        = azurerm_application_insights.application-insights.connection_string

  depends_on = [azurerm_key_vault_access_policy.github-kv-access]
}

resource "azurerm_key_vault_secret" "azure-translation-access-key" {
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "azure-translation-access-key"
  value        = var.azure_translation_access_key

  depends_on = [azurerm_key_vault_access_policy.github-kv-access]
}

resource "azurerm_key_vault_secret" "pdf-generation-api-key" {
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "pdf-generation-api-key"
  value        = var.pdf_generation_api_key

  depends_on = [azurerm_key_vault_access_policy.github-kv-access]
}