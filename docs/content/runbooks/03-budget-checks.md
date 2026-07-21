---
title: Budget Checks and Alerts
layout: sub-navigation
sectionKey: Runbooks
order: 3
includeInBreadcrumbs: true
eleventyNavigation:
  parent: Runbooks
  key: Budget Checks and Alerts
---

## Introduction

### Overview

The budget checks and alerts runbook for Care Leavers website lists the ways the budgets for the site can be tracked. These should be tracked regularly throughout the month.

These budgets relate to the cost of the infrastructure within Azure. The budgets that have been set-up relate to both the whole subscription, and then specific resources (currently the AI translate resource).

## Budgets

### Checks

Withn Azure, you are able to check the current spending of the given subscription. You can do this in a couple of different ways:

**1. Azure Cost Analysis:**
- Navigate to 'Subscriptions' in the Azure portal
- Select your given subscription
- Select 'Cost analysis' under 'Cost Management' in the left hand menu
- You will now be presented with a full view for the cost of that subscription

**2. Azure Budgets:**
- Navigate to 'Budgets' in the Azure portal
- Select the scope required (the subscription you want to look at the budgets for)
- You will then be presented with:
  - The budget for that subscription/resource
  - The forcasted spend
  - The evaluated spend and the progress of the budget

### Alerts

Additionally, emailed alerts will be sent to the C&F Operations group, if you do not have access please reach out to the team.

These alerts are sent for given criteria, as seen below:

**Subscription Cost:**
- Dev (Test):
  - Alerts at 80%, forecasted > 100%, forecasted > 110%
  - Budget is £100 per month
- Test (Staging):
  - Alerts at 80%, forecasted > 100%, forecasted > 110%
  - Budget is £50 per month
- Production:
  - Alerts at 80%, forecasted > 100%, forecasted > 110%
  - Budget is £300 per month

**Resource - Azure AI Translate Cost:**
- Dev (Test):
  - Alerts at 80%, forecasted > 100%, forecasted > 110%
  - Budget is £10 per month
- Test (Staging):
  - Alerts at 80%, forecasted > 100%, forecasted > 110%
  - Budget is £10 per month
- Production:
  - Alerts at 80%, forecasted > 100%, forecasted > 110%
  - Budget is £100 per month

## What to do if they exceed?

Should budgets for the month exceed, inform the developers and wider team to inform them of an increased cost. 

Dependent on the quantity of increase will determine next steps.