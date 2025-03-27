import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class EligibleChildPage extends BasePage {
    // Locators for key sections
    public readonly mainContent: Locator;
    public readonly supportSection: Locator;
    public readonly generalSupportSection: Locator;
    public readonly helpWithMoneySection: Locator;
    public readonly moreSupportSection: Locator;
    public readonly supportOnce18Section: Locator;

    constructor(page: Page) {
        super(page);
        this.mainContent = page.locator('#main-content'); // Main content wrapper
        this.supportSection = page.locator('#Support-as-an-eligible-child');
        this.generalSupportSection = page.locator('#General-support');
        this.helpWithMoneySection = page.locator('#Help-with-money');
        this.moreSupportSection = page.locator('#zstrong-More-support--strong---160-');
        this.supportOnce18Section = page.locator('#zstrong-Support-once-you-re-18--strong---160-');
    }

    async openEligibleChildPage() {
        await this.navigateTo('/eligible-child'); 
    }

    async verifySectionsVisibility() {
        await expect(this.mainContent).toBeVisible();
        await expect(this.supportSection).toBeVisible();
        await expect(this.generalSupportSection).toBeVisible();
        await expect(this.helpWithMoneySection).toBeVisible();
        await expect(this.moreSupportSection).toBeVisible();
        await expect(this.supportOnce18Section).toBeVisible();
    }

    async assertPageElements() {
        await this.validateURLContains('/eligible-child');
        await this.verifyLogoPresence();
        await this.verifyHeading(
            "Eligible child",
            "Find out what support you have the right to if your care leaver status is ‘eligible child’"
        );
    }
}
