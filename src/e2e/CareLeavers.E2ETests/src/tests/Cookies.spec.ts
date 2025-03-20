import { test, expect } from '@playwright/test';
import { BasePage } from '../pages/BasePage';
import { prioritisedListOfCommonPagesToTest } from '../helpers/urls-to-check';

// Main test to validate cookie banner
test.describe('Cookie Banner Functionality', () => {
    prioritisedListOfCommonPagesToTest.forEach((path) => {
        test(`Validate cookie banner on ${path}`, async ({ page, context }) => {
            const basePage = new BasePage(page);

            // 1. Navigate to page & verify banner appears
            await basePage.navigateTo(path);
            await expect(basePage.cookieBanner).toBeVisible();

            // 2. Accept cookies & verify consent stored
            await basePage.acceptCookies();
            expect(await basePage.verifyConsentCookie(context)).toBeTruthy();

            // 3. Clearing cookies should trigger banner again
            await basePage.clearCookies(context);
            await page.reload();
            await expect(basePage.cookieBanner).toBeVisible();

            // 4. Simulate expired cookie & verify banner reappears
            await basePage.setExpiredConsentCookie(context);
            await page.reload();
            await expect(basePage.cookieBanner).toBeVisible();
        });
    });
});

// Test to reject cookies
test.describe('Reject Cookie Functionality', () => {
    prioritisedListOfCommonPagesToTest.forEach((path) => {
        test(`Reject cookie banner on ${path}`, async ({ page, context }) => {
            const basePage = new BasePage(page);

            // 1. Navigate to page & verify banner appears
            await basePage.navigateTo(path);
            await expect(basePage.cookieBanner).toBeVisible();

            // 2. Reject cookies
            await basePage.rejectCookies();

            // 3. Verify that the consent cookie is now set to 'no'
            const cookiesAfterReject = await context.cookies();
            const consentCookieAfterReject = cookiesAfterReject.find(cookie => cookie.name === '.AspNet.Consent');
            expect(consentCookieAfterReject).toBeDefined();  // Consent cookie should now exist
            expect(consentCookieAfterReject?.value).toBe('no');  // It should have the value 'no' after rejection
        });
    });
});
