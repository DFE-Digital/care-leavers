resource "azurerm_frontdoor" "web-frontdoor" {
  name                = local.frontdoor_name
  resource_group_name = azurerm_resource_group.web-rg.name
  tags                = local.common_tags

  routing_rule {
    name               = "webRoutingRule"
    accepted_protocols = ["Http", "Https"]
    patterns_to_match  = ["/*"]
    frontend_endpoints = ["${local.service_prefix}-web-fd"]
    forwarding_configuration {
      forwarding_protocol = "MatchRequest"
      backend_pool_name   = "${local.service_prefix}-webBackend"
    }
  }

  routing_rule {
    name               = "webStagingRoutingRule"
    accepted_protocols = ["Http", "Https"]
    patterns_to_match  = ["/staging-health"]
    frontend_endpoints = ["${local.service_prefix}-web-fd"]
    forwarding_configuration {
      forwarding_protocol    = "MatchRequest"
      backend_pool_name      = "${local.service_prefix}-webStagingBackend"
      custom_forwarding_path = "/health"
    }
  }

  backend_pool_load_balancing {
    name = "${local.service_prefix}-LoadBalancingSettings"
  }

  backend_pool_health_probe {
    name    = "${local.service_prefix}-HealthProbeSetting"
    enabled = false
  }

  backend_pool {
    name = "${local.service_prefix}-webBackend"
    backend {
      host_header = azurerm_linux_web_app.web-app-service.default_hostname
      address     = azurerm_linux_web_app.web-app-service.default_hostname
      http_port   = 80
      https_port  = 443
    }

    load_balancing_name = "${local.service_prefix}-LoadBalancingSettings"
    health_probe_name   = "${local.service_prefix}-HealthProbeSetting"
  }
  backend_pool {
    name = "${local.service_prefix}-webStagingBackend"
    backend {
      host_header = azurerm_linux_web_app_slot.web-app-service-staging.default_hostname
      address     = azurerm_linux_web_app_slot.web-app-service-staging.default_hostname
      http_port   = 80
      https_port  = 443
    }

    load_balancing_name = "${local.service_prefix}-LoadBalancingSettings"
    health_probe_name   = "${local.service_prefix}-HealthProbeSetting"
  }
  backend_pool_settings {
    enforce_backend_pools_certificate_name_check = true
  }

  frontend_endpoint {
    name                                    = local.frontdoor_name
    host_name                               = local.frontdoor_url
    web_application_firewall_policy_link_id = azurerm_frontdoor_firewall_policy.web_firewall_policy.id
  }
}

resource "azurerm_frontdoor_firewall_policy" "web_firewall_policy" {
  name                = "webFirewallPolicy"
  resource_group_name = azurerm_resource_group.web-rg.name
  tags                = local.common_tags
  mode                = "Detection"
}