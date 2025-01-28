locals {
  common_tags = {
    "Environment"      = var.cip_environment
    "Product"          = "Design Operations"
    "Service"          = "Newly Onboarded"
    "Service Offering" = "Design Operations"
  }
  service_prefix = "s186${var.environment_prefix}-cl"
  location       = "westeurope"
  frontdoor_name = "${local.service_prefix}-web-fd"
  frontdoor_url  = "${local.frontdoor_name}.azurefd.net"
}