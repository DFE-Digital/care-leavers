locals {
  common_tags = {
    "Environment"      = var.elz_environment
    "Product"          = "Design Operations"
    "Service"          = "Newly Onboarded"
    "Service Offering" = "Design Operations"
  }
  prefix         = var.environment_prefix
  service_prefix = "${var.environment_prefix}-uks-cl"
  location       = "uksouth"
}