import { test, expect } from '@playwright/test';
import { BasePage } from '../pages/BasePage';
import {
    prioritisedListOfCommonPagesToTest,
    helplineLinksToTest,
    metaDataLinksToTest,
    shareAndPrintLinksToTest
} from '../helpers/urls-to-check';

// Defining a hook that runs before each test to create the basePage
test.describe('Shared Website Functionalities', () => {
    let basePage: BasePage;

    test.beforeEach(async ({page}) => {
        // Instantiate BasePage for each test
        basePage = new BasePage(page);
    });

    // Test to validate WebsiteNameLink, navigation bar links including Menu button on Mobile Devices and footer links
    test.describe('Page Elements Validation', () => {
        prioritisedListOfCommonPagesToTest.forEach((path) => {
            test(`Validate main elements like navigation links ,Navigation Bar and FooterLinks  on ${path}`, async ({page}) => {
                await basePage.navigateTo(path);

                // Validate Website Name Link
                await expect(basePage.WebsiteNameLink).toHaveText(/Support for/i);
                await expect(basePage.WebsiteNameLink).toBeVisible();
                
                // Validate logo has accessible name
                await expect(basePage.logoLink).toHaveAccessibleName(/Support for/i)
                await expect(basePage.logoLink).toBeVisible();

                // Validate Navigation Bar
                const isDesktop = page.viewportSize()?.width ? page.viewportSize()!.width > 600 : false;
                await basePage.verifyNavigation(isDesktop);

                // Validate Footer Links
                await expect(basePage.footer).toContainText("Open Government Licence v3.0");
                await basePage.verifyFooterLinks();
            });
        });
    });

    // Test to validate Social Media Share and Print Buttons 
    test.describe('Social Media Share and Print Buttons Visibility', () => {
        shareAndPrintLinksToTest.forEach((path) => {
            test(`Verify share and print buttons are visible on ${path}`, async ({ page }) => {
                const basePage = new BasePage(page);

                // Navigate to the page and Verify all share and print buttons are visible
                await basePage.navigateTo(path);
                await basePage.verifyShareButtonsVisibility();
            });
        });
    });

    // Test to validate Metadata of page is visible
    test.describe('Metadata Visibility Across Multiple Pages', () => {
        metaDataLinksToTest.forEach((path) => {
            test(`Verify metadata is populated on ${path}`, async ({ page }) => {
                const basePage = new BasePage(page);

                // Navigate to the page and Verify all share and print buttons are visible
                await basePage.navigateTo(path);
                await basePage.verifyMetadataIsPopulated();
            });
        });
    });

    // Test to validate helpline links
    test.describe('Helpline Links Functionality', () => {
        helplineLinksToTest.forEach((path) => {
            test(`Validate helpline links on ${path}`, async () => {
                // Navigate to page & check that helpline links are visible
                await basePage.navigateTo(path);
                await expect(basePage.ifYouNeedHelpSection).toBeVisible();
                await expect(basePage.helplineLink).toHaveAttribute('href', '/en/helplines');
                await expect(basePage.helplineLink).toBeVisible();
            });
        });
    });
});
    