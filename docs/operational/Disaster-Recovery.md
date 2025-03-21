# Disaster Recovery

## Overview
This document details information on our disaster recovery plan for the Care Leavers website.

## DfE Azure

Aside from Contentful, all services are hosted on Azure. Including
- Azure App Service
- Azure Front Door
- Azure Redis Cache
- Azure AI Translation

Due to the nature of these services, there are no services that could be considered "stateful" and therefore we 
do not need to consider data loss/recovery in Azure in the event of a disaster.

All services are provisioned via Terraform, so we can respond quickly to re-provision resources where necessary.

## Contentful

Contentful is a third-party service that we use to store our content. They have a 99.99% SLA and have their own backup
and disaster recovery plans in place.

## Considerations

- If Azure Front Door requires re-creation due to complete failure, we will need to contact ServiceNow to update the DNS
  records to point to the new Front Door instance.
- For Contentful downtime, we will need to contact Contentful support to understand the issue and ETA for resolution. The 
  site will continue to function with the Azure Redis Cache, but with potentially stale content.