import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class AllSupportPage extends BasePage {
    public readonly mainContent: Locator;
    public readonly supportCards: Locator;
    public readonly knowWhatSupportSection: Locator;
    public readonly knowWhatSupportLink: Locator;

    constructor(page: Page) {
        super(page);
        this.mainContent = page.locator('#main-content');
        this.supportCards = page.locator('.dfe-card-container'); // Support cards

        // "Know what support you can get" Section
        this.knowWhatSupportSection = page.locator('.govuk-grid-column-one-half.text-column');
        this.knowWhatSupportLink = this.knowWhatSupportSection.locator('a.govuk-link');
    }

    async openAllSupportPage() {
        await this.navigateTo('/all-support');
        await this.waitForPageLoad();
    }

    async assertPageElements() {
        await this.validateURLContains('/all-support');
        await this.verifyLogoPresence();
        await this.verifyHeading(
            "All support",
            "When you leave care, you may have the right to certain support and there’s some you must apply for."
        );
        await expect(this.mainContent).toBeVisible();
        await this.verifySupportCardsPresence();
        await expect(this.knowWhatSupportSection).toBeVisible();
    }

    async verifySupportCardsPresence() {
        const cardCount = await this.supportCards.count();
        expect(cardCount).toBeGreaterThan(0); // Ensure cards exist

        for (let i = 0; i < cardCount; i++) {
            await expect(this.supportCards.nth(i)).toBeVisible();
        }
    }

    async verifySupportCardsNavigation(cards: { title: string; url: string }[]) {
        for (const card of cards) {
            const cardLocator = this.supportCards.locator('h3', { hasText: new RegExp(card.title, 'i') });
            await cardLocator.first().waitFor({ state: 'attached' });
            await cardLocator.first().scrollIntoViewIfNeeded();
            await cardLocator.first().click();
            await expect(this.page).toHaveURL(new RegExp(card.url));
            await this.page.goBack();
            await this.validateURLContains('/all-support');
        }
    }

    async verifyKnowWhatSupportSection() {
        await expect(this.knowWhatSupportSection).toBeVisible();
        await expect(this.knowWhatSupportLink).toBeVisible();
        await expect(this.knowWhatSupportLink).toHaveAttribute('href', expect.stringContaining('/your-rights'));
    }
}
