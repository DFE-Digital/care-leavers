# Stopping The Site

## Overview
This document details how to temporarily stop the site, in case of something like a DDoS attack

## DfE Azure

All services are hosted in DfE Azure, therefore you will need someone with the relevant permissions to access the subscription the service has been hosted in.

To temporarily stop the site, you need to stop at least one of the below:

### Stop the Web App

- Stop the `web-app-service` service for that environment

### Disable the Front Door endpoint

- Find the front door endpoint for that environment
- Update endpoint
- Set the status to disabled

