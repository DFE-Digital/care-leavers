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