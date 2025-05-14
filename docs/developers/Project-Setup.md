# Project Setup

The project is made up of various components, each with their own README files detailing setup.

## Website

This is the main website for the Care Leavers project. It is a .NET MVC application that uses Contentful
as a headless CMS and accesses these via the Contentful API.

To run this, you will need to have access to a contentful space and ensured the content models have been migrated.

- [README Here](../../src/web/README.md)

## End-to-end tests

We have a suite of end-to-end tests that run against the website to ensure it is functioning as expected.
This uses Playwright Typescript which runs against a site host either locally or via Docker in the Pipeline. 
The E2E tests have their own dedicated Contentful environment which should only be changed when working on the tests.

The best way to ensure full E2E testing is to do the following:

- Rebuild the docker image using the Dockerfile in the `/web` folder
- Ensure you have a `docker-compose-local.yml` file in the `/src/infrastructure/docker` folder
- _** DO NOT ADD THIS FILE TO GITHUB **_ as it will later contain secrets
- Add the following to the file:
```yaml
name: 'care-leavers'
services:
  care-leavers-web:
    image: care-leavers-web:e2e
    ports:
      - "8080:8080"
      - "8081:8081"
      - "7050:8081"
    environment:
      - ASPNETCORE_ENVIRONMENT=EndToEnd
      - Caching__Type=Memory
      - ContentfulOptions__DeliveryApiKey={YOUR_DELIVERY_API_KEY_HERE}
      - ContentfulOptions__PreviewApiKey={YOUR_PREVIEW_API_KEY_HERE}
      - ContentfulOptions__SpaceId={YOUR_SPACE_ID_HERE}
      - ContentfulOptions__UsePreviewApi=true
      - ContentfulOptions__Environment=e2e
      - ASPNETCORE_URLS=http://+:8080;https://+:8081
      - ASPNETCORE_Kestrel__Certificates__Default__Password=e2e
      - ASPNETCORE_Kestrel__Certificates__Default__Path=/app/aspnetapp.pfx
```
- Ensure any existing development instances are stopped
- Start this docker-compose file via the IDE or Command Line
- Open a shell in the `/src/e2e/CareLeavers.E2ETests` folder
- Run your E2E tests using `yarn playwright test`
- If there are any failures, view them easily using `yarn playwright show-report`


Further testing information is available via the [README here](../../src/e2e/CareLeavers.E2ETests/README.md)

## Infrastructure

All other services are hosted on Azure. These services are provisioned via Terraform and are deployed as part of the deployment
pipeline. 

- [README Here](../../src/infrastructure/terraform/README.md)

## Contentful Migration

Contentful models are tracked code first and can be migrated to a Contentful space using this project and the Contentful CLI.
Ensure you have followed the steps of logging in and have the CLI installed.

- [README Here](../../src/contentful/CareLeavers.ContentfulMigration/README.md)