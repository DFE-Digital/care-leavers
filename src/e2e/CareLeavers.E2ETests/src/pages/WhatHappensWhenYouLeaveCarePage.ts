import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class WhatHappensWhenYouLeaveCarePage extends BasePage {
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
            whoThisGuideIsFor: page.locator('#Who-this-guide-is-for'),
            whatHappensAndWhen: page.locator('#What-happens-and-when'),
            meetingPersonalAdviser: page.locator('#Meeting-your-personal-adviser'),
            checkingSupport: page.locator('#Checking-what-support-you-need'),
            makingPathwayPlan: page.locator('#Making-your-Pathway-Plan'),
            discussingFuture: page.locator('#Discussing-your-future'),
            supportAfterLeavingCare: page.locator('#Support-after-you-leave-care'),
        };

        // Locators for the Helpful Links section
        this.helpfulLinksSection = page.locator('#Helpful-links');
        this.helpfulLinks = [
            page.locator('a[href="housing-and-accommodation"]'),
            page.locator('a[href="money-and-benefits"]'),
            page.locator('div.hf-card:nth-child(3) a[href="all-support"]')  
        ];
    }

    async openWhatHappensPage() {
        await this.navigateTo('/what-happens-when-you-leave-care');
    }

    async assertPageElements() {
        await this.validateURLContains('/what-happens-when-you-leave-care');
        await this.verifyLogoPresence();
        await this.verifyHeading(
            "What happens when you leave care",
            "What to expect as you prepare to leave care to begin independent life."
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

    async verifyTOCNavigation() {
        const tocLinks = [
            { href: '#Who-this-guide-is-for', key: 'whoThisGuideIsFor' },
            { href: '#What-happens-and-when', key: 'whatHappensAndWhen' },
            { href: '#Meeting-your-personal-adviser', key: 'meetingPersonalAdviser' },
            { href: '#Checking-what-support-you-need', key: 'checkingSupport' },
            { href: '#Making-your-Pathway-Plan', key: 'makingPathwayPlan' },
            { href: '#Discussing-your-future', key: 'discussingFuture' },
            { href: '#Support-after-you-leave-care', key: 'supportAfterLeavingCare' }
        ];

        for (const { href, key } of tocLinks) {
            const tocLink = this.page.locator(`a[href="${href}"]`);
            await tocLink.click();
            await expect(this.sections[key]).toBeVisible();
        }
    }
}
