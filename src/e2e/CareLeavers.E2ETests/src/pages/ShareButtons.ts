import { Page, Locator, expect } from '@playwright/test';
import { BasePage } from './BasePage';  

export class ShareButtons extends BasePage {  
    facebookButton: Locator;
    twitterButton: Locator;
    emailButton: Locator;
    printButton: Locator;

    constructor(page: Page) {
        super(page);
        this.facebookButton = page.locator('.st-btn[data-network="facebook"]');
        this.twitterButton = page.locator('.st-btn[data-network="twitter"]');
        this.emailButton = page.locator('.st-btn[data-network="email"]');
        this.printButton = page.locator('.st-btn[data-network="print"]');
    }

    // Method to check visibility of all buttons
    async areButtonsVisible() {
        await expect(this.facebookButton).toBeVisible();
        await expect(this.twitterButton).toBeVisible();
        await expect(this.emailButton).toBeVisible();
        await expect(this.printButton).toBeVisible();
    }

    // Method to click Facebook button and check for dialog
    async clickFacebookButton(context: any) {
        const [newPage] = await Promise.all([
            context.waitForEvent('page'),
            this.facebookButton.click()
        ]);
        await newPage.waitForLoadState();
        return newPage.url();
    }

    // Method to click Twitter button and check for dialog
    async clickTwitterButton(context: any) {
        const [newPage] = await Promise.all([
            context.waitForEvent('page'),
            this.twitterButton.click()
        ]);
        await newPage.waitForLoadState();
        return newPage.url();
    }

    // Method to click Email button and check for popup
    async clickEmailButton() {
        const [popup] = await Promise.all([
            this.page.waitForEvent('popup'),
            this.emailButton.click()
        ]);
        return popup.url();
    }

    // Method to click Print button and check for dialog
    async clickPrintButton() {
        const [dialog] = await Promise.all([
            this.page.waitForEvent('dialog'),
            this.printButton.click()
        ]);
        return dialog.type();
    }
}
