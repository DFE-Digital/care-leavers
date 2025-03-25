import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class LeavingCareAllowancePage extends BasePage {
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

    async openLeavingCareAllowancePage() {
        await this.navigateTo('/leaving-care-allowance');
    }

    async assertPageElements() {
        await this.validateURLContains('/leaving-care-allowance');
        await this.verifyLogoPresence();
        await this.verifyHeading("Leaving Care Allowance", "care");

        // Ensure main content wrapper is visible
        await expect(this.mainContent).toBeVisible();
        await expect(this.contentsSection).toHaveText('Contents');

        // Verify the contents section has links
        const linkCount = await this.sectionLinks.count();
        expect(linkCount).toBeGreaterThan(0);

        for (let i = 0; i < linkCount; i++) {
            await expect(this.sectionLinks.nth(i)).toBeVisible();
        }

        // Verify all major sections exist
        const sectionCount = await this.pageSections.count();
        expect(sectionCount).toBeGreaterThan(0);

        for (let i = 0; i < sectionCount; i++) {
            await expect(this.pageSections.nth(i)).toBeVisible();
        }
    }
}