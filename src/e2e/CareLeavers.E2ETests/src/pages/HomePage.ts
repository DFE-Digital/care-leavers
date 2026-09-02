import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class HomePage extends BasePage {
    
    // Phase Banner Section
    private readonly phaseBanner: Locator;
    
    //Sections Locators
    private readonly whoIsThisForSection: Locator;
    private readonly findSupportSection: Locator;

    //Support cards Sections
    private readonly supportCards: Locator;

    //"Know what support you can get" section
    private readonly knowWhatSupportSection: Locator;
    private readonly supportLink: Locator;
    private readonly supportImage: Locator;

    //"Guide" section
    private readonly guidesSection: Locator;
    private readonly guidesHeading: Locator;
    private readonly guideImage: Locator;
    private readonly guideLink: Locator;

    //"Check Your Care Leaver Support" section
    private readonly CheckYourCareLeaverSupport: Locator;   

    constructor(page: Page) {
        super(page);

        // Phase Banner Section
        this.phaseBanner = page.locator('.govuk-phase-banner');

        // Main Content Sections
        this.whoIsThisForSection = page.locator('#Who-is-this-support-for-');
        this.findSupportSection = page.locator('#Find-support');
        this.knowWhatSupportSection = page.locator('section.dfe-section.banner.banner-blue');
        this.guidesSection = page.locator('section.dfe-section.alternating-image-text');

        // Generic locator for contentful card content type on the home page
        this.supportCards = page.locator('.dfe-card-container');
        
        // Locator for the "Know what support you can get" section
        this.supportLink = this.knowWhatSupportSection.locator('a.govuk-link');
        this.supportImage = this.knowWhatSupportSection.locator('.image-container');

        // Locator for the "Guides" section
        this.guidesHeading = page.locator('h2#Guides');
        this.guideImage = this.guidesSection.locator('.image-container');
        this.guideLink = this.guidesSection.locator('a.govuk-link');
        
        //"Locator for the Call to Action button in Care Leaver Support" section
        this.CheckYourCareLeaverSupport = page.locator('.govuk-button--start');        
    }
    
    async openHomePage() {
        await this.navigateTo('/home');
        await this.waitForPageLoad();
    }

    async verifyPhaseBanner() {
        await expect(this.phaseBanner).toBeVisible();
        await expect(this.phaseBanner.locator('.translate > a')).toHaveText("en");
        await expect(this.phaseBanner.locator('.govuk-phase-banner__content__tag')).toHaveText("Beta");
        await expect(this.phaseBanner.locator('.govuk-phase-banner__text')).toContainText("This is a new service.");
    }
    async assertPageElements() {
        await this.validateURLContains('/home');

        // Verify the logo is present
        await this.verifyLogoPresence();
        
        //Phase Banner verification
        await this.verifyPhaseBanner();
        
        // Validate the main heading and paragraph
        await this.verifyHeading(
            "Find support for care leavers",
            "Leaving care can be a challenging time"
        );       
    }

    async verifySectionsVisibility() {       
        await expect(this.findSupportSection).toBeVisible();       
        await expect(this.guidesSection).toBeVisible();
    }
    
    async verifySupportCardsNavigation(cards: { title: string; url: string }[]) {
        for (const card of cards) {
            const cardLocator = this.supportCards.locator('h3', { hasText: new RegExp(card.title, 'i') });
            await cardLocator.first().waitFor({ state: 'attached' });
            await cardLocator.first().scrollIntoViewIfNeeded();
            await cardLocator.first().click();
            await expect(this.page).toHaveURL(new RegExp(card.url));
            await this.page.goBack();
            await this.validateURLContains('/home');
        }
    }
    
    async verifyGuidesSection() {     
        // Validate that the guide image exists and is wrapped in a link
        await expect(this.guideImage).toBeVisible();
        await expect(this.guideLink).toBeVisible();
        await expect(this.guideLink).toHaveAttribute('href', expect.stringContaining('/en/guide-leaving-care'));
    }

    async verifyContentfulCallToActionButton()
    {
        // Validate that the "CheckYourCareLeaverSupport" section exists
        await expect(this.CheckYourCareLeaverSupport).toBeVisible();        
        await expect(this.CheckYourCareLeaverSupport).toHaveAttribute('href', expect.stringContaining('/en/check-your-care-leaver-support'));   
    }

}
