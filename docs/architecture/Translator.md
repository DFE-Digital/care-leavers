# Azure Translator

A quarter of care leavers are unaccompanied asylum-seeking young people. Many of these young people speak English as a second or other language. We use Azure Translator to improve the accessibility of the information on the site through translation.

## Integration

Pages are translated on a page by page basis into the language choses by the user. The translation is done via an API call to the Translator service.

## Hosting

Each subscription has its own instance of Azure Translator. To deploy and make changes to the resource it is necessary to get exemption from the Azure Policy which currently blocks all Foundry related resources.
