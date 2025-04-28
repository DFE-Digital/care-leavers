import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class WorkAndEmploymentPage extends BasePage {
    public readonly mainContent: Locator;
    public readonly pageSections: Locator;
    public readonly checkStatusLink: Locator;

    constructor(page: Page) {
        super(page);
        this.mainContent = page.locator('#main-content');
        this.pageSections = page.locator('h2, h3, h4'); // Select all major sections (h2, h3, h4)
        this.checkStatusLink = page.locator('a[href="/en/your-rights"]').nth(1); // "Check your care leaver status" link
    }

    async openWorkAndEmploymentPage() {
        await this.navigateTo('/en/work-and-employment');
    }

    async assertPageElements() {
        await this.validateURLContains('/en/work-and-employment');
        await this.verifyLogoPresence();
        await this.verifyHeading("Work and employment", "work");

        // Ensure main content wrapper is visible
        await expect(this.mainContent).toBeVisible();

        // Verify all major sections exist (no content validation)
        const sectionCount = await this.pageSections.count();
        expect(sectionCount).toBeGreaterThan(0);

        for (let i = 0; i < sectionCount; i++) {
            await expect(this.pageSections.nth(i)).toBeVisible();
        }

        // Check "Check your care leaver status" link is present
        await expect(this.checkStatusLink).toBeVisible();
        await expect(this.checkStatusLink).toHaveAttribute('href', '/en/your-rights');
    }
}
