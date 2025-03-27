import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class UnaccompaniedAsylumSeekingPage extends BasePage {
    public readonly pageHeading: Locator;
    public readonly pageDescription: Locator;
    public readonly mainContent: Locator;
    public readonly pageSections: Locator;

    constructor(page: Page) {
        super(page);
        this.pageHeading = page.locator('.govuk-heading-xl'); // Unaccompanied asylum-seeking young people
        this.pageDescription = page.locator('.govuk-body-l'); // Find out about support, help, and advice
        this.mainContent = page.locator('#main-content');
        this.pageSections = page.locator('.govuk-grid-column-two-thirds .govuk-heading-l'); // Sections under the main content
    }

    async openUnaccompaniedAsylumSeekingPage() {
        await this.navigateTo('/unaccompanied-asylum-seeking-young-people'); 
    }

    async assertPageElements() {
        await this.validateURLContains('/unaccompanied-asylum-seeking-young-people');
        await this.verifyLogoPresence();
        await this.verifyHeading("Unaccompanied asylum", "Find out about support, help and advice");

        // Ensure main content wrapper is visible
        await expect(this.mainContent).toBeVisible();

        // Verify all sections under the main content are visible
        const sectionCount = await this.pageSections.count();
        expect(sectionCount).toBeGreaterThan(0);

        for (let i = 0; i < sectionCount; i++) {
            await expect(this.pageSections.nth(i)).toBeVisible();
        }
    }
}
