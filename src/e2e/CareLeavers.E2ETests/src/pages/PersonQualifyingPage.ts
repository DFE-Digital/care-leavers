import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage'; 

export class PersonQualifyingPage extends BasePage {
    // Locators for key sections of the page
    public readonly mainContent: Locator;
    public readonly supportSection: Locator;
    public readonly educationSupportSection: Locator;
    public readonly moreSupportSection: Locator;

    constructor(page: Page) {
        super(page);
        
        this.mainContent = page.locator('#main-content'); // Main content wrapper
        this.supportSection = page.locator('#zstrong-Checking-what-support-you-need--strong---160-');
        this.educationSupportSection = page.locator('#zstrong-If-you-re-in-education-or-training--strong---160-');
        this.moreSupportSection = page.locator('#z-160---strong-More-support--strong---160-');
    }
    
    async openPersonQualifyingPage() {
        await this.navigateTo('/person-qualifying-for-advice-and-assistance');  
    }
    
    async verifySectionsVisibility() {
        await expect(this.mainContent).toBeVisible();
        await expect(this.supportSection).toBeVisible();
        await expect(this.educationSupportSection).toBeVisible();
        await expect(this.moreSupportSection).toBeVisible();
    }
    
    async assertPageElements() {
        await this.validateURLContains('/person-qualifying-for-advice-and-assistance');
        await this.verifyLogoPresence();

        // Ensure that all sections are visible
        await expect(this.mainContent).toBeVisible();
        await expect(this.supportSection).toBeVisible();
        await expect(this.educationSupportSection).toBeVisible();
        await expect(this.moreSupportSection).toBeVisible();
    }
}
