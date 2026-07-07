# Release Process

This document outlines the process for deploying the application to the **Test** and **Production** environments.

---

## Automated Deployments to Test

When a pull request is merged into the `main` branch, the following actions are automatically triggered:

1.  **New Tag Creation**: The workflow finds the most recent tag (e.g., `v1.2.3`), increments it (to `v1.2.4`), and applies it to the merge commit.
2.  **Deployment to Test**: The newly tagged version is automatically deployed to the **Test** environment. This is done against the main branch on the `Deploy with Branch` workflow.
3.  **Contentful Migrations**: Any new Contentful migrations are also deployed to the **Test** environment.

This process ensures that the **Test** environment always reflects the latest version of the `main` branch.

---

## Deployments to environments

Deployments to the **Production** environment are **manual**.

### 1. Create a GitHub Release (Primary Method)

This is the standard way to deploy a version that has been fully validated in the Test environment.

1.  **Choose a Version**: Identify the tag you want to release to production (e.g., `v1.2.4`).
2.  **Create a New Release**:
    * Navigate to the **Releases** page in the GitHub repository.
    * Click **Draft a new release**.
    * Select the tag you want to deploy from the **Choose a tag** dropdown.
    * Add a title and description for the release notes.
    * Click **Publish release**.
3.  **Releasing Through Environments**: You should then release this tag through all environments (test, staging and production) using the `Deploy with Tag` workflow.
    * **Note:** Releasing to production will require a manual approval from another team member within GitHub

### 2. Manual Workflow Dispatch - if you just want to deploy latest version of Main (Alternative Method)

You can also trigger a deployment manually without creating a formal release. This is useful for hotfixes or special circumstances.

1.  Navigate to the **Actions** tab in the GitHub repository.
2.  In the left sidebar, find and click on the **Deploy with Branch** workflow to release off a branch or the **Deploy with Tag** workflow to release off a tag.
3.  Click the **Run workflow** dropdown button, which appears on the right side of the page.
4.  Select the branch you want to run the Workflow from **or** select the specific **tag version** (e.g., `v1.2.5`), the **environment you want to deploy**. 
5.  Click the green **Run workflow** button to start the deployment.