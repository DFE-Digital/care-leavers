---
title: 008 - AI Translation - Kill Switch
layout: sub-navigation
sectionKey: Decisions
order: 8
includeInBreadcrumbs: true
eleventyNavigation:
  parent: Decisions
  key: 008 - AI Translation - Kill Switch
---

## Context and Problem Statement

Our application utilises the Azure AI Translator service to translate site content for users. Currently, we rely on a Fair Use Policy enforced at the user-session level, which limits the number of languages a user can request translations for within a 24-hour period.

While this deters casual abuse, it is highly vulnerable to malicious actors using simple scripts to generate new sessions, bypassing the policy. Because Azure AI Translator is billed on consumption (per character/document size), an automated attack could quickly rack up thousands of pounds in unexpected Azure costs. We need a robust "kill switch" or circuit breaker to cap our maximum financial exposure without overly degrading the user experience during normal operations.

## Options Considered

We considered the following options:

### Option A - Azure Function App polling Azure Cost Management API

- Run a scheduled Azure Function App to check the current accummulated spend on Azure Budget for the translation service. If the spend crosses a threshold, disable the service.
- Azure Cost Management data is often delayed by up to 24 hours. A high volume script could still rack up massive costs before the budget reflects the usage and triggers the kill switch.

### Option B - Native Azure AI Translate Circuit Breaker

- Rely on build-in Azure API Management or Foundry Services quotas.
- While Azure allows setting rate limits (call per second), it lacks flexibility, a native "hard financial stop" or monthly translation volume kill switch that seamlessly integrates with our application's custom UX for graceful degradation.

## Decision

We will implement a custom cicuit breaker in our backend code.

This will maintain a persistent record (in a database, cache or local file) of the total size of documents/characters translated across the service within the current calendar month.

Workflow:

1. Before making a call to the Azure AI Translator service, the backend checks the current monthly accumulated translation volume.
2. If the volume is below our defined safety limit, the translation proceeds, and the requested document size is added to the monthly tally.
3. If the volume is above the limit, the circuit breaker trips. The backend intercepts the request and prevents the call to Azure.
4. The user is served a graceful information/error page stating that the translation functionality is currently unavailable.

## Consequences

### Positive

- Provides an absolute, real-time hard cap on our monthly Azure translation costs, eliminating the risk of malicious users/scripts.
- Tripping the circuit breaker gives the engineering and security teams the necessary time to investigate the traffic anomaly, block malicious IPs/actors and remedy the situation.
- Once the attack is mitigated, the team can manually reset the circuit breaker or increase the limit to restore functionality.
- Ensure real-time accuracy of volume usage.

### Negative

- Because the limit is global, it could tempararily break translation features for legitimate users until the team intervenes.
- Requires building and maintainting the tracking logic, storage mechanism and the fallback UI/error handling in the application frontend.
