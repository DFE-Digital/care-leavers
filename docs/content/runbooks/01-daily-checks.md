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

### Document Purpose

This document lists all the daily checks that should be performed every morning.

**1. Running ZAP scan against elz-staging**: 
- This GitHub Work flow runs scan against elz-staging environment. 
- The job is scheduled to run at 03 AM, Monday through Friday. 
- This workflow generates a ZAP Security Scan Summary which can be seen on the workflow. 
- Zap action report can also be downloaded. 
	
**2. Link Validation Check**: 
- This GitHub workflow scans for all broken links found on the production care leavers website (https://www.support-for-care-leavers.education.gov.uk). 
- This job is scheduled to run at 02:00 AM, Monday through Friday.

**3. Generate - Documentation**: 
- This GitHub workflow auto generates terraform documentation based on recent changes to main branch. 
- This job is scheduled to run at 12 AM everyday.

**4. Performing Contentful Backup**:
- This Github workflow creates backup and exports it to file storage.
- This job is scheduled to run at 11:00 PM, Monday through Friday. 
- The backup file is then uploaded to blob storage. 

Please note:- 
_our prod environment (in Azure) requires approval before it can be deployed to, so an automated workflow wouldn't work/be able to make a backup and deploy.
So it was decided to backup in our dev account and store it in the developers main environment (dev/test). It can be restore if needed. 
So this workflow takes the backup of our production site and stores in dev storage account. 
This workflow is being run as an extra precaution as contentful takes its own backups. But if things go wrong from our side. We also have a backup to restore from._

