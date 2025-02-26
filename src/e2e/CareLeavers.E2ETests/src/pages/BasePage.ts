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
        await expect(this.cookieBanner).toBeVisible({ timeout: 10000 });
        await this.rejectButton.click();

        // Small delay to let UI settle
        await this.page.waitForTimeout(500); 
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
        const domain = process.env.PLAYWRIGHT_BASE_URL
        await context.addCookies([
            {
                name: '.AspNet.Consent',
                value: 'expired',
                domain: domain,
                path: '/',
                expires: Date.now() - 1000,
                httpOnly: false,
                secure: true,
                sameSite: 'Strict',
            },
        ]);
    }
}
