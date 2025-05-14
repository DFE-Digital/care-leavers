# Azure Front Door

To protect the site from intrusion and general threats, as well as handling things like Geoblocking and SSL certifictes, we are using Front Door combined with the Web Application Firewall

## Geoblocking

The site is only intended for access from within the United Kingdom, therefore we have leveraged the Geoblocking functionality within Azure's Web Application Firewall (WAF) to dissuade users outside of the United Kingdom from accessing the site.

This has worked well, however, we came up against some problems.

### Contentful

Contentful's SaaS offering is currently hosted within Europe, but not necessarily within the UK, therefore we have put a rule in to always allow access from Contentful by identifying the `X-Contentful-CRN` header

### Search Engines

Many search engine spiders exist outside the UK, therefore we have had to put some exclusions into the WAF rules.

As IP ranges can change, we chose to allow search engines to bypass the geoblocking by utilising Regular Expressions on the User Agent of the search bot:

```regexp
aolbuild|baidu|bingbot|bingpreview|msnbot|duckduckgo|adsbot-google|googlebot|mediapartners-google|teoma|slurp|yandex|yahoo
```

The rule does not allow search engines to index translated or PDF content on the site, plus pages that are not meant to be indexed are excluded using `robots.txt` rules and Robots Meta tags within the HTML of the site.

### Social Media

For the same reasons above, many social media platforms are also outside of the UK, but the sites themselves often "scrape" Open Graph information from the page being shared.
To support this, we have also allowed social media bots to access these URLS 

```regexp
facebookbot|facebookexternalhit|facebookscraper|twitterbot|meta-externalagent|meta-externalfetcher|microsoftpreview|linkedinbot|pinterest|redditbot|telegrambot|mastadon|duckduckbot
```

### AI

We have allowed some AI tools to scrape the site to gather information, but have blocked access to translated and PDF content in order to protect the site from the costs incurred by translation and PDF generation.

The following AI bots have been allowed:

- OpenAI
- ChatGPT
- Cohere
- Google (Including Gemini)
- Amazon (Alexa etc)
- Apple (Siri)
- Duck AI Chat (DuckDuckGo)

```regexp
oai-search|chatgpt|gptbot|cohere-ai|google-extended|amazonbot|applebot|duckassistbot
```

### DfE Tools

A number of tools are used within DfE which allow Civil Servants and Contractors to share information.
These tools are also hosted outside of the UK, so we have included exemptions to the geoblocking for the followign tools:

- Slack
- Office 365
- LucidSpark
- Figma

The user agents we have unblocked are as follows:

```js
["slack", "embedly", "figma", "skype"]
```

## Threat Blocking

### Front Door Premium
Azure Front Door Premium is utilised to handle threat blocking, bot detection, and ensure the site mitigates the latest OWASP threats with as little intervention as possible.

### The National Archive
As a gov.uk site, the site was crawled by [The National Archive](https://www.nationalarchives.gov.uk/) as part of the UK Government Web Archive.
Their crawler, however, ignores the robots.txt file by default, which resulted in the site hitting rate limits for translation and PDF generation.

We have reached out to the service, who have agreed to exclude the URLs specified in our robots.txt, however while we waited for this to happen, we also placed a restriction on the Mirror Bot scraping translated and PDF content
