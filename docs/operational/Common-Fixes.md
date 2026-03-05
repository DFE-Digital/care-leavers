# Common Fixes

## Overview
This document details common problems and how to fix them.

## Problems and Fixes

| Issue | Description | Fix |
|-------|-------------|:---:|
| Redeployed the app but old content is showing | Within the Test (Dev) and Production environments we implemenent caching via Azure Managed Redis. If you notice you have made changes but they are not displaying, it is likely a caching problem. | Firstly, clear the cache on the site by running the deploy-env workflow against the specific environment with "Clear Cache" set to true. If it is a specific page posing the problem, navigate to Contentful, select the correct environment and locate the page, you then need to unpublish and republish the page. |