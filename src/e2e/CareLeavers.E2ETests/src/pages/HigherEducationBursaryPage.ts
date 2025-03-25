import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class HigherEducationBursaryPage extends BasePage {
    public readonly mainContent: Locator;
    public readonly contentsSection: Locator;
    public readonly pageSections: Locator;
    public readonly sectionLinks: Locator;

    constructor(page: Page) {
        super(page);
        this.mainContent = page.locator('.govuk-grid-column-two-thirds').nth(1);
        this.contentsSection = page.locator('nav h2.govuk-heading-s');
        this.sectionLinks = page.locator('nav ul.govuk-list li a');
        this.pageSections = page.locator('section.dfe-section h2, section.dfe-section h3');
    }

    async openHigherEducationBursaryPage() {
        await this.navigateTo('/higher-education-bursary');
    }

    async assertPageElements() {
        await this.validateURLContains('/higher-education-bursary');
        await this.verifyLogoPresence();
        await this.verifyHeading("Higher Education Bursary", "care");

        await expect(this.mainContent).toBeVisible();
        await expect(this.contentsSection).toHaveText('Contents');

        // Verify the contents section has at least one link
        const linkCount = await this.sectionLinks.count();
        expect(linkCount).toBeGreaterThan(0);

        for (let i = 0; i < linkCount; i++) {
            // Ensure each link under contents is visible
            await expect(this.sectionLinks.nth(i)).toBeVisible();
        }

        // Verify all major sections (h2 and h3 elements in the page) are present and visible
        const sectionCount = await this.pageSections.count();
        expect(sectionCount).toBeGreaterThan(0);

        for (let i = 0; i < sectionCount; i++) {
            // Ensure each section header is visible
            await expect(this.pageSections.nth(i)).toBeVisible();
        }
    }
}
