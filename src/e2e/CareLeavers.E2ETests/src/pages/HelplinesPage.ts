import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class HelplinesPage extends BasePage {
    // Locators for key sections
    public readonly mainHeading: Locator;
    
    public readonly mainContent: Locator;
    public readonly tableOfContents: Locator;
    public readonly someoneToTalkToSection: Locator;
    public readonly understandingRightsSection: Locator;
    public readonly housingMoneySection: Locator;

    // Adding locators for the helpline sections
    public readonly samaritansSection: Locator;
    public readonly papyrusSection: Locator;
    public readonly childlineSection: Locator;
    public readonly helpAtHandSection: Locator;
    public readonly shelterSection: Locator;
    public readonly centrePointSection: Locator;

    constructor(page: Page) {
        super(page);
        this.mainHeading = page.locator('h1.govuk-heading-xl');
        
        this.mainContent = page.locator('.govuk-grid-column-two-thirds').nth(1);
        this.tableOfContents = page.locator('#main-content-contents ol');

        // Section locators
        this.someoneToTalkToSection = page.locator('section.dfe-section:nth-of-type(1)');
        this.understandingRightsSection = page.locator('section.dfe-section:nth-of-type(2)');
        this.housingMoneySection = page.locator('section.dfe-section:nth-of-type(3)');

        // Helpline-specific sections 
        this.samaritansSection = page.locator('section.dfe-section:has-text("Samaritans")');
        this.papyrusSection = page.locator('section.dfe-section:has-text("Papyrus")');
        this.childlineSection = page.locator('section.dfe-section:has-text("Childline")');
        this.helpAtHandSection = page.locator('section.dfe-section:has-text("Help at Hand")');
        this.shelterSection = page.locator('section.dfe-section:has-text("Shelter")');
        this.centrePointSection = page.locator('section.dfe-section:has-text("Centre Point")');
    }

    async openHelplinesPage() {
        await this.navigateTo('/en/helplines');
    }

    async verifySectionsVisibility() {
        // Ensure the main content and table of contents are visible
        await expect(this.mainContent).toBeVisible();
        await expect(this.tableOfContents).toBeVisible();

        // Verify that the individual helpline sections are visible
        await expect(this.samaritansSection).toBeVisible();
        await expect(this.papyrusSection).toBeVisible();
        await expect(this.childlineSection).toBeVisible();
        await expect(this.helpAtHandSection).toBeVisible();
        await expect(this.shelterSection).toBeVisible();
        await expect(this.centrePointSection).toBeVisible();

        // You can also check if any other sections (like someoneToTalkToSection) are visible as needed
        await expect(this.someoneToTalkToSection).toBeVisible();
        await expect(this.understandingRightsSection).toBeVisible();
        await expect(this.housingMoneySection).toBeVisible();
    }

    async assertPageElements() {
        await this.validateURLContains('/helplines');
        await this.verifyLogoPresence();
        
        await expect(this.mainHeading).toBeVisible();
        const actualHeading = await this.mainHeading.innerText();
        expect(actualHeading.trim()).toBe("Helplines");
    }
}
