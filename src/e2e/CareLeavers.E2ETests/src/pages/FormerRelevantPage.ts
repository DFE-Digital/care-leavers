import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class FormerRelevantPage extends BasePage {
    //locators for key sections
    public readonly mainContent: Locator;    
    public readonly helpWithMoneySection: Locator;
   

    constructor(page: Page) {
        super(page);
        this.mainContent = page.locator('#main-content'); // Main content wrapper        
        this.helpWithMoneySection = page.locator('#Help-with-money');        
    }

    async openFormerRelevantPage() {
        await this.navigateTo('/former-relevant-child'); 
    }

    async verifySectionsVisibility() {
        await expect(this.mainContent).toBeVisible();        
        await expect(this.helpWithMoneySection).toBeVisible();       
    }

    async assertPageElements() {
        await this.validateURLContains('/former-relevant-child');
        await this.verifyLogoPresence();
        await this.verifyHeading(
            "relevant",  
            "relevant"
        );
    }
}
