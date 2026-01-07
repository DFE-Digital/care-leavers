import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class CheckYourCareLeaverSupport extends BasePage {
    public readonly gtaaIFrame: Locator;

    constructor(page: Page) {
        super(page);

        this.gtaaIFrame = page.locator('#gtaaFrame');
    }

    async openCheckYourCareLeaverSupportPage() {
        await this.navigateTo('/en/check-your-care-leaver-support');
        await this.waitForPageLoad();
    }

    async assertIframeLoaded() {
        await expect(this.gtaaIFrame).toBeVisible();
    }
}
