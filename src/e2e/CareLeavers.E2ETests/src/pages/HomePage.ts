import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class HomePage extends BasePage {
    private supportForCareLeaversLink: Locator;
    private mainHeading: Locator;
    private whoIsThisFor: Locator;
    private firstParagraph: Locator;
    private footer: Locator;

    private supportHeading: Locator;
    private whoIsThisForSection: Locator;
    private findSupportSection: Locator;
    private whatSupportCanYouGetSection: Locator;
    private helpfulGuidesSection: Locator;
    private ifYouNeedHelpSection: Locator;
    private footerLinks: Locator;
    private homeNavLink: Locator;
    private supportForCareLeaversNav: Locator;
    private yourRightsNav: Locator;
    private leavingCareGuidesNav: Locator;
    private helplinesNav: Locator;
    private supportCards: Locator;

    constructor(page: Page) {
        super(page);
        
        //Website title
        this.supportForCareLeaversLink = page.locator('a.dfe-header__link--service', { hasText: 'Support for care leavers' });

        this.mainHeading = page.locator('h1');
        this.whoIsThisFor = page.locator('h3').first();
        this.firstParagraph = page.locator('p.govuk-body').first();
        this.footer = page.locator('footer');
        
        this.supportHeading = page.locator('h1.govuk-heading-xl');
        this.whoIsThisForSection = page.locator('#Who-is-this-support-for-');
        this.findSupportSection = page.locator('#Find-the-right-support');
        //update locators 
        this.whatSupportCanYouGetSection = page.locator('#what-support-can-you-get');
        this.helpfulGuidesSection = page.locator('#helpful-guides');
        this.ifYouNeedHelpSection = page.locator('#if-you-need-help');
        this.footerLinks = page.locator('footer a');

        this.homeNavLink = page.locator('a.dfe-header__navigation-link', { hasText: "Home" });
        this.supportForCareLeaversNav = page.locator('a.dfe-header__navigation-link', { hasText: "All support" });
        //update locators 
        this.yourRightsNav = page.locator('[role="link"][aria-label="Your rights"]');
        this.leavingCareGuidesNav = page.locator('[role="link"][aria-label="Leaving care guides"]');
        this.helplinesNav = page.locator('[role="link"][aria-label="Helplines"]');
        // Generic locator for all cards on the home page
        this.supportCards = page.locator('.hf-card-container');

    }
    
    async openHomePage() {
        await this.navigateTo('/home');
        await this.waitForPageLoad();
    }
    
    async assertPageElements() {
        await this.validateURLContains('/home');
        
        await expect(this.supportForCareLeaversLink).toHaveText('E2E Tests Support for Care Leavers');
        await expect(this.mainHeading).toHaveText("Get support if you've been in care");
        await expect(this.firstParagraph).toContainText("Starting life as an adult can be challenging");
        await expect(this.whoIsThisFor).toHaveText("Who is this support for?");
        await expect(this.footer).toContainText("Open Government Licence v3.0");
    }

    async verifySectionsVisibility() {
        await expect(this.supportForCareLeaversLink).toBeVisible();
        await expect(this.supportHeading).toBeVisible();
        await expect(this.whoIsThisForSection).toBeVisible();
        await expect(this.findSupportSection).toBeVisible();
        /*
            await expect(this.whatSupportCanYouGetSection).toBeVisible();
            await expect(this.helpfulGuidesSection).toBeVisible();
            await expect(this.ifYouNeedHelpSection).toBeVisible();
         */
    }

    async verifyFooterLinks() {
        const footerLinksCount = await this.footerLinks.count();
        expect(footerLinksCount).toBeGreaterThan(0);

        for (let i = 0; i < footerLinksCount; i++) {
            const link = this.footerLinks.nth(i);
            await expect(link).toBeVisible();
            const href = await link.getAttribute('href');
            expect(href).not.toBeNull();
        }
    }

    async verifyNavigation() {
        
        await this.homeNavLink.click();
        await this.page.waitForURL(/\/home/);

        await this.supportForCareLeaversNav.click();
        await this.page.waitForURL(/\/all-support/);

        /* update locators
        await this.yourRightsNav.click();
        await this.page.waitForURL(/\/your-rights/);

        await this.leavingCareGuidesNav.click();
        await this.page.waitForURL(/\/leaving-care/);

        await this.helplinesNav.click();
        await this.page.waitForURL(/\/helplines/);
        */
    }
    
    async clickSupportCard(cardTitle: string, expectedUrl: string) {
        // Find the card with the matching title and Click
        const card = this.supportCards.filter({ hasText: cardTitle });
        await card.click();
        await this.page.waitForURL(expectedUrl); // Ensure navigation to the correct URL
    }

    async verifySupportCardsNavigation(cards: { title: string; url: string }[]) {
        for (const card of cards) {
            await this.clickSupportCard(card.title, card.url);
            await this.page.goBack(); // Navigate back to the home page
        }
    }
}
