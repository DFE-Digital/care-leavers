import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class MoneyAndBenefitsPage extends BasePage {
    public readonly mainContent: Locator;
    public readonly pageSections: Locator;
    public readonly checkStatusLink: Locator;
    public readonly cardExternalLink: Locator;
    public readonly printableCollectionLink: Locator;

    constructor(page: Page) {
        super(page);
        this.mainContent = page.locator('#main-content');
        this.pageSections = page.locator('h2, h3, h4'); // Select all major sections
        this.checkStatusLink = page.locator('a[href="/en/your-rights"]').nth(1); // "Check your care leaver status" link
        this.cardExternalLink = page.locator('.dfe-card a.dfe-card-link--header[href^="http"]').first();    
        this.printableCollectionLink = page.locator('.print-collection-summary'); // Link to the printable collection page
    }

    async openPrintableCollectionPage() {
        await this.navigateTo('/print/en/money-and-benefits');
        await expect(this.printableCollectionLink).toBeVisible();
    }

    async checkSpacerContentTypeExists() {
        const spacerLocator = this.page.locator('.govuk-section-break govuk-section-break--l');
        await expect(spacerLocator);
    }

    async openMoneyAndBenefitsPage() {
        await this.navigateTo('/en/money-and-benefits');
    }

    async assertPageElements() {
        await this.validateURLContains('/en/money-and-benefits');
        await this.verifyLogoPresence();
        await this.verifyHeading("money", "money");

        // Ensure main content wrapper is visible
        await expect(this.mainContent).toBeVisible();

        // Verify all major sections exist(no content validation)
        const sectionCount = await this.pageSections.count();
        expect(sectionCount).toBeGreaterThan(0);

        for (let i = 0; i < sectionCount; i++) {
            await expect(this.pageSections.nth(i)).toBeVisible();
        }

        // Check "Check your care leaver status" link is present
        await expect(this.checkStatusLink).toBeVisible();
        await expect(this.checkStatusLink).toHaveAttribute('href', '/en/your-rights');
        
        await this.verifyCardsCanHaveExternalLinks();
    }
    
    private async verifyCardsCanHaveExternalLinks() {
        const href = await this.cardExternalLink.getAttribute('href')
        
        await Promise.all([
            this.page.waitForURL(href!),
            this.cardExternalLink.click(), // Will navigate to another page
        ]);

        // Assert that the URL matches the href
        expect(this.page.url()).toContain(href);
    }
}
