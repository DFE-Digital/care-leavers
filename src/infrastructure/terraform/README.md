# Care Leavers Terraform

Terraform within the site can be provisioned via CI/CD.

## Project Setup

### Terraform State

If you are provisioning this project in a new environment or account, you will need to setup the remote Terraform State resources prior to provisioning other infrastructure.

This can be completed by authenticating to the AZ CLI and running the following commands.

```
az group create --name <subscription-name><environment-prefix>rg-uks-cl-tfstate --location uksouth --output none --tags "Environment=<your-environment>" "Product=Support for care leavers" "Service offering=Support for care leavers"
az storage account create --name <subscription-name><environment-prefix>cltfstate --resource-group <subscription-name><environment-prefix>rg-uks-cl-tfstate --location uksouth --sku Standard_LRS --https-only true
az storage container create --name tfstate --account-name <subscription-name><environment-prefix>cltfstate
```

Examples of substitutions can be seen below:
- subscription-name: s123
- environment-prefix: d01, t01, p01
- your-environment: Dev, Test, Prod

Note: This assumes you are creating the resources in the ELZ environment.