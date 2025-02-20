import { Page, expect } from '@playwright/test';

export class BasePage {
    protected readonly page: Page;

    constructor(page: Page) {
        this.page = page;
    }

    async navigateTo(url: string) {
        console.log(`Navigating to ${url}`);
        await this.page.goto(url, { waitUntil: 'networkidle' });

        // Print the complete URL to confirm where the test is running
        const currentURL = this.page.url();
        console.log(`Current URL: ${currentURL}`);
    }

    async validateURLContains(path: string) {
        await expect(this.page).toHaveURL(new RegExp(path));
    }

    async waitForPageLoad() {
        await this.page.waitForLoadState('load');
    }
}
