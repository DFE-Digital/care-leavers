locals {
  common_tags = {
    "Environment"      = var.cip_environment
    "Product"          = "Design operations"
    "Service"          = "Newly onboarded"
    "Service offering" = "Design operations"
  }
  service_prefix = "s186"
}