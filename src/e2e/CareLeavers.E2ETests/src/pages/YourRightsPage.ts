import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class YourRightsPage extends BasePage {
    // locators for key sections
    public readonly mainContent: Locator;
    public readonly tableOfContents: Locator;
    public readonly careLeaverStatusSection: Locator;
    public readonly workOutStatusSection: Locator;
    public readonly formerRelevantChildSection: Locator;
    public readonly personsQualifyingSection: Locator;
    public readonly eligibleChildSection: Locator;
    public readonly relevantChildSection: Locator;

    constructor(page: Page) {
        super(page);
        this.mainContent = page.locator('#main-content'); // Main content wrapper
        this.tableOfContents = page.locator('nav .govuk-list');


        this.careLeaverStatusSection = page.locator('#What-is-a--care-leaver-status--');
        this.workOutStatusSection = page.locator('#How-to-work-out-your-status');
        this.formerRelevantChildSection = page.locator('#Former-relevant-child');
        this.personsQualifyingSection = page.locator('#Persons-qualifying-for-advice-and-assistance');
        this.eligibleChildSection = page.locator('#Eligible-child');
        this.relevantChildSection = page.locator('#Relevant-child');
    }

    async openYourRightsPage() {
        await this.navigateTo('/your-rights'); 
    }

    async verifySectionsVisibility() {
        await expect(this.mainContent).toBeVisible();
        await expect(this.tableOfContents).toBeVisible();
        await expect(this.careLeaverStatusSection).toBeVisible();
        await expect(this.workOutStatusSection).toBeVisible();
        await expect(this.formerRelevantChildSection).toBeVisible();
        await expect(this.personsQualifyingSection).toBeVisible();
        await expect(this.eligibleChildSection).toBeVisible();
        await expect(this.relevantChildSection).toBeVisible();
    }

    async assertPageElements() {
        await this.validateURLContains('/your-rights');
        await this.verifyLogoPresence();
        await this.verifyHeading(
            "Your rights",
            'Learn about care leaver statuses');
        
        await expect(this.mainContent).toBeVisible();
        await expect(this.tableOfContents).toBeVisible();
        await expect(this.careLeaverStatusSection).toBeVisible();
        await expect(this.workOutStatusSection).toBeVisible();
        await expect(this.formerRelevantChildSection).toBeVisible();
        await expect(this.personsQualifyingSection).toBeVisible();
        await expect(this.eligibleChildSection).toBeVisible();
        await expect(this.relevantChildSection).toBeVisible();
    }
}
