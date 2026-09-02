import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class RelevantChildPage extends BasePage {
    // Locators for key sections on the Relevant Child page
    public readonly mainContent: Locator;
    public readonly relevantChildSupportSection: Locator;
    public readonly generalSupportSection: Locator;
    public readonly helpWithMoneySection: Locator;
    public readonly moreSupportSection: Locator;

    constructor(page: Page) {
        super(page);
        this.mainContent = page.locator('#main-content'); // Main content wrapper
        this.relevantChildSupportSection = page.locator('#Support-as-a-relevant-child');
        this.generalSupportSection = page.locator('#General-support');
        this.helpWithMoneySection = page.locator('#Help-with-money');
        this.moreSupportSection = page.locator('#zstrong-More-support--strong---160-');
    }

    async openRelevantChildPage() {
        await this.navigateTo('/relevant-child'); 
    }

    async verifySectionsVisibility() {
        await expect(this.mainContent).toBeVisible();       
        await expect(this.helpWithMoneySection).toBeVisible();        
    }

    async assertPageElements() {
        await this.validateURLContains('/relevant-child');
        await this.verifyLogoPresence();
        await this.verifyHeading(
            "child",
            "child"
        );
    }
}
