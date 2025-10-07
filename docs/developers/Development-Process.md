# Development Process

These are the preliminary development processes for developers and QAs. These processes are subject to continuous changes so it's advice to frequently review it for changes.

## Developer Development Process

1. Pick up ticket (3 amigos; optional)
2. Define branch.
   ```
   Format:
   
   e.g.
   
   feat/CARE-XXX/focus
   bug/CARE-XXX/focus
   ```
3. Run tests to make sure you know everything is working before you start.
4. Implement the necessary changes
    1. Content wise via Contentful development environment
    2. Content type wise via migration scripts. File name format: `00XX-description.cjs`
    3. Html changes via .cshtml
    4. Css class attachment to content types via .cshtml or ContentRenderer classes
5. Implement playwright tests and fix broken tests
6. Commit changes
    1. Message format: 
      ```
      CARE-XXX {message}
      
      {details}
      ```
7. Create pull request
8. Update e2e contentful environment
    1. Use `Deploy - Contentful Migrations` to run the new script in the e2e environment, use your branch as the source code
    2. Use `Deploy - Environment` to deploy the code changes made outside of the migration scripts
    3. Manually update content added in development to e2e
9. Rerun the checks in pull request, if failed due content differences in e2e environment
10. When pull request has been approved and merged, do step 8 for the test environment
11. Move the ticket to the Jira `Test` column and inform any QA/Testers.
12. QA/Tester to perform manual tests to see if the acceptance criteria was met.
13. Once QA/Test has signed off.
14. Update `production` contentful environment
    1. Use `Deploy - Contentful Migrations` to run the new script in the production environment, use your branch as the source code
    2. Use `Deploy - Environment` to deploy the code changes made outside of the migration scripts to the `staging` azure instance (staging uses published/draft pages in contentful)
    3. Use `Deploy - Environment` to deploy the code changes made outside of the migration scripts to the `production` azure instance (potentially; only uses published pages in contentful)
15. Inform Content Designers/anyone responsible for content in the Care Leavers MS Teams group chat, that the code changes have been deployed ready for her to apply the content changes.

## QA Test Suite Development Process

1. Define or Pick up ticket in Jira Test column
    1. Subtask to fix related tests (Ideally the responsibility of the Devs).
    2. Define general test fix, load testing, etc. ticket(s).
2. Move Jira ticket the 'In Progress'.
3. Define branch. Format:
   1. task/CARE-XXX/{focus} (for test fixes, modifications, etc.)
   2. test/CARE-XXX/{focus} (for running complex tests, e.g. performance, load, security, etc. testing)
4. Implement the necessary changes
   1. Add test modifications
   2. Potentially, update Contentful development, e2e and/or test environments
5. Commit changes
   1. Message format: "CARE-XXX {message}"
6. Create pull request
7. Move Jira ticket the 'Ready for Review (Tech)'.
8. Pull request approved and merged, by at least a Dev, QA or Technical Architecture
9. Mark ticket as 'Done'.site. Pass or fail. (In development)