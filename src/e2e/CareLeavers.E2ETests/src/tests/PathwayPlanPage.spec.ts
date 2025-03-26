import { test, Browser, BrowserContext, chromium, firefox, webkit, BrowserType } from '@playwright/test';
import { PathwayPlanPage } from '../pages/PathwayPlanPage';

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

test.describe('Pathway Plan Page Tests', () => {

    test.beforeEach(async ({ page }) => {
        const pathwayPlanPage = new PathwayPlanPage(page);
        await pathwayPlanPage.openPathwayPlanPage();
    });

    test('should verify page structure without validating content', async ({ page }) => {
        const pathwayPlanPage = new PathwayPlanPage(page);
        await pathwayPlanPage.assertPageElements();
    });

});

// Cleanup after tests
test.afterAll(async () => {
    await context.close();
    await browser.close();
});
