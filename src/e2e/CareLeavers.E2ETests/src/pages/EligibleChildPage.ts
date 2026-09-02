import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class EligibleChildPage extends BasePage {
    // Locators for key sections
    public readonly mainContent: Locator;
    public readonly supportSection: Locator;    
    public readonly helpWithMoneySection: Locator;    

    constructor(page: Page) {
        super(page);
        this.mainContent = page.locator('#main-content'); // Main content wrapper
        this.supportSection = page.locator('#primary-content');       
        this.helpWithMoneySection = page.locator('#Help-with-money');       
    }

    async openEligibleChildPage() {
        await this.navigateTo('/eligible-child'); 
    }

    async verifySectionsVisibility() {
        await expect(this.mainContent).toBeVisible();
        await expect(this.supportSection).toBeVisible();        
        await expect(this.helpWithMoneySection).toBeVisible();        
    }

    async assertPageElements() {
        await this.validateURLContains('/eligible-child');
        await this.verifyLogoPresence();
        await this.verifyHeading(
            "child",
            "child"
        );
    }
}
