# Contentful Development Process

When adding to or modifying content models in Contentful, there is a process to follow as a developer to ensure code, tests and the CMS are all in sync.

### 1. Make Changes in Development (Contentful)

In Contentful, make sure you've switched to the Development environment. Do all the changes you need to do in there.

### 2. Update Models (Code)

- In `src/web/CareLeavers.Web/Models` you will have to modify the models to reflect your changes in Contentful
- In the rest of the codebase you will need consume your changes (such as in the relevant `.cshtml` files, renderers etc)

### 3. Create new Contentful Migration

- In `src/contentful/CareLeavers.ContentfulMigration/Migrations` you need to create a new migration following the pattern in there.
- You need to replicate your changes in code. Use the other files as guidance. You can JSON Preview the Content Models and find your changes to see what values and equivalent functions to call. 

Unfortunately this is a manual process as the Contentful CLI or website has no mechanism to generate migrations.

### 4. Update Integration Tests

The Integration (i.e., Snapshot) Tests are located in `src/web/tests/CareLeavers.Integration.Tests`.

Inside `SnapshotTests` you'll see two folders - `Input` and `Output`.

The snapshot tests combine the JSON files inside of `Input` and output an HTML page in `Output` - this is supposed to reflect pulling data from Contentful (The JSON) and rendering a webpage after parsing it (The HTML).

Essentially it tests if the content is being rendered correctly, and that content changes don't have any side effects on other content.

When changing the content models, you'll need to add or change the existing JSON files inside of `Input` to reflect your changes.

When done, inside of `SnapshotTests.cs` there is a test `GenerateSnapshots` that you need to manually run. It will create the Output files and test them. Note that this is manual, they are **not** executed by means of `dotnet test` on the solution etc. This will mean the `AssertSnapshots` tests will be testing accurate data.

**Note** - for changes to the `Configuration` content model, you change `TestSupport/MockContentfulConfiguration.cs` instead of the JSON files as an edge case.