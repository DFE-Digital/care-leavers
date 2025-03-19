import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class CareTermsExplainedPage extends BasePage {
    // Defining locators for key sections
    public readonly mainContent: Locator;
    public readonly tableOfContents: Locator;
    public readonly sections: { [key: string]: Locator };
    public readonly helpfulLinksSection: Locator;
    public readonly helpfulLinks: Locator[];

    constructor(page: Page) {
        super(page);
        this.mainContent = page.locator('#main-content'); // Main content wrapper
        this.tableOfContents = page.locator('nav .govuk-list'); // TOC section

        // Store all sections in an object 
        this.sections = {
            inCare: page.locator('#What-does--in-care--mean-'),
            lookedAfter: page.locator('#What-does--looked-after--mean-'),
            careLeaver: page.locator('#What-is-a-care-leaver-'),
            notLookedAfter: page.locator('#Who-is-not--looked-after--'),
            careLeaverStatus: page.locator('#What-is-a-care-leaver-status-'),
        };

        // Locators for the Helpful Links section
        this.helpfulLinksSection = page.locator('#Helpful-links');
        this.helpfulLinks = [
            page.locator('a[href="housing-and-accommodation"]'),
            page.locator('a[href="money-and-benefits"]'),
            page.locator('div.hf-card:nth-child(3) a[href="all-support"]')
        ];
    }

    // Navigate to the page
    async openCareTermsPage() {
        await this.navigateTo('/care-terms-explained');
    }

    // Assert page elements are correct
    async assertPageElements() {
        await this.validateURLContains('/care-terms-explained');
        await this.verifyLogoPresence();
        await this.verifyHeading(
            "Care terms explained",
            "Learn what the terms ‘in care’, ‘looked after’, and ‘care leaver’ mean."
        );

        await expect(this.mainContent).toBeVisible();
        await expect(this.tableOfContents).toBeVisible();

        // Ensure all sections are visible
        for (const section of Object.values(this.sections)) {
            await expect(section).toBeVisible();
        }

        // Ensure the Helpful Links section is visible
        await expect(this.helpfulLinksSection).toBeVisible();

        // Ensure all helpful links are visible and contain appropriate text
        for (const link of this.helpfulLinks) {
            await expect(link).toBeVisible();
            const linkText = await link.locator('h3').innerText();
            expect(linkText).not.toBe('');
        }
    }

    // Verify the table of contents links and their corresponding sections
    async verifyTOCNavigation() {
        const tocLinks = [
            { href: '#What-does--in-care--mean-', key: 'inCare' },
            { href: '#What-does--looked-after--mean-', key: 'lookedAfter' },
            { href: '#What-is-a-care-leaver-', key: 'careLeaver' },
            { href: '#Who-is-not--looked-after--', key: 'notLookedAfter' },
            { href: '#What-is-a-care-leaver-status-', key: 'careLeaverStatus' }
        ];

        for (const { href, key } of tocLinks) {
            const tocLink = this.page.locator(`a[href="${href}"]`);
            await tocLink.click();
            await expect(this.sections[key]).toBeVisible();
        }
    }
}
