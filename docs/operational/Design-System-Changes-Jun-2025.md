# Design System Changes - June 2025

## Overview

The site has been developed to take into account the GOVUK and DfE Branding changes that are due to be launched on June 25th 2025.

## Activating the new branding

The rebrand can be activated in one of two ways:

- If the current date is greater than, or equal to: June 25th 2025
- The parameter `REBRAND` is set to true within GitHub Actions and the site is redeployed

## Testing

The rebrand is active in both Integration and End-to-End testing to ensure that the tests don't break after the rebrand occurs.