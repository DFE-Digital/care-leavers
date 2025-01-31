# Branching strategy and code quality

## Branching strategy

This project uses the Trunk Based Development branching strategy.
This strategy seeks to avoid long-running feature/release branches and all work is based on ```main```.

Small tweaks and chore work can be done directly on ```main``` but more complex features including logic
should be done in a feature branch and merged back into ```main``` via a pull request.

Long-running feature branches that drift too far from ```main``` should be avoided to avoid merge conflicts.

Feature branches should be squashed and rebased onto main before being fast-forwarded into main.
This ensures an easy-to-follow git history and avoids merge commits.

## Code quality and rules

Before pushing to main/raising a pull request, ensure the following:
- The build is passing and any relevant tests are passing.
- The code is formatted correctly.
- Errant comments and debug code are removed.

The following checks/actions run on every pull request or main push:

- **Web .NET Build** - Builds the solution and runs unit/integration tests. Pass or fail.
- **SonarCloud** - Static code analysis with a pre-configured quality gate (Note: Currently under review)
- **Web E2E Tests** - Runs the end-to-end tests against the deployed site. Pass or fail. (In development)