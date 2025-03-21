import { test, Browser, BrowserContext, Page, chromium, firefox, webkit, BrowserType } from '@playwright/test';
import { AllSupportPage } from '../pages/AllSupportPage';
import { supportCards } from "../helpers/urls-to-check";

let browser: Browser;
let context: BrowserContext;
let page: Page;
let allSupportPage: AllSupportPage;

test.beforeAll(async ({ browserName }) => {
    //Get browser type based on Playwright config
    const browserType: BrowserType<{}> = { chromium, firefox, webkit }[browserName];

    if (!browserType) {
        throw new Error(`Unsupported browser: ${browserName}`);
    }

    browser = await browserType.launch(); //Launch browser once
    context = await browser.newContext(); //Reuse context across tests
    page = await context.newPage();
    allSupportPage = new AllSupportPage(page);
});

test.describe('All Support Page Tests', () => {

    test.beforeEach(async () => {
        await allSupportPage.openAllSupportPage(); // ✅ Open page before each test
    });

    test('should assert page elements are correct', async () => {
        await allSupportPage.assertPageElements();
    });

    test('should verify navigation for all support cards', async () => {
        await allSupportPage.verifySupportCardsNavigation(supportCards);
    });

    test('should verify "Know what support you can get" section', async () => {
        await allSupportPage.verifyKnowWhatSupportSection();
    });
});

//Cleanup after tests
test.afterAll(async () => {
    await context.close();
    await browser.close();
});
