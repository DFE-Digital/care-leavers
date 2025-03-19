import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class FormerRelevantPage extends BasePage {
    //locators for key sections
    public readonly mainContent: Locator;
    public readonly supportSection: Locator;
    public readonly generalSupportSection: Locator;
    public readonly helpWithMoneySection: Locator;
    public readonly moreSupportSection: Locator;

    constructor(page: Page) {
        super(page);
        this.mainContent = page.locator('#main-content'); // Main content wrapper
        this.supportSection = page.locator('#Support-as-a-former-relevant-child');
        this.generalSupportSection = page.locator('#zGeneral-support');
        this.helpWithMoneySection = page.locator('#Help-with-money');
        this.moreSupportSection = page.locator('#z-strong-More-support--strong---160-');
    }

    async openFormerRelevantPage() {
        await this.navigateTo('/former-relevant-child'); 
    }

    async verifySectionsVisibility() {
        await expect(this.mainContent).toBeVisible();
        await expect(this.supportSection).toBeVisible();
        await expect(this.generalSupportSection).toBeVisible();
        await expect(this.helpWithMoneySection).toBeVisible();
        await expect(this.moreSupportSection).toBeVisible();
    }

    async assertPageElements() {
        await this.validateURLContains('/former-relevant-child');
        await this.verifyLogoPresence();
        await this.verifyHeading(
            "Former relevant child",  
            "Find out what support you have the right to if your care leaver status is ‘former relevant child'"
        );

        await expect(this.mainContent).toBeVisible();
        await expect(this.supportSection).toBeVisible();
        await expect(this.generalSupportSection).toBeVisible();
        await expect(this.helpWithMoneySection).toBeVisible();
        await expect(this.moreSupportSection).toBeVisible();
    }
}
