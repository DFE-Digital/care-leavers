import { test, Browser, BrowserContext, Page, expect, chromium, firefox, webkit, BrowserType } from '@playwright/test';
import { UnaccompaniedAsylumSeekingPage } from '../pages/UnaccompaniedAsylumSeekingPage';

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

test.describe('Unaccompanied Asylum Seeking Page Tests', () => {

    test.beforeEach(async ({ page }) => {
        const asylumSeekingPage = new UnaccompaniedAsylumSeekingPage(page);
        await asylumSeekingPage.openUnaccompaniedAsylumSeekingPage();
    });

    test('should verify page structure with heading and body sections', async ({ page }) => {
        const asylumSeekingPage = new UnaccompaniedAsylumSeekingPage(page);
        await asylumSeekingPage.assertPageElements();
    });

});

// Cleanup after tests
test.afterAll(async () => {
    await context.close();
    await browser.close();
});
