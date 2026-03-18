# Synchronisation Tool

This is a small utility tool that takes content from one environment and updates another.

This project primarily uses it to update our Test environment with what's in Production, as they fall out of sync due to external changes. This gives us more confidence when testing as we can more accurately see how new content would look in a Production environment.

This is currently triggered via a manual dispatch GitHub Action.

## Running

- Copy `.env.example` to `.env`
- Update the values in `.env`
- Run `npm run sync` in the terminal
