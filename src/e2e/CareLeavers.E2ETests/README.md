# Playwright E2E Testing Framework

This repository contains an End-to-End (E2E) automation framework built with Playwright and TypeScript. The framework supports cross-browser testing on all major browsers (Chromium, Firefox, WebKit) and mobile emulation for devices like Pixel 8 and iPhone 16. It is designed for scalability, maintainability, and ease of use.

## Features

- **Page Object Model (POM)** for maintainable test design.
- **Parallel test execution** for faster feedback.
- **Tracing, screenshots, and video recording** for debugging.
- **Configurable retries and timeouts**.
- **Detailed HTML reports**.
- **Cross-browser support** (Chromium, Firefox, WebKit).
- **Mobile device testing**.

## Project Structure

playwright-e2e-framework/
|
├── src/
│   ├── pages/                # Page Object Models
│   │   └── example.page.ts
│   ├── tests/                # Test files
│   │   └── example.spec.ts
│   ├── helpers/              # Utility functions
│   │   └── utilities.ts
│   ├── tasks/                # Reusable task-based functions
│       └── performAction.ts
|
├── playwright.config.ts      # Playwright configuration file
├── tsconfig.json             # TypeScript configuration
├── package.json              # Node.js dependencies
├── .eslintrc.json            # ESLint configuration
├── .prettierrc               # Prettier configuration
├── .gitignore                # Files to ignore in version control



## Getting Started

### Prerequisites

- **Node.js** (LTS version recommended)
- **yarn** package manager

### Installation

1. Clone the repository:

    git clone <repository-url>
    cd playwright-e2e-framework

2. Install dependencies:

    yarn install

3. Install Playwright browsers:

    npx playwright install

## Running Tests

### Run All Tests

yarn playwright test

### Run a Specific Test

yarn playwright test src/tests/example.spec.ts

### Run Tests in a Specific Browser

yarn playwright test --project=Chromium

### Run Tests in Debug Mode

yarn playwright test --debug

### Reporting and Debugging

### View Test Report

After running tests, generate and view an HTML report:

yarn playwright show-report

### Screenshots and Videos

Screenshots: Captured on failures.

Videos: Retained on test failures (configurable in playwright.config.ts).


### Configuration

Playwright Config (playwright.config.ts)

Key settings:

testDir: Directory containing test files.

timeout: Global timeout for each test.

retries: Number of retries on failure.

projects: Defines configurations for browsers and devices.

use: Default settings (e.g., trace, screenshot, video).

### Contribution Guidelines

1. Fork the repository.

2. Create a new branch for your feature/bug fix.

3. Commit changes with clear messages.

4. Submit a pull request for review.

### License

This project is licensed under the MIT License. See the LICENSE file for details.

### Support

For questions or issues, please open an issue on the repository or contact the maintainer.