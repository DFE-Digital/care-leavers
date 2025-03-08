import { test, expect } from '@playwright/test';
import { BasePage } from '../pages/BasePage';
import { breadcrumbTestData } from '../helpers/urls-to-check';

test.describe('Breadcrumbs Functionality', () => {
    let basePage: BasePage;

    test.beforeEach(async ({ page }) => {
        basePage = new BasePage(page);
        await basePage.waitForPageLoad();
    });

    // Test that breadcrumbs are not visible on the Home page
    test('Breadcrumbs should not be visible on the home page (/)', async ({ page }) => {
        await basePage.navigateTo('/'); // Navigate to the homepage
        await basePage.waitForPageLoad();
        const breadcrumbItems = basePage.getBreadcrumbItems();
        const breadcrumbCount = await breadcrumbItems.count();
        expect(breadcrumbCount).toBe(0); // Ensure breadcrumbs are not visible on Home Page
    });

    // Iterate over each group of Test Data and Validate breadcrumbs for each URL
    for (const { urls, expectedBreadcrumbs } of breadcrumbTestData) {
        urls.forEach((url) => {
            test(`Breadcrumbs for ${url} should be visible, correctly formatted, and clickable`, async ({ page }) => {
                // Skip breadcrumb check for the home page
                if (url === '/') {
                    return; // Skip further checks for the home page
                }
                await basePage.checkBreadcrumbs(url, expectedBreadcrumbs);
            });
        });
    }
});
