# Care Leavers - Node

This directory contains the frontend build system for the `CareLeavers.Web` project.

It performs the following tasks that are needed for the functionality of the project:

- Compiles, bundles and minifies the SCSS into CSS. This includes both all of our own custom SCSS and the GOV.UK and DfE Frontend SCSS.
- Copies the minified JavaScript out of the GOV.UK & DfE Frontend.
- Copies the assets (images, fonts and so on). This includes both all of our custom assets and the GOV.UK and DfE Frontend assets.
- Takes all of this and populates the `wwwroot` directory in the `CareLeavers.Web` project.

The Dockerfile executes this automatically. This means that no additional input is needed at deploy-time or when running E2E tests.

## Local Development Instructions

1. Run `npm install` to install the dependencies.
2. Run `npm run build` to execute the build script and populate `wwwroot`