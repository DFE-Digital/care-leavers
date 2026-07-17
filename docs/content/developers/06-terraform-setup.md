---
title: Terraform Setup
layout: sub-navigation
sectionKey: Developers
order: 6
includeInBreadcrumbs: true
eleventyNavigation:
  parent: Developers
  key: Terraform Setup
---

This document outlines the our terraform setup, and explains certain choices we have made.

---

## `.tfvars` Files

While Terraform allows multiple ways to supply variables, we have standardized on using environment-specific `.tfvars` files for all non-sensitive configuration values, while enforcing strict segregation for sensitive inputs (secrets).

### Why We Use `.tfvars`

To balance agility with security, our team uses explicit, environment-specific `.tfvars` files (e.g., elz-test.tfvars, elz-production.tfvars) stored directly within the repository under `src/infrastructure/terraform/env_vars`. This approach is chosen for three primary reasons:

1. **Cognitive Adjacency:** Keeping variable values side-by-side with your core infrastructure definitions code drastically lowers the barrier to entry for developers. There is no need to context-switch or query external CI/CD platforms (like GitHub Secrets) just to understand basic non-sensitive environment configurations.
2. **Easier to Reason About:** When variable files are tracked in source control, it is incredibly easy to audit, review, and track changes to environment configurations over time. You can view diffs during pull request reviews to see exactly what is changing in an environment.
3. **Strict Policy-as-Code Compliance:** Security scanners like Checkov require context to accurately evaluate threat postures. Passing explicit `.tfvars` files to the scanner prevents false positives and ensures rules are validated against actual deployment topologies.

### How We Handle Secrets

**No `.tfvars` file may ever contain real secret data.**

To handle sensitive values (e.g., API keys, service principal credentials), we utilise another variable injection method - dynamic CI/CD ingestion (environment variables).

Secrets required during deployment are injected dynamically by the CI/CD runner using Terraform-native environment variables prefixed with `TF_VAR_`. This is accompanied by setting the `sensitive = true` in the variable configuration itself, ensuring they are not tracked or displayed in git.
