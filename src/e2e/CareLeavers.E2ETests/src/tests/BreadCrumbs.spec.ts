import { test, Browser, BrowserContext, Page, expect, chromium, firefox, webkit, BrowserType } from '@playwright/test';
import { BasePage } from '../pages/BasePage';
import { breadcrumbTestData } from '../helpers/urls-to-check';

let browser: Browser;
let context: BrowserContext;
let page: Page;
let basePage: BasePage;

test.beforeAll(async ({ browserName }) => {
    const browserType: BrowserType<{}> = { chromium, firefox, webkit }[browserName];

    if (!browserType) {
        throw new Error(`Unsupported browser: ${browserName}`);
    }

    browser = await browserType.launch(); 
    context = await browser.newContext(); //Reuse context across tests
    page = await context.newPage();
    basePage = new BasePage(page);
});

test.describe('Breadcrumbs Functionality', () => {

    test.beforeEach(async () => {
        await basePage.waitForPageLoad(); 
    });

    // Test that breadcrumbs are not visible on the Home page
    test('Breadcrumbs should not be visible on the home page (/)', async () => {
        await test.step('Navigate to Home Page', async () => {
            await basePage.navigateTo('/en/home');
            await basePage.waitForPageLoad();
        });

        await test.step('Verify breadcrumbs are not visible', async () => {
            const breadcrumbItems = basePage.getBreadcrumbItems();
            const breadcrumbCount = await breadcrumbItems.count();
            expect(breadcrumbCount).toBe(0);
        });
    });
    
    for (const { urls, expectedBreadcrumbs } of breadcrumbTestData) {
        for (const url of urls) {
            test(`Breadcrumbs for ${url} should be visible, correctly formatted, and clickable`, async () => {
                if (url === '/' || url === '/en/home/') {
                    return; // Skip further checks for home page
                }

                await test.step(`Navigate to ${url}`, async () => {
                    await basePage.navigateTo(url);
                    await basePage.waitForPageLoad();
                });

                await test.step(`Verify breadcrumbs on ${url}`, async () => {
                    await basePage.checkBreadcrumbs(url, expectedBreadcrumbs);
                });
            });
        }
    }
});

//Cleanup 
test.afterAll(async () => {
    await context.close();
    await browser.close();
});
