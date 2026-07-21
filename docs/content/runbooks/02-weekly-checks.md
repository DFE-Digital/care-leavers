---
title: Weekly Checks
layout: sub-navigation
sectionKey: Runbooks
order: 2
includeInBreadcrumbs: true
eleventyNavigation:
  parent: Runbooks
  key: Weekly Checks
---

## Revision/Document History

| Version | Date          | Author               | Reviewer(s) | Approver | Description of Changes         |
|---------|---------------|----------------------|-------------|----------|--------------------------------|
| 1.0     | 20/07/2026  | Rachna Mehta         |             |          | 							   |

## Introduction

### Overview

The weekly checks runbook for Care Leavers website lists all checks that should be performed every monday morning or on tuesday after the bank holiday.

Alongside all weekly checks, please ensure daily checks are also completed.

### GitHub Workflows

- **Generate - Documentation**: This GitHub workflow auto generates terraform documentation updated based on recent changes to main branch. This job runs at 12:00 AM everyday.

### Depandabot Checks

Every friday, Depandabot checks are run and these checks generate separate pull requests per type of checks. These PR's need to be reviewed and merged into the main branch after making any changes that are needed.

