import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class HomePage extends BasePage {
    
    // Phase Banner Section
    private phaseBanner: Locator;

    // Main Header Sections Locators
    private mainHeading: Locator;
    private firstHeaderParagraph: Locator;
    private supportHeading: Locator;
    
    //Sections Locators
    private whoIsThisForSection: Locator;
    private findSupportSection: Locator;

    //Support cards Sections
    private supportCards: Locator;

    //"Know what support you can get" section
    private knowWhatSupportSection: Locator;
    private supportLink: Locator;
    private supportImage: Locator;

    //"Guide" section
    private guidesSection: Locator;
    private guidesHeading: Locator;
    private guideImage: Locator;
    private guideLink: Locator;


    constructor(page: Page) {
        super(page);

        // Phase Banner Section
        this.phaseBanner = page.locator('.govuk-phase-banner');

        // Main Header section  
        this.mainHeading = page.locator('h1');
        this.supportHeading = page.locator('h1.govuk-heading-xl');
        let headerSection = page.locator('div#main-header-container')
        this.firstHeaderParagraph = headerSection.locator('p.govuk-body-l').first();

        // Main Content Sections
        this.whoIsThisForSection = page.locator('#Who-is-this-support-for-');
        this.findSupportSection = page.locator('#Find-support');
        this.knowWhatSupportSection = page.locator('section.dfe-section.banner.banner-blue');
        this.guidesSection = page.locator('section.dfe-section.alternating-image-text');

        // Generic locator for all cards on the home page
        this.supportCards = page.locator('.hf-card-container');
        
        // Locator for the "Know what support you can get" section
        this.supportLink = this.knowWhatSupportSection.locator('a.govuk-link');
        this.supportImage = this.knowWhatSupportSection.locator('.image-container');

        // Locator for the "Guides" section
        this.guidesHeading = page.locator('h2#Guides');
        this.guideImage = this.guidesSection.locator('.image-container');
        this.guideLink = this.guidesSection.locator('a.govuk-link');
    }
    
    async openHomePage() {
        await this.navigateTo('/home');
        await this.waitForPageLoad();
    }

    async verifyPhaseBanner() {
        await expect(this.phaseBanner).toBeVisible();
        await expect(this.phaseBanner.locator('.govuk-link[href="/translation"]')).toHaveText("en");
        await expect(this.phaseBanner.locator('.govuk-phase-banner__content__tag')).toHaveText("Beta");
        await expect(this.phaseBanner.locator('.govuk-phase-banner__text')).toContainText("This is a new service.");
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
        
        //Phase Banner verification
        await this.verifyPhaseBanner();
    }

    async verifySectionsVisibility() {
        await expect(this.supportHeading).toBeVisible();
        await expect(this.firstHeaderParagraph).toBeVisible();
        await expect(this.whoIsThisForSection).toBeVisible();
        await expect(this.findSupportSection).toBeVisible();
        await expect(this.knowWhatSupportSection).toBeVisible();
       await expect(this.guidesSection).toBeVisible();
    }
    
    async clickSupportCard(cardTitle: string, expectedUrl: string) {
        // Find the card with the matching title and Click
        const card = this.supportCards.filter({ hasText: cardTitle });
        await expect(card).toBeVisible();  
        
        // Validate that the card contains an image
        const cardImage = card.locator('img');  
        await expect(cardImage).toBeVisible();
        
        await card.click();
        //await this.page.waitForURL(expectedUrl); // Ensure navigation to the correct URL
        await expect(this.page).toHaveURL(new RegExp(expectedUrl));

    }

    async verifySupportCardsNavigation(cards: { title: string; url: string }[]) {
        for (const card of cards) {
            const cardLocator = this.page.locator('.hf-card-container h3', { hasText: new RegExp(card.title, 'i') });
            await cardLocator.first().waitFor({ state: 'attached' });
            await cardLocator.first().scrollIntoViewIfNeeded();
            await cardLocator.first().click();
            await expect(this.page).toHaveURL(new RegExp(card.url));
            await this.page.goBack();
            await this.validateURLContains('/home');
        }
    }

    async verifyKnowWhatSupportSection() {
        await expect(this.knowWhatSupportSection.locator('h3')).toBeVisible();  // Validate the heading exists 
        await expect(this.knowWhatSupportSection.locator('p')).toBeVisible();   // Validate at least one paragraph exists
        
        // Validate the link exists and is functional
        await expect(this.supportLink).toBeVisible();
        await expect(this.supportLink).toHaveAttribute('href', expect.stringContaining('/status'));

        // Validate the image container exists
        await expect(this.supportImage).toBeVisible();
    }

    async verifyGuidesSection() {
        // Validate the Guides heading exists
        await expect(this.guidesHeading).toBeVisible();

        // Validate that the guide image exists and is wrapped in a link
        await expect(this.guideImage).toBeVisible();
        await expect(this.guideLink).toBeVisible();
        await expect(this.guideLink).toHaveAttribute('href', expect.stringContaining('/guide-leaving-care'));
    }

}
