import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class ErrorPage extends BasePage {

    public readonly mainContent: Locator;
    public readonly mainHeading: Locator;
    
    constructor(page: Page) {
        super(page);
        this.mainContent = page.locator('#main-content');
        this.mainHeading = page.locator('h1.govuk-heading-xl');
    }
    
    async openErrorPage() {
        await this.navigateTo('/en/all-suppor');
        await this.waitForPageLoad();
    }

    async openErrorPageWithCorrectUrl() {
        await this.navigateTo('/en/all-support');
        await this.waitForPageLoad();
    }

    async assertPageElements({checkUrl = false} = {}) {
        if(checkUrl) {
            await this.validateURLContains('/en/service-unavailable');
        }
        await expect(this.mainHeading).toBeVisible();
        await expect(this.mainContent).toBeVisible();
    }
}