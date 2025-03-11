import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class HomePage extends BasePage {
    // Main Header Sections Locators
    private mainHeading: Locator;
    private firstHeaderParagraph: Locator;
    private supportHeading: Locator;
    
    //Sections Locators
    private whoIsThisForSection: Locator;
    private findSupportSection: Locator;
    private knowWhatSupportSection: Locator;
    private GuidesSection: Locator;

    //Support cards Sections
    private supportCards: Locator;

    constructor(page: Page) {
        super(page);
        
        // Main Header section  
        this.mainHeading = page.locator('h1');
        this.supportHeading = page.locator('h1.govuk-heading-xl');
        let headerSection = page.locator('div#main-header-container')
        this.firstHeaderParagraph = headerSection.locator('p.govuk-body-l').first();

        // Main Content Sections
        this.whoIsThisForSection = page.locator('#Who-is-this-support-for-');
        this.findSupportSection = page.locator('#Find-support');
        this.knowWhatSupportSection = page.locator('#know-what-support');//update locators
        this.GuidesSection = page.locator('#helpful-guides');//update locators 
        
        // Generic locator for all cards on the home page
        this.supportCards = page.locator('.hf-card-container');
        
    }
    
    async openHomePage() {
        await this.navigateTo('/home');
        await this.waitForPageLoad();
    }
    
    async assertPageElements() {
        await this.validateURLContains('/home');
        // Check if the main heading is visible
        await expect(this.mainHeading).toHaveText("Find support for care leavers");

        // Validate first paragraph
        await expect(this.firstHeaderParagraph).toContainText("Leaving care can be a challenging time");

        // Check if the "who is this for" section is visible
        await expect(this.whoIsThisForSection).toContainText('support for');
        await expect(this.page.locator('#Who-is-this-support-for-')).toHaveClass(/govuk-heading-l/);
    }

    async verifySectionsVisibility() {
        await expect(this.supportHeading).toBeVisible();
        await expect(this.firstHeaderParagraph).toBeVisible();
        await expect(this.whoIsThisForSection).toBeVisible();
        await expect(this.findSupportSection).toBeVisible();
        /*
        await expect(this.whatSupportCanYouGetSection).toBeVisible();
       await expect(this.helpfulGuidesSection).toBeVisible();   
    */
    }
    
    async clickSupportCard(cardTitle: string, expectedUrl: string) {
        // Find the card with the matching title and Click
        const card = this.supportCards.filter({ hasText: cardTitle });
        await card.click();
        await this.page.waitForURL(expectedUrl); // Ensure navigation to the correct URL
    }

    async verifySupportCardsNavigation(cards: { title: string; url: string }[]) {
        for (const card of cards) {
            await this.clickSupportCard(card.title, card.url);
            await this.page.goBack(); // Navigate back to the home page
        }
    }
}
