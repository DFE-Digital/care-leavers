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

- [Terraform Docs](../architecture/Terraform.md)
- [README Here](../../src/e2e/CareLeavers.E2ETests/README.md)

## Infrastructure

All other services are hosted on Azure. These services are provisioned via Terraform and are deployed as part of the deployment
pipeline. 

- [README Here](../../src/infrastructure/terraform/README.md)

## Contentful Migration

Contentful models are tracked code first and can be migrated to a Contentful space using this project and the Contentful CLI.
Ensure you have followed the steps of logging in and have the CLI installed.

- [README Here](../../src/contentful/CareLeavers.ContentfulMigration/README.md)