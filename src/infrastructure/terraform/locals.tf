locals {
  common_tags = {
    "Product"          = "Support for care leavers"
    "Service Offering" = "Support for care leavers"
  }
  prefix         = "s272${var.environment_prefix}"
  service_prefix = "s272${var.environment_prefix}-uks-cl"
  location       = "uksouth"
}