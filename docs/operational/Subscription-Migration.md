# Azure Subscription Migration

## Overview
This document details how the migration from the Design-Ops subscription to a new Azure subscription should happen.

## DfE Azure

As the services used to host the site as not considered "stateful", we 
do not need to consider data loss/recovery in Azure.

All services are provisioned via Terraform, so we can provision the site in a new subscription quickly.

To do this, we need to configure the deployment accordingly.

## AI Policy Exemption

You may need to request an exemption for the subscription to allow the use of Microsoft Cognitive AI services.
Please speak with the AI Centre of Excellence before attempting to deploy the site, as it will fail to deploy without the exemption applied.

## Infrastructure as Code Changes

Some changes may need to be edited in the `locals.tf` file to ensure the correct tags are created when the Terraform is run.

These changes exist in the `/infrastructure/terraform` folder of the root project.

Currently this is set to:

```terraform
locals {
  common_tags = {
    "Environment"      = var.cip_environment
    "Product"          = "Design Operations"
    "Service"          = "Newly Onboarded"
    "Service Offering" = "Design Operations"
  }
  service_prefix = "s186${var.environment_prefix}-cl"
  location       = "westeurope"
}
```

These entries would need to be updating to whatever tags and service prefixes the new subscription requires.

Some parts of this, for example `cip_environment` and `environement_prefix` are assigned via GitHub Actions (as seen below)

## GitHub Actions

### Environment Secrets

For each environment, the following secrets need updating in GitHub by someone with the correct permissions:

- `AZURE_CLIENT_ID` - The client ID the Azure SDK (and Terraform) should use
- `AZURE_CLIENT_SECRET` - The secret for the above client ID
- `AZURE_SUBSCRIPTION_ID` - The subscription in which the new infrastructure should live
- `AZURE_TENANT_ID` - The tenant in which the new infrastructure should live
- `AZURE_TRANSLATION_ACCESS_KEY` - The API key for translating content on the site _(This may not be available until after the first deployment, so you may need a second deployment)_

### Environment Variables

For each environment, the following variables need updating in GitHub by someone with the correct permissions:

- CIP_ENVIRONMENT - The environment this deployment should sit within (usually `development`, `test`, or `production`)
- ENVIRONMENT_PREFIX - The environment prefix for this deployment (eg: t01, t02)
- AZURE_TRANSLATION_DOCUMENT_ENDPOINT - The service URL of the Azure Document Translation endpoint. _(This may not be available until after the first deployment, so you may need a second deployment)_

## Post-Deployment Steps

Before tearing down the old environment, you will need to perform the following steps:

1. Ensure you have deployed the infrastructure once, then gather the `AZURE_TRANSLATION_ACCESS_KEY` and `AZURE_TRANSLATION_DOCUMENT_ENDPOINT` from the Azure Portal
2. Update the corresponding variables/secrets above and re-deploy to setup the environments to use the new resources
3. Open the Azure Portal and verify the environment custom domain name via the Front Door profile
4. Open a *Service Now* request to validate and approve the domain name
    1. You may need to provide the DNS verification record determined in step 3 above
    2. You will need to provide the endpoint association address from Front Door for the CNAME record
5. Once the domain has been verified and the DNS has propagated, you may need to wait up to 24 hours for the DNS to take effect
6. Once Application Insights and Front Door logs show no further traffic is hitting the old subscription, you can stop the App Service
7. Once the environment has been fully migrated, you can remove the following resource groups for each environment in the old subscription:
    - ai-services
    - caching-rg
    - core-rg
    - load-testing
    - tfstate
    - web-rg