resource "azurerm_cdn_frontdoor_profile" "frontdoor-web-profile" {
  name                = "${local.service_prefix}-web-fd-profile"
  resource_group_name = azurerm_resource_group.web-rg.name
  sku_name            = "Standard_AzureFrontDoor"
  tags                = local.common_tags
}

resource "azurerm_cdn_frontdoor_origin_group" "frontdoor-origin-group" {
  cdn_frontdoor_profile_id = azurerm_cdn_frontdoor_profile.frontdoor-web-profile.id
  name                     = "${local.service_prefix}-web-fd-origin-group"

  health_probe {
    interval_in_seconds = 60
    protocol            = "Https"
    request_type        = "GET"
    path                = "/health"
  }

  load_balancing {
    sample_size                 = 4
    successful_samples_required = 2
  }
}

resource "azurerm_cdn_frontdoor_origin" "frontdoor-web-origin" {
  cdn_frontdoor_origin_group_id  = azurerm_cdn_frontdoor_origin_group.frontdoor-origin-group.id
  certificate_name_check_enabled = false
  host_name                      = azurerm_linux_web_app.web-app-service.default_hostname
  http_port                      = 80
  https_port                     = 443
  origin_host_header             = azurerm_linux_web_app.web-app-service.default_hostname
  priority                       = 1
  weight                         = 1
  name                           = "${local.service_prefix}-web-fd-origin"
}

resource "azurerm_cdn_frontdoor_endpoint" "frontdoor-web-endpoint" {
  cdn_frontdoor_profile_id = azurerm_cdn_frontdoor_profile.frontdoor-web-profile.id
  name                     = "${local.service_prefix}-web-fd-endpoint"
  tags                     = local.common_tags
}

resource "azurerm_cdn_frontdoor_route" "frontdoor-web-route" {
  name                          = "${local.service_prefix}-web-fd-route"
  cdn_frontdoor_endpoint_id     = azurerm_cdn_frontdoor_endpoint.frontdoor-web-endpoint.id
  cdn_frontdoor_origin_group_id = azurerm_cdn_frontdoor_origin_group.frontdoor-origin-group.id
  cdn_frontdoor_origin_ids      = [azurerm_cdn_frontdoor_origin.frontdoor-web-origin.id]

  forwarding_protocol    = "MatchRequest"
  https_redirect_enabled = true
  patterns_to_match      = ["/*"]
  supported_protocols    = ["Http", "Https"]
}

resource "azurerm_cdn_frontdoor_security_policy" "frontdoor-web-security-policy" {
  name                     = "${local.service_prefix}-web-fd-security-policy"
  cdn_frontdoor_profile_id = azurerm_cdn_frontdoor_profile.frontdoor-web-profile.id

  security_policies {
    firewall {
      cdn_frontdoor_firewall_policy_id = azurerm_cdn_frontdoor_firewall_policy.web_firewall_policy.id

      association {
        domain {
          cdn_frontdoor_domain_id = azurerm_cdn_frontdoor_endpoint.frontdoor-web-endpoint.id
        }
        patterns_to_match = ["/*"]
      }
    }
  }
}

resource "azurerm_cdn_frontdoor_firewall_policy" "web_firewall_policy" {
  name                = "webFirewallPolicy"
  resource_group_name = azurerm_resource_group.web-rg.name
  tags                = local.common_tags
  mode                = "Detection"
  sku_name            = azurerm_cdn_frontdoor_profile.frontdoor-web-profile.sku_name
}