---
title: Daily Checks
layout: sub-navigation
sectionKey: Runbooks
order: 1
includeInBreadcrumbs: true
eleventyNavigation:
  parent: Runbooks
  key: Daily Checks
---

## Revision/Document History

| Version | Date          | Author               | Reviewer(s) | Approver | Description of Changes         |
|---------|---------------|----------------------|-------------|----------|--------------------------------|
| 1.0     | 20/07/2026  | Rachna Mehta         |             |          | 							   |

## Introduction

### Overview

The daily checks runbook for Care Leavers website lists all the daily checks that developers should perform as a practice.

### Azure Alerts

Azure alert emails may be received in your inbox. These need to be reviewed and actioned appropriately.
To receive these emails you should be added to C&F Operations group. Pradeep NEELAKANDAN can add you to this group.

### GitHub Workflows

This document lists all the daily checks that should be performed every morning for GitHub Workflows.

**1. Running ZAP scan against elz-staging**: 
- This GitHub Work flow runs scan against elz-staging environment. 
- The job is scheduled to run at 03:00 AM, Monday through Friday. 
- This workflow generates a ZAP Security Scan Summary which can be seen on the workflow. 
- Zap action report can also be downloaded. 
	
**2. Link Validation Check**: 
- This GitHub workflow scans for all broken links found on the [production](https://www.support-for-care-leavers.education.gov.uk) care leavers website. 
- This job is scheduled to run at 02:00 AM, Monday through Friday.

**3. Generate - Documentation**: 
- This GitHub workflow auto generates terraform documentation based on recent changes to main branch. 
- This job is scheduled to run at 12:00 AM everyday.

**4. Performing Contentful Backup**:
- This Github workflow creates backup and exports it to file storage.
- This job is scheduled to run at 11:00 PM, Monday through Friday. 
- The backup file is then uploaded to blob storage. 

Please note:- 
_Our production environment requires manual approval before a deployment can begin, restricting our ability to use an automated workflow. It was decided to backup production in our storage dev account so it can be restored quickly. Contentful will take its own backups in case of error, however this will support developers in restoring the site should an issue occur._

