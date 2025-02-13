# Deployment Architecture

The build and release pipelines are all controlled via GitHub actions which act as our CI/CD process.

The actions are categorised into different types:
- **Validate**: Perform some sort of validation, such as running tests and are typically pass/fail
- **Generate**: Generate some sort of artifact, such as reporting
- **Deploy**: Perform a deployment to a target environment

## Build Pipeline

On creation of a Pull Request or push to main, multiple validation actions run, depending on what areas of the repo
have been modified. Any failures should be addressed before merging. Failures on main should be fixed as priority.

## Release Pipeline

Once the team is happy to release, they can request this via workflow dispatch. This will trigger the deployment pipeline 
and run the following steps:

```mermaid
%%{ init: { 'flowchart': { 'curve': 'step' } } }%%
flowchart TD
    accDescr: Deployment process flow
    
    A["Build Docker Image"]-->B["Push Docker Image to Registry"]
    B-->C["Provision Terraform Infrastructure"]
    C-->D["Push Docker Image to Azure Web App"]
    D-->E["Request Deployment Slot Swap"]
    E-->F{Is slot warmed up?}
    F--No-->F
    F-->H["Deployment Slot Swapped"]
```

## Contentful Models Deployment

Contentful Models are deployed via Contentful Migrations through the Contentful CLI. Because contentful and app service environments
are not one-to-one, the release process is managed seperately.

### App Service to Contentful Environment Mapping
```mermaid
%%{ init: { 'flowchart': { 'curve': 'stepAfter' } } }%%
flowchart TD
    accDescr: "App Service to Contentful Environment Mapping"
    
    A["Local Dev Environment"]-->CDev["Contentful - Dev"]
    B["App Service - Test"]-->CStag["Contentful - Staging"]
    C["App Service - Staging"]--Preview-->CProd["Contentful - Production"]
    D["App Service - Production"]--Published-->CProd
```


### Contentful Migration Process
```mermaid
%%{ init: { 'flowchart': { 'curve': 'stepAfter' } } }%%
flowchart TD
    accDescr: "Contentful Migration Process"
    
    A["Install the CLI"]-->B["Login to Contentful"]
    B-->C["Running Migrations .NET App"]
    C-->D["Foreach migration"]
    D-->E{Is migration 
    already applied?}
    E--No-->G{Any migrations left?}
    E--Yes-->F["Run Migration"]
    F-->G
    G--Yes-->D
    G--No-->H["Migration Complete"]
```
