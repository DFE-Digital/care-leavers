---
title: 009 - Azure VNet Integration
layout: sub-navigation
sectionKey: Decisions
order: 9
includeInBreadcrumbs: true
eleventyNavigation:
  parent: Decisions
  key: 009 - Azure VNet Integration
---

## Context and Problem Statement

Our current Azure infrastructure relies primarily on public endpoints for resource accessibility (e.g. Storage Accounts, App Services). While protected by access controls and basic firewall rules, this architecture increases our attack surface.

To align with enterprise security compliance standards and follow the principle of least privilege, we need to isolate our infrastructure network traffic. We require a solution that ensures data remains within the Microsoft backbone network and prevents data exfiltration.

## Options Considered

### Option A - Azure Service Endpoints

Pros:

- Easier to configure
- No extra costs for runtime data processing
- Simplifies DNS management

Cons:

- Traffic still goes to the public IP of the PaaS service
- Does not provide protection against data exfiltration

### Option B - Public endpoints with strict IP whitelisting

Pros:

- Low admin overhead
- No virtual network infrastructure costs

Cons:

- Resources remain exposed to the public internet
- Managing dynamic IP whitelists are error prone

## Decision

We will implement a secure network perimeter using Azure Virtual Networks (VNets), segmented Subnets, Network Security Groups (NSGs), and Azure Private Endpoints.

Specifically, we will adopt the following design patterns:

- Deploy a dedicated VNet with strictly defined subnets categorised by application tiers (e.g., Web, Application, Data/Integration).
- Apply an NSG to every subnet. NSG rules will follow a "deny-by-default" posture, explicitly allowing only necessary inbound and outbound traffic between tiers.
- Disable public network access on PaaS offerings (Storage, Key Vaults, etc.) and inject them into our private subnets via Private Endpoints, assigning them private IP addresses.
- Implement Azure Private DNS Zones integrated with our VNet to ensure standard PaaS FQDNs seamlessly resolve to their respective private IPs.

## Consequences

### Positive

- PaaS services are completely hidden from the public internet, drastically reducing the risk of brute-force or zero-day exploits.
- Private Endpoints map to a specific resource instance rather than the entire multi-tenant service, stopping unauthorised data movement to external tenants.
- NSGs allow us to audit, log, and tightly restrict lateral movement within the network.

### Negative

- Introducing Private Endpoints requires robust management of Azure Private DNS Zones to avoid connection routing failures.
- Azure charges hourly for each Private Endpoint, alongside standard data processing fees (per GB) for inbound/outbound traffic.
- Increased effort required to manage NSG rule lifecycles, IP address space allocations, and subnet sizing.
- Current Azure policy is set to deny subnets without an NSG which blocks Terraform deployment, necessitating use of Azure API as a workaround.
