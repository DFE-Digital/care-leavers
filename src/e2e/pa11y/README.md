# Pa11y

## Introduction

Pa11y CI is a command line tool which runs accessibility tests on your pages. It has been configured to run against 
the Azure hosted site from GitHub actions deployment validation pipeline.

It uses the Pa11y Docker image to run the tests and outputs the results to the console.

The tests run against WCAG 2.2 AAA standards. There are some exceptions to this which are detailed below.

## Running locally

To run the tests locally, ensure yarn is installed and run the following command in this directory:

```bash
yarn install

yarn pa11y-ci --config pa11y.json <endpoint>
```

You can also run against a sitemap by running:

```bash
yarn pa11y-ci --config pa11y.json --sitemap <sitemap-url>
```

## Known Exceptions

GOV UK Links Contrast ratio does not meet 7:1 ratio. https://github.com/alphagov/govuk-design-system/issues/4011