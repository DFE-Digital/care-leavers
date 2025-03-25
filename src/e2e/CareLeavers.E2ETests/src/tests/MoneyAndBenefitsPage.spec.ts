import { test, Browser, BrowserContext, Page, expect, chromium, firefox, webkit, BrowserType } from '@playwright/test';
import { MoneyAndBenefitsPage } from '../pages/MoneyAndBenefitsPage';

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

test.describe('Money and Benefits Page Tests', () => {

    test.beforeEach(async ({ page }) => {
        const moneyAndBenefitsPage = new MoneyAndBenefitsPage(page);
        await moneyAndBenefitsPage.openMoneyAndBenefitsPage();
    });

    test('should verify page structure without validating content', async ({ page }) => {
        const moneyAndBenefitsPage = new MoneyAndBenefitsPage(page);
        await moneyAndBenefitsPage.assertPageElements();
    });

});

// Cleanup after tests
test.afterAll(async () => {
    await context.close();
    await browser.close();
});
