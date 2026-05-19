# Decision - 0007 - PDF Generation

## Context and Problem Statement

Currently, the printable collection portion of the site uses a third-party service to generate PDFs.

During the bot crawling the site as described in [006](006-ai-translation-circuit-breakers.md), this also impacted the PDF generation as it blew through our monthly PDF usage limit.

We want to prevent this from happening again, as it impacts the user.

## Considered Options

We considered the following options:

- Local (i.e. on the server) PDF generation using a tool such as Gotenberg or IronPDF.
- Moving to client-sided PDF generation using the device's built-in mechanisms.

## Decision Outcome

We explored local PDF generation, and although while promising the free options are very costly in terms of implementation complexity, the easier implementations cost money.

Since this response came from essentially a denial-of-service from an impolite web crawler, we wanted to come up with a solution fairly quickly and so skipped exploring the paid options for the time being.

Whilst investigating, I came across the [GDS recommended approach for the printable collection pattern](https://design-guide.publishing.service.gov.uk/components/print-link/) - as described here if you have a multi-page guide the approach is to offer a printable version (which we technically already do) but it opens the page as normal and adds a print button at the top.

I decided it'd be better to align with GDS, and it also solves our problem as it can be done client sided. The site already falls back to a page like the above links if the third party service fails, so we just have to improve what is already offered.

### Considerations on Selected Technology

I consulted the dev team on the approach, and it was agreed that it'd be a sensible solution.

I then consulted the content & interaction designers and got their sign-off on the approach, and finally the project manager approved the approach.

There is one minor risk with this approach, which is the "Print this Page" button requires JavaScript to function. However, this button is just for convenience and just opens your devices print PDF dialogue for you - the user can still manually trigger this (e.g., CMD + P on macOS, CTRL + P on Windows, Share → Print on iOS, etc.) even if they have JavaScript disabled. There is also precedent for this button as it already exists on the site in other places.

In the long term, we still intend to resume exploring a locally generated approach, but we are happy for this solution to be in place for the short-medium term to manage risk.