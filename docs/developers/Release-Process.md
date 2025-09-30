# Release Process

This document outlines the process for deploying the application to the **Test** and **Production** environments.

---

## Automated Deployments to Test

When a pull request is merged into the `main` branch, the following actions are automatically triggered:

1.  **New Tag Creation**: The workflow finds the most recent tag (e.g., `v1.2.3`), increments it (to `v1.2.4`), and applies it to the merge commit.
2.  **Deployment to Test**: The newly tagged version is automatically deployed to the **Test** environment.
3.  **Contentful Migrations**: Any new Contentful migrations are also deployed to the **Test** environment.

This process ensures that the **Test** environment always reflects the latest version of the `main` branch.

---

## Deployments to Production

Deployments to the **Production** environment are **manual** and are triggered by creating a new release in GitHub.

1.  **Choose a Version**: Identify the tag that you want to release to production (e.g., `v1.2.4`). This will typically be a version that has been validated in the Test environment.
2.  **Create a New Release**:
    * Navigate to the "Releases" page in the GitHub repository.
    * Click "Draft a new release."
    * Select the tag you want to deploy from the "Choose a tag" dropdown.
    * Add a title and description for the release.
    * Click "Publish release."
3.  **Automatic Deployment**: Publishing the release will automatically trigger the deployment workflow, which will push the selected version to the **Production** environment.