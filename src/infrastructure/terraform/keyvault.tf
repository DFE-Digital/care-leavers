resource "azurerm_key_vault" "key-vault" {
  location            = local.location
  name                = "${local.prefix}kv-uks-cl"
  resource_group_name = azurerm_resource_group.core-rg.name
  sku_name            = "standard"
  tenant_id           = data.azurerm_client_config.client.tenant_id

  purge_protection_enabled = true

  network_acls {
    default_action = "Allow"
    bypass         = "AzureServices"
  }

  tags = local.common_tags
}

resource "azurerm_key_vault" "kv" {
  location            = local.location
  name                = "${local.prefix}-kv-uks-cl"
  resource_group_name = azurerm_resource_group.core-rg.name
  sku_name            = "standard"
  tenant_id           = data.azurerm_client_config.client.tenant_id

  purge_protection_enabled = true

  rbac_authorization_enabled = true

  network_acls {
    default_action = "Allow"
    bypass         = "AzureServices"
  }

  tags = local.common_tags
}

# RBAC: allow app identity to read secrets (use object_id, not client_id)

data "azurerm_role_definition" "kv_secrets_user" {
  name  = "Key Vault Secrets User"
  scope = azurerm_key_vault.kv.id
}

data "azurerm_role_definition" "kv_secrets_officer" {
  name  = "Key Vault Secrets Officer"
  scope = azurerm_key_vault.kv.id
}


data "azurerm_role_definition" "kv_admin" {
  name  = "Key Vault Administrator"
  scope = azurerm_key_vault.kv.id
}

# Role assignments

resource "azurerm_role_assignment" "kv_user" {
  scope              = azurerm_key_vault.kv.id
  role_definition_id = data.azurerm_role_definition.kv_secrets_user.role_definition_id
  principal_id       = azurerm_user_assigned_identity.cl-identity-reader.principal_id
  principal_type     = "ServicePrincipal"
}

resource "azurerm_role_assignment" "kv_officer" {
  scope              = azurerm_key_vault.kv.id
  role_definition_id = data.azurerm_role_definition.kv_secrets_officer.role_definition_id
  principal_id       = azurerm_user_assigned_identity.cl-identity-administrator.principal_id
  principal_type     = "ServicePrincipal"
}

resource "azurerm_role_assignment" "kv_administrator" {
  scope              = azurerm_key_vault.kv.id
  role_definition_id = data.azurerm_role_definition.kv_admin.role_definition_id
  principal_id       = azurerm_user_assigned_identity.cl-identity-administrator.principal_id
  principal_type     = "ServicePrincipal"
}

resource "azurerm_role_assignment" "kv_admin_sp" {
  scope              = azurerm_key_vault.kv.id
  role_definition_id = data.azurerm_role_definition.kv_admin.role_definition_id
  principal_id       = data.azurerm_client_config.client.object_id
  principal_type     = "ServicePrincipal"
}

# Secrets

resource "azurerm_key_vault_secret" "contentful-delivery-api-key-keyvault" {
  key_vault_id = azurerm_key_vault.kv.id
  name         = "contentful-delivery-api-key"
  value        = var.contentful_delivery_api_key

  depends_on = [
    azurerm_role_assignment.kv_officer,
    azurerm_role_assignment.kv_administrator,
    azurerm_role_assignment.kv_admin_sp
  ]
}

resource "azurerm_key_vault_secret" "contentful-preview-api-key-keyvault" {
  key_vault_id = azurerm_key_vault.kv.id
  name         = "contentful-preview-api-key"
  value        = var.contentful_preview_api_key

  depends_on = [
    azurerm_role_assignment.kv_officer,
    azurerm_role_assignment.kv_administrator,
    azurerm_role_assignment.kv_admin_sp
  ]
}

resource "azurerm_key_vault_secret" "contentful-management-api-key-keyvault" {
  key_vault_id = azurerm_key_vault.kv.id
  name         = "contentful-management-api-key"
  value        = var.contentful_management_api_key

  depends_on = [
    azurerm_role_assignment.kv_officer,
    azurerm_role_assignment.kv_administrator,
    azurerm_role_assignment.kv_admin_sp
  ]
}

resource "azurerm_key_vault_secret" "contentful-space-id-keyvault" {
  key_vault_id = azurerm_key_vault.kv.id
  name         = "contentful-space-id"
  value        = var.contentful_space_id

  depends_on = [
    azurerm_role_assignment.kv_officer,
    azurerm_role_assignment.kv_administrator,
    azurerm_role_assignment.kv_admin_sp
  ]
}

resource "azurerm_key_vault_secret" "application-insights-connection-string-keyvault" {
  key_vault_id = azurerm_key_vault.kv.id
  name         = "application-insights-connection-string"
  value        = azurerm_application_insights.application-insights.connection_string

  depends_on = [
    azurerm_role_assignment.kv_officer,
    azurerm_role_assignment.kv_administrator,
    azurerm_role_assignment.kv_admin_sp
  ]
}

resource "azurerm_key_vault_secret" "pdf-generation-api-key-keyvault" {
  key_vault_id = azurerm_key_vault.kv.id
  name         = "pdf-generation-api-key"
  value        = var.pdf_generation_api_key

  depends_on = [
    azurerm_role_assignment.kv_officer,
    azurerm_role_assignment.kv_administrator,
    azurerm_role_assignment.kv_admin_sp
  ]
}

resource "azurerm_key_vault_secret" "azure-translation-access-key-keyvault" {
  key_vault_id = azurerm_key_vault.kv.id
  name         = "azure-translation-access-key"
  value        = azurerm_cognitive_account.ai-translator.primary_access_key

  depends_on = [
    azurerm_role_assignment.kv_officer,
    azurerm_role_assignment.kv_administrator,
    azurerm_role_assignment.kv_admin_sp
  ]
}

# key-vault secrets

resource "azurerm_key_vault_secret" "contentful-delivery-api-key-keyvault" {
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "contentful-delivery-api-key"
  value        = var.contentful_delivery_api_key

  depends_on = [azurerm_key_vault_access_policy.github-kv-access]
}

resource "azurerm_key_vault_secret" "contentful-preview-api-key-keyvault" {
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "contentful-preview-api-key"
  value        = var.contentful_preview_api_key

  depends_on = [azurerm_key_vault_access_policy.github-kv-access]
}

resource "azurerm_key_vault_secret" "contentful-management-api-key-keyvault" {
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "contentful-management-api-key"
  value        = var.contentful_management_api_key

  depends_on = [azurerm_key_vault_access_policy.github-kv-access]
}

resource "azurerm_key_vault_secret" "contentful-space-id-keyvault" {
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "contentful-space-id"
  value        = var.contentful_space_id

  depends_on = [azurerm_key_vault_access_policy.github-kv-access]
}

resource "azurerm_key_vault_secret" "application-insights-connection-string-keyvault" {
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "application-insights-connection-string"
  value        = azurerm_application_insights.application-insights.connection_string

  depends_on = [azurerm_key_vault_access_policy.github-kv-access]
}

resource "azurerm_key_vault_secret" "pdf-generation-api-key-keyvault" {
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "pdf-generation-api-key"
  value        = var.pdf_generation_api_key

  depends_on = [azurerm_key_vault_access_policy.github-kv-access]
}

resource "azurerm_key_vault_secret" "azure-translation-access-key-keyvault" {
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "azure-translation-access-key"
  value        = azurerm_cognitive_account.ai-translator.primary_access_key

  depends_on = [azurerm_key_vault_access_policy.github-kv-access]
}