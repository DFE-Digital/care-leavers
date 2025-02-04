locals {
  common_tags = {
    "Environment"      = var.cip_environment
    "Product"          = "Design Operations"
    "Service"          = "Newly Onboarded"
    "Service Offering" = "Design Operations"
  }
  service_prefix = "s186${var.environment_prefix}-cl"
  location       = "westeurope"
}