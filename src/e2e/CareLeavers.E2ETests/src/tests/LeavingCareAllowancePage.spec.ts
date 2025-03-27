import { test, Browser, BrowserContext, chromium, firefox, webkit, BrowserType } from '@playwright/test';
import { LeavingCareAllowancePage } from '../pages/LeavingCareAllowancePage';

let browser: Browser;
let context: BrowserContext;

test.beforeAll(async ({ browserName }) => {
    const browserType: BrowserType = { chromium, firefox, webkit }[browserName];

    if (!browserType) {
        throw new Error(`Unsupported browser: ${browserName}`);
    }

    browser = await browserType.launch();
    context = await browser.newContext();
});

test.describe('Leaving Care Allowance Page Tests', () => {

    test.beforeEach(async ({ page }) => {
        const leavingCareAllowancePage = new LeavingCareAllowancePage(page);
        await leavingCareAllowancePage.openLeavingCareAllowancePage();
    });

    test('should verify page structure without validating content', async ({ page }) => {
        const leavingCareAllowancePage = new LeavingCareAllowancePage(page);
        await leavingCareAllowancePage.assertPageElements();
    });

});

// Cleanup after tests
test.afterAll(async () => {
    await context.close();
    await browser.close();
});
