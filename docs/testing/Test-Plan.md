# Test Plan for DfE Care Leavers Website 

## Revision/Document History

| Version | Date          | Author             | Reviewer(s) | Approver | Description of Changes |
|---------|---------------|--------------------|-------------|----------|------------------------|
| 1.0     | [21/03/2025]  | Varsha Krishnamurthy |             |          | Initial draft of the test plan |
| 1.1     | [DD/MM/YYYY]  |                    |             |          | [Brief description of updates] |

## Introduction

### Overview

The Care Leavers website is designed to support young people transitioning out of care by providing access to critical information and services. The site incorporates technologies like Contentful CMS, Redis Cache, and Translation APIs to deliver a user-centric experience.

Young care leavers often face significant challenges, such as navigating education, employment, and training (EET) options while dealing with unstable housing, limited support networks, and mental health struggles. This test plan aims to ensure the website meets the needs of care leavers by addressing their unique circumstances.

### Document Purpose

This test plan outlines a comprehensive strategy for validating both functional and non-functional requirements of the Care Leavers website. The platform is designed to address the real-world challenges faced by care leavers during their transition to independence, including:

- **Difficult Transitions**: Care leavers often face abrupt transitions into adulthood, frequently feeling unprepared and unsupported, which can hinder their ability to make informed decisions regarding education, employment, and training (EET). The website aims to provide clear guidance on EET options, considering the complexities of housing and independent living.
- **Rights and Entitlements**: A significant number of care leavers are unaware of their rights and the support available to them, which can be a barrier to accessing EET opportunities. The website intends to provide clear, accurate, and accessible information to help care leavers understand and exercise their entitlements.
- **Access to Housing and Support**: Stable housing and financial security are crucial for care leavers to pursue EET opportunities. The platform aims to connect users with relevant resources to mitigate challenges related to accommodation and financial instability.
- **Inconsistent Support**: The level of support from local authorities can vary, leading to gaps in assistance for care leavers. The website strives to serve as a consistent and reliable source of information, bridging these gaps and providing timely support.
- **Instability in Pathways**: Care leavers often experience disrupted educational and employment pathways due to instability in their lives. The platform aims to offer tailored guidance to help users navigate and re-engage with EET opportunities when they are ready.
- **User-Facing Information, Advice, and Guidance (IAG)**: While various charities provide support for care leavers, there is a need for a centralized, credible, and up-to-date source of information. The website seeks to complement existing resources by offering comprehensive IAG to empower care leavers in their transition.

By addressing these critical areas, this test plan aims to ensure that the Care Leavers website effectively supports its users, enabling them to navigate their transition to independence with confidence and resilience.

## Testing Objectives

- Validate that all functionalities meet the business requirements.
- Ensure seamless integration between the web application and external systems.
- Verify the responsiveness and performance of the website.
- Identify and resolve defects before release.
- Focus on user-centric testing, prioritizing accessibility and usability.
- Validate mobile-first design responsiveness.

## Testing Scope

### In-Scope:
- **Functional Testing**: Validating website features, content retrieval from Contentful, and API-driven functionality.
- **UI/UX Testing**: Ensuring user-friendly navigation, intuitive content display, and adherence to design guidelines.
- **Mobile Responsiveness**: Testing on multiple devices and screen sizes.
- **Download, Save, and Share functionality for documents.**
- **Integration Testing**:
  - Contentful CMS (data fetching and rendering).
  - Translation Services (Google & Azure Translate API).
  - Redis Cache.
  - API Testing (validation of request/response structure).
  - API Failovers handled gracefully.
- **Performance & Security Testing** (basic validation; full-fledged testing may be scheduled later).
- **Browser Compatibility Testing**: Ensuring support across:
  - Chrome (latest 2 versions)
  - Edge (latest 2 versions)
  - Firefox (latest version)
  - Safari-webkit
  - iOS (Safari, latest version)
  - Android (Chrome, latest version)

### Out-of-Scope:
- **Contentful Infrastructure Testing**: The internal architecture and hosting of Contentful CMS are outside the testing scope. Only API responses and data retrieval will be validated.
- **Third-Party Translation API Behavior Changes**: Any internal changes or performance variations in Google/Azure Translate APIs are beyond the scope, except for their integration with the Care Leavers website.

