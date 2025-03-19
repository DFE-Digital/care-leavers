import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';

export class LeavingCareGuidesPage extends BasePage {
    // Define locators for key sections
    public readonly mainContent: Locator;
    public readonly guideSection: Locator;
    public readonly firstGuide: Locator;
    public readonly secondGuide: Locator;

    constructor(page: Page) {
        super(page);
        this.mainContent = page.locator('#main-content'); // Main content wrapper
        this.guideSection = page.locator('.dfe-section.alternating-image-text'); 
        this.firstGuide = page.locator('a[href="what-happens-when-you-leave-care"]'); 
        this.secondGuide = page.locator('a[href="care-terms-explained"]'); 
    }

    async openLeavingCareGuidesPage() {
        await this.navigateTo('/leaving-care-guides'); 
    }

    async assertPageElements() {
        await this.validateURLContains('/leaving-care-guides');
        await this.verifyLogoPresence();
        await this.verifyHeading(
            "Leaving care guides",
            "Guides to help you prepare for leaving care, understand your rights and find support."
        );

        await expect(this.mainContent).toBeVisible();
        await expect(this.guideSection).toBeVisible();
        await expect(this.firstGuide).toBeVisible();
        await expect(this.secondGuide).toBeVisible();
    }
    
    async verifyGuideLinksNavigation() {
        await this.firstGuide.click();
        await this.validateURLContains('/what-happens-when-you-leave-care');
        await this.page.goBack(); // Navigate back to main page

        await this.secondGuide.click();
        await this.validateURLContains('/care-terms-explained');
    }
}
