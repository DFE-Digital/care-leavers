# Care Leavers

The service aims to bring together and simplify relevant information for young people leaving care, and their intermediaries, 
to make them aware of, and help them understand the services and support available to them so they can navigate independent life to meet their diverse needs.

Project wide documentation can be found at: https://dfe-digital.github.io/care-leavers

## Rerequisites

To setup the development environment, restore the required .Net tools:

```bash
dotnet tool restore
dotnet husky install
```

This will install the tools specified in `.config/dotnet-tools.json`, including Husky.Net for pre-commit hooks. Commits run a GitLeaks pre-commit scan via Husky. The hook will use a pinned GitLeaks 8.30.0 binary and download it into a user cache directory if it is not already available on your machine. The auto-install path currently supports macOS and Linux on x64 and arm64, plus Windows on x86.

If you need to refresh the repository baseline for tracked features, run:

```bash
dotnet pwsh ./scripts/security/run-gitleaks.ps1 -Mode Baseline
```

If GitLeaks blocks a commit and you are certain the finding is expected. update `.gitleaks.toml` or regenerate `.gitleaks.baseline.json`.

For urgent one-off commits only, you can bypass the local scan with:

```bash
CL_SKIP_GITLEAKS=1 git commit
```

## Running the project

The site is made up of various components, each with their own README files detailing setup.

- Website: [README](./src/web/README.md)
- End-to-end tests: [README](./src/e2e/CareLeavers.E2ETests/README.md)
- Terraform: [README](./src/infrastructure/terraform/README.md)
- Contentful Migration: [README](./src/contentful/CareLeavers.ContentfulMigration/README.md)
- Release Process: [README](./docs/developers/Release-Process.md)
