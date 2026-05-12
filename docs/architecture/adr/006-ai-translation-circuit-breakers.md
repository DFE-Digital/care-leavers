# Decision - 0006 - AI Translation Circuit Breakers

## Context and Problem Statement

Following the discovery of a bot not abiding by the robots.txt and abusing the translation features of the site, circuit breakers were required to be put in place. The following issues occured as a result:
- The bot translated the site into all avilable languages, causing several thousand being spent - AI translation can be costly and therefore it is essential that appropriate restrictions are put in place to prevent the misuse and overuse of the functionality.
- When combined with translating resources into all potential languages, the PDF generator hit its monthly limit in a short period of time, making the tool unavailable for other users of the site.

## Considered Options

We considered the following options, ensuring the functionality would be available to users that needed it, but restricted to what was deemed 'reasonable' use:
- Block bots from being able to access translation
- Limit the number of languages that can be translated per session
- Limit the amount of PDFs that can be generated per session
- Reduce the number of langauges that can be translated to


## Decision Outcome

Based on analysis performed and discussions within the team, we chose a combination of the following offerings, with a longer term solution being considered for PDF generation.

The outcome determined included:
- Blocking bots from accessing translation
  - Some bots are allowed to the site explicitly as part of a social media rule on the WAF
  - These bots have been explicitly denied access to the translation resource, as they should not need to translate any resources
- Limiting the amount of translations per session
  - It was determined that there should be "reasonable" use of the translation tool, with a user not needing to translate more than 5 unique languages within a given 24 hour session
  - Users will be able to revisit the 5 translated languages as much as they wish to within the given 24 hour session
  - An error page will appear when a user attempts a 6th translation (or more) stating they are unable to translate the site further until x time the following day
- Limiting the amount of PDFs generated
  - This is part of a shorter term solution for PDF generation
  - To ensure the amount of PDFs generated do not exceed the allocated free tier limit, a user will be able to generate up to 10 PDFs in a given session
  - Longer term, the PDF generation model will be adjusted to remove the use of a third-party tool

While it was considered to drop the quantity of languages offered, there is insignificant research in relation to the Care Leavers project as to which languages are most utilised by users on the site. If further research is conducted, this feature may be implemented in future.

### Considerations on selected technology

This was approved in principal in discussions with the developers, the Technical Architect, the PM and DM on the Care Leavers team.
