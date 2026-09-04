import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class YourRightsPage extends BasePage {
    // locators for key sections
    public readonly mainContent: Locator;
    public readonly tableOfContents: Locator; 

    //locator for banner on the Your Rights page
    public readonly contentFulBannerSection: Locator;    
    public readonly contentFulBannerHeading: Locator;

    //locator for Contentful Definition link
    public readonly contentfulDefinitionLink: Locator;
    //locator for Contentful Card 
    public readonly contentfulCardLink: Locator;
    //locator for Contentful Definition
    public readonly contentfulDefinition: Locator;
    //locator for Contentful Grid
    public readonly contentfulGrid: Locator;
    //locator for Contentful NavigationLink
    public readonly contentfulNavigationLink: Locator;
    //locator for Contentful NavigationLink
    public readonly contentfulNavigationLinkItem: Locator;

    constructor(page: Page) {
        super(page);
        this.mainContent = page.locator('#main-content'); // Main content wrapper
        this.tableOfContents = page.locator('#main-content-contents ol');  
        this.contentFulBannerSection = page.locator('.dfe-section banner') 
        this.contentFulBannerHeading = this.contentFulBannerSection.locator('h2, .govuk-heading-l');  
        this.contentfulDefinitionLink = page.locator('a[href*="#definition"]'); 
        this.contentfulCardLink = page.locator('.dfe-card-container'); 
        this.contentfulDefinition = page.locator('.dfe-section dfe box-ext');
        this.contentfulGrid = page.locator('.dfe-grid-container');
        this.contentfulNavigationLink = page.locator('.govuk-service-navigation__item');
        this.contentfulNavigationLinkItem = page.locator('.govuk-service-navigation__link').nth(2);
    }

    async openYourRightsPage() {
        await this.navigateTo('/your-rights'); 
    }

    async verifySectionsVisibility() {
        await expect(this.mainContent).toBeVisible();
        await expect(this.tableOfContents).toBeVisible();       
    }

    async assertPageElements() {
        await this.validateURLContains('/your-rights');
        await this.verifyLogoPresence();
        await this.verifyHeading(
            "right",
            'right');
    }

    async verifyContentfulDefinitionLink() {
        await expect(this.contentfulDefinitionLink).toBeVisible();
    }

    async verifyContentfulCardExists() {
        await expect(this.contentfulCardLink);
    }

    async verifyContentfulGridExists() {
        await expect(this.contentfulGrid);
    }

    async verifyContentfulDefinitionExists() {
        await expect(this.contentfulDefinition);
    }

    async assertBannerExists() {
        await expect(this.contentFulBannerSection);
        await expect(this.contentFulBannerHeading);       
    }

    async verifyContentfulNavigationLinkExists(){
        await expect(this.contentfulNavigationLink);
        await expect(this.contentfulNavigationLinkItem).toHaveAttribute('href', '/en/your-rights');;

    }
}
