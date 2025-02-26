import { Page,Locator, expect, BrowserContext, Cookie } from '@playwright/test';

export class BasePage {
    protected readonly page: Page;

    // Locators for cookie banner and buttons
    public readonly cookieBanner: Locator;
    public readonly acceptButton: Locator;
    public readonly rejectButton: Locator;

    constructor(page: Page) {
        this.page = page;
        this.cookieBanner = page.locator('.govuk-cookie-banner');
        this.acceptButton = page.locator('#accept-cookie');   
        this.rejectButton = page.locator('#reject-cookie');   
    }

    // Navigation method
    async navigateTo(url: string) {
        await this.page.goto(url, { waitUntil: 'networkidle' });
    }

    // Validate URL contains a specific path
    async validateURLContains(path: string) {
        await expect(this.page).toHaveURL(new RegExp(path));
    }

    // Wait for the page to load
    async waitForPageLoad() {
        await this.page.waitForLoadState('load');
    }
    
    // Cookie banner functionality
    async acceptCookies() {
        await this.acceptButton.click();
        await expect(this.cookieBanner).not.toBeVisible();
    }
    async rejectCookies() {
        await expect(this.rejectButton).toBeVisible({ timeout: 10000 });
        await this.rejectButton.click();
        // Ensure the banner disappears
        await expect(this.cookieBanner).not.toBeVisible();
    }

    // Verify that the consent cookie exists
    async verifyConsentCookie(context: BrowserContext): Promise<boolean> {
        const cookies: Cookie[] = await context.cookies();
        return cookies.some((cookie: Cookie) => cookie.name === '.AspNet.Consent');

    }

    // Clear cookies
    async clearCookies(context: BrowserContext) {
        await context.clearCookies();
        await this.page.reload(); 
        await this.page.waitForLoadState('load'); 
        await expect(this.cookieBanner).toBeVisible();// Ensure the cookie banner appears again after clearing cookies
    }

    // Set an expired consent cookie to trigger re-prompt
    async setExpiredConsentCookie(context: BrowserContext) {
        const baseURL = process.env.BASE_URL || 'http://localhost:7050'; 

        if (!baseURL) {
            throw new Error("BASE_URL is not defined");
        }

        await context.addCookies([
            {
                name: '.AspNet.Consent',
                value: 'expired',
                domain: new URL(baseURL).hostname, // Extract domain from BASE_URL
                path: '/',
                expires: Date.now() / 1000 - 10, // Set to past time to expire
                secure: true,
                httpOnly: true,
                sameSite: 'Strict'
            }
        ]);
    }
}