## Test Approach

### Testing Methodologies

#### Manual Testing:
- Exploratory tests.
- Smoke and sanity tests.

#### Automated Testing:
- UI/UX testing.
- End to End Testing.
- Regression testing.

### Tools:
- Playwright with TypeScript
- Postman
- Lighthouse
- Pa11y

### Testing Phases

#### 1. Unit Testing:

**Developer Responsibility:**
- Cover assigned tickets with unit tests.
- Mark tickets as “Dev done” only after completing unit tests.

**Guidelines:**
- Tests should focus on individual functions or classes.
- Test names should follow a clear naming convention (e.g., Method_Condition_ExpectedOutcome).
- Only public methods and interfaces should be tested.
- Tests must be self-contained and repeatable.

**Continuous Integration (CI):**
- Run unit tests automatically with each code check-in.
- Halt CI pipeline on test failure.

#### 2. Component Integration Testing:

**Developer Responsibilities:**
- Verify integration tests cover interactions between components, services, and external dependencies.

**Guidelines:**
- Focus on testing interactions between modules rather than isolated units.
- Use clear naming conventions (e.g., ComponentA_CallsComponentB_ExpectedOutcome).
- Mock dependencies where appropriate but verify real service interactions when necessary.

**Continuous Integration (CI):**
- Integration tests must run automatically as part of the CI pipeline.
- Any integration test failure should halt the CI pipeline.

#### 3. End-to-End Testing:

**QA Responsibilities:**
- Design and execute E2E tests covering critical user journeys within the Care Leavers website.
- Use Playwright with TypeScript to automate tests across different browsers and devices.
- Validate UI interactions, Contentful-driven content rendering, and the user journeys.

**Continuous Integration (CI):**
- Run E2E tests as part of the CI/CD pipeline, triggered on major deployments.
- Fail pipeline execution if critical user journey tests fail.

#### 4. Adhoc Testing

**QA Responsibilities:**
- Conduct manual exploratory, smoke, and sanity testing to uncover edge cases and unexpected behaviours.
- Perform context-driven testing beyond scripted automation to identify usability, accessibility, and performance issues.

### Process & Best Practices:
- Use session-based testing to ensure structured yet flexible exploration.
- Take detailed notes and screenshots of anomalies to assist in defect reproduction.
- Communicate findings quickly with developers to facilitate fast resolutions.

#### Non-Functional Testing:

**Accessibility Testing:**
- Validate compliance with WCAG 2.1 AA standards using tools like Lighthouse and Pa11y.
- Consider user scenarios for keyboard-only users, low vision users, and those with learning difficulties.

**Performance Testing:**
- Evaluate scalability by simulating high-traffic scenarios. This will be revisited later.

**Security Testing:**
- Penetration testing to identify vulnerabilities such as SQL injection and XSS.
- Tools like OWASP ZAP will be used for automation.

### Definition of Done (DoD)
For a ticket or feature to be considered complete and ready for testing, the following criteria must be met:
- Code Peer Review: All code must be peer-reviewed and approved.
- Feature Deployed to Test Environment.
- Acceptance Criteria met.
- No Critical Defects.

## Test Environments

| Environment | Purpose                         |
|-------------|---------------------------------|
| Development | Initial testing.               |
| Testing     | Shared QA environment for functional tests. |
| Staging     | Pre-production validation.     |
| Production  | Post-deployment testing.       |

## Risks and Dependencies

- **Risks**: Delays in third-party API responses, cache inconsistencies, security vulnerabilities.
- **Dependencies**: Fully functional Contentful CMS and API services, Access to Redis Cache and Translation APIs.

## Best Practices
- Foster collaboration between developers, testers, and stakeholders.
- Leverage automation for repetitive tasks.
- Maintain detailed documentation for reproducibility.

## Deliverables

- Testing Metrics and Reports
- Test Scripts (for Automation)
- Test Execution Logs
- Reference Materials

## Acronyms

- CMS: Content Management System
- CI/CD: Continuous Integration/Continuous Deployment
- API: Application Programming Interface
- WCAG: Web Content Accessibility Guidelines
