terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 4.52"
    }
    azapi = {
      source  = "Azure/azapi"
      version = "2.9.0"
    }
  }
  backend "azurerm" {}
}

provider "azurerm" {
  features {}
}

provider "azapi" {}
