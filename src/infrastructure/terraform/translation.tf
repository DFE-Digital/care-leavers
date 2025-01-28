# resource "azurerm_cognitive_account" "translation-cognitive-account" {
#   name                = "${local.service_prefix}-translation"
#   location            = azurerm_resource_group.core-rg.location
#   resource_group_name = azurerm_resource_group.core-rg.name
#   sku_name            = "S0"
#   kind                = "TextTranslation"
#   tags                = local.common_tags
# }
#
# resource "azurerm_key_vault_secret" "translation-access-key" {
#   key_vault_id = azurerm_key_vault.key-vault.id
#   name         = "translation-access-key"
#   value        = azurerm_cognitive_account.translation-cognitive-account.primary_access_key
# }