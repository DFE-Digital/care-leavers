import { Page, Locator, expect } from '@playwright/test';
export class HomePage {
    private page: Page;
    private mainHeading: Locator;
    private whoIsThisFor: Locator;
    private firstParagraph: Locator;
    private footer: Locator;

    constructor(page: Page) {
        this.page = page;
        this.mainHeading = page.locator('h1');
        this.whoIsThisFor = page.locator('h3').first();
        this.firstParagraph = page.locator('p.govuk-body').first();
        //this.firstParagraph = page.locator('p:has-text("Starting life as an adult can be challenging")');
        this.footer = page.locator('footer');
    }

    async navigate() {
        await this.page.goto('/');
    }

    async assertPageElements() {
        // Check page URL
        await expect(this.page).toHaveURL(/\/home$/);

        // need to move all the hardcoded values to point to mock json when and as soon as it is available 
        // Check main heading
        await expect(this.mainHeading).toHaveText("Get support if you've been in care");
        
        // Check key text content
        await expect(this.firstParagraph).toContainText("Starting life as an adult can be challenging");

        // Check "Who is this for?" section
        await expect(this.whoIsThisFor).toHaveText("Who is this support for?");

        // Validate web footer text
        await expect(this.footer).toContainText("Open Government Licence v3.0");
    }
}
