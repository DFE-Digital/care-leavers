variable "cip_environment" {
  description = "The CIP environment to match subscription (e.g. Dev)"
  type        = string
}

variable "environment_prefix" {
  description = "Environment prefix (e.g. d01)"
  type        = string
}

variable "contentful_delivery_api_key" {
  description = "Contentful Delivery API Key"
  type        = string
  sensitive   = true
}

variable "contentful_preview_api_key" {
  description = "Contentful Preview API Key"
  type        = string
  sensitive   = true
}

variable "contentful_management_api_key" {
  description = "Contentful Management API Key"
  type        = string
  sensitive   = true
}


variable "contentful_space_id" {
  description = "Contentful Space ID"
  type        = string
  sensitive   = true
}

variable "contentful_environment" {
  description = "Contentful Environment"
  type        = string
}

variable "contentful_use_preview_api" {
  description = "Use Contentful Preview API?"
  type        = bool
}

variable "pdf_generation_use_sandbox" {
  description = "Generate PDFs in Sandbox Mode?"
  type        = bool
}

variable "pdf_generation_api_key" {
  description = "PDF Generation API Key"
  type        = string
  sensitive   = true
}

variable "caching_type" {
  description = "Caching type"
  type        = string
}

variable "scripts_clarity" {
  description = "Clarity code"
  type        = string
}

variable "custom_domain" {
  description = "Custom front-door domain"
  type        = string
  default     = ""
}

variable "aspnetcore_environment" {
  description = "ASP.NET Core environment"
  type        = string
}

variable "azure_translation_access_key" {
  description = "Azure Translation Access Key"
  type        = string
  sensitive   = true
  default     = ""
}

variable "azure_translation_document_endpoint" {
  description = "Azure Document Translation Endpoint"
  type        = string
  default     = ""
}

variable "azure_frontdoor_scale" {
  description = "Azure Front Door Scale"
  type        = string
  default     = "Standard_AzureFrontDoor"
}

variable "rebrand" {
  description = "Force DfE Rebrand before 25th June 2025"
  type        = bool
  default     = false
}