import { test, expect } from '@playwright/test';
import { BasePage } from '../pages/BasePage'; 
import { pagesToTest } from '../helpers/urls-to-check'; 

test.describe('Cookie Banner Functionality', () => {
    pagesToTest.forEach((path) => {
        test(`Validate cookie banner on ${path}`, async ({ page, context }) => {
            const basePage = new BasePage(page);

            // 1. Navigate to page & verify banner appears
            await basePage.navigateTo(path);
            await expect(basePage.cookieBanner).toBeVisible();

            // 2. Accept cookies & verify consent stored
            await basePage.acceptCookies();
            expect(await basePage.verifyConsentCookie(context)).toBeTruthy();

            // 3. Reject cookies & verify no tracking cookie stored
            await basePage.navigateTo(path);
            await basePage.rejectCookies();
            expect(await basePage.verifyConsentCookie(context)).toBeFalsy();

            // 4. Clearing cookies should trigger banner again
            await basePage.clearCookies(context);
            await page.reload();
            await expect(basePage.cookieBanner).toBeVisible();

            // 5. Simulate expired cookie & verify banner reappears
            await basePage.setExpiredConsentCookie(context);
            await page.reload();
            await expect(basePage.cookieBanner).toBeVisible();
        });
    });
});
