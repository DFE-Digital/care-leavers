import { test, Browser, BrowserContext, chromium, firefox, webkit, BrowserType } from '@playwright/test';
import { HigherEducationBursaryPage } from '../pages/HigherEducationBursaryPage';

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

test.describe('Higher Education Bursary Page Tests', () => {

    test.beforeEach(async ({ page }) => {
        const higherEducationPage = new HigherEducationBursaryPage(page);
        await higherEducationPage.openHigherEducationBursaryPage();
    });

    test('should verify page structure without validating content', async ({ page }) => {
        const higherEducationPage = new HigherEducationBursaryPage(   page);
        await higherEducationPage.assertPageElements();
    });

});

// Cleanup after tests
test.afterAll(async () => {
    await context.close();
    await browser.close();
});
