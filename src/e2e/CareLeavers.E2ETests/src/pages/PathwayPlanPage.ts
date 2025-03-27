import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class PathwayPlanPage extends BasePage {
    public readonly mainContent: Locator;
    public readonly contentsSection: Locator;
    public readonly pageSections: Locator;
    public readonly sectionLinks: Locator;

    constructor(page: Page) {
        super(page);
        this.mainContent = page.locator('.govuk-grid-column-two-thirds').nth(1); // Main content section
        this.contentsSection = page.locator('nav h2.govuk-heading-s'); // Contents heading
        this.sectionLinks = page.locator('nav ul.govuk-list li a'); 
        this.pageSections = page.locator('section.dfe-section h2, section.dfe-section h3'); // Major sections inside the page
    }

    // Method to open the Pathway Plan page
    async openPathwayPlanPage() {
        await this.navigateTo('/pathway-plan');
    }

    // Method to assert the elements on the Pathway Plan page
    async assertPageElements() {
        await this.validateURLContains('/pathway-plan');
        await this.verifyLogoPresence();
        await this.verifyHeading("Pathway Plan", "plan");

        // Ensure the main content wrapper is visible
        await expect(this.mainContent).toBeVisible();
        await expect(this.contentsSection).toHaveText('Contents');

        // Verify the contents section has links
        const linkCount = await this.sectionLinks.count();
        expect(linkCount).toBeGreaterThan(0);

        // Check if each link inside the contents section is visible
        for (let i = 0; i < linkCount; i++) {
            await expect(this.sectionLinks.nth(i)).toBeVisible();
        }

        // Ensure all major sections (like "What is a Pathway Plan?", "Who gets one?", etc.) are visible
        const sectionCount = await this.pageSections.count();
        expect(sectionCount).toBeGreaterThan(0);

        for (let i = 0; i < sectionCount; i++) {
            await expect(this.pageSections.nth(i)).toBeVisible();
        }
    }
}
