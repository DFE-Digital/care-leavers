import { test, expect } from '@playwright/test';
import { BasePage } from '../pages/BasePage';
import { commonPagesToTest, helplineLinksToTest } from '../helpers/urls-to-check';

// Defining a hook that runs before each test to create the basePage
test.describe('Shared Website Functionalities', () => {
    let basePage: BasePage;

    test.beforeEach(async ({ page }) => {
        // Instantiate BasePage for each test
        basePage = new BasePage(page);
    });

    // Test to validate WebsiteNameLink 
    test.describe('Navigation Links Functionality', () => {
        commonPagesToTest.forEach((path) => {
            test(`Validate navigation links on ${path}`, async () => {
                // Navigate to page & check that WebsiteNameLink is visible
                await basePage.navigateTo(path);
                await expect(basePage.WebsiteNameLink).toHaveText(/Support for/i);
                await expect(basePage.WebsiteNameLink).toBeVisible();
            });
        });
    });

    // Test to validate navigation bar links including Menu button on Mobile Devices
    test.describe('Navigation Bar Functionality', () => {
        commonPagesToTest.forEach((path) => {
                test(`Validate Navigation Bar works for each link on ${path}`, async ({ page }) => {  

                    // Check if the device is desktop or mobile
                const isDesktop = page.viewportSize()?.width ? page.viewportSize()!.width > 600 : false;
                await basePage.navigateTo(path);
                await basePage.verifyNavigation(isDesktop);
            });
        });
    });

    // Test to validate footer links
    test.describe('Footer Links Functionality', () => {
        commonPagesToTest.forEach((path) => {
            test(`Validate footer links on ${path}`, async () => {
                // Navigate to page & check that footer links are visible
                await basePage.navigateTo(path);
                await expect(basePage.footer).toContainText("Open Government Licence v3.0");
                await basePage.verifyFooterLinks();
            });
        });
    });

    /*commented for now until the mocks are updated to include this 
    // Test to validate helpline links
    test.describe('Helpline Links Functionality', () => {
        helplineLinksToTest.forEach((path) => {
            test(`Validate helpline links on ${path}`, async () => {
                // Navigate to page & check that helpline links are visible
                await basePage.navigateTo(path);
                await expect(basePage.ifYouNeedHelpSection).toBeVisible();
                await expect(basePage.helplineLink).toHaveAttribute('href', 'helplines');
                await expect(basePage.helplineLink).toBeVisible();
            });
        });
    });
    */
});
