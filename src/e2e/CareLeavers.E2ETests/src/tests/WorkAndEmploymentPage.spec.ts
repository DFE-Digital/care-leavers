import { test, Browser, BrowserContext, Page, expect, chromium, firefox, webkit, BrowserType } from '@playwright/test';
import { WorkAndEmploymentPage } from '../pages/WorkAndEmploymentPage';

let browser: Browser;
let context: BrowserContext;

test.beforeAll(async ({ browserName }) => {
    const browserType: BrowserType<{}> = { chromium, firefox, webkit }[browserName];

    if (!browserType) {
        throw new Error(`Unsupported browser: ${browserName}`);
    }

    browser = await browserType.launch();
    context = await browser.newContext();
});

test.describe('Work and Employment Page Tests', () => {

    test.beforeEach(async ({ page }) => {
        const workAndEmploymentPage = new WorkAndEmploymentPage(page);
        await workAndEmploymentPage.openWorkAndEmploymentPage();
    });

    test('should verify page structure without validating content', async ({ page }) => {
        const workAndEmploymentPage = new WorkAndEmploymentPage(page);
        await workAndEmploymentPage.assertPageElements();
    });

});

// Cleanup after tests
test.afterAll(async () => {
    await context.close();
    await browser.close();
});
