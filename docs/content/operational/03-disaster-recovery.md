---
title: Disaster Recovery
layout: sub-navigation
sectionKey: Operational
order: 3
includeInBreadcrumbs: true
eleventyNavigation:
  parent: Operational
  key: Disaster Recovery
---

## Overview

This document details information on our disaster recovery plan for the Care Leavers website.

## DfE Azure

Aside from Contentful, all services are hosted on Azure. Including

- Azure App Service
- Azure Front Door
- Azure Managed Redis
- Azure AI Translation

Due to the nature of these services, there are no services that could be considered "stateful" and therefore we do not need to consider data loss/recovery in Azure in the event of a disaster.

All services are provisioned via Terraform, so we can respond quickly to re-provision resources where necessary.

## Contentful

Contentful is a third-party service that we use to store our content. They have a 99.99% SLA and have their own backup
and disaster recovery plans in place.

In addition, we create a daily backup of the production space in an automated GitHub workflow. The export is stored in a Storage Account on Azure on the 'elz-test' subscription. We cannot store the backup in production as the current configuration needs a team member to approve any deployments to production.

### Restore

We have taken the decision to not create an automated restore workflow as Contentful handles the high availability, replication, and infrastructure-level disaster recovery on their side.

Our backup serves a very specific purpose: protecting against human error or logical corruption.

Follow these steps to restore:

- Donwload the JSON export. Either donwload the file via the Azure portal or run `az storage blob download` command.
- Start the Docker restore image. Run the container from a local terminal.
- Point the CLI either to a sandbox environment or production to replace the current configuration.

## Considerations

- If Azure Front Door requires re-creation due to complete failure, we will need to contact ServiceNow to update the DNS records to point to the new Front Door instance.
- For Contentful downtime, we will need to contact Contentful support to understand the issue and ETA for resolution. The site will continue to function with the Azure Managed Redis, but with potentially stale content.
