import { Page, Locator } from '@playwright/test';

export class ShareButtons {
    readonly page: Page;
    readonly facebook: Locator;
    readonly twitter: Locator;
    readonly email: Locator;
    readonly print: Locator;

    constructor(page: Page) {
        this.page = page;

        // Define locators for share buttons
        this.facebook = page.locator('[data-network="facebook"]');
        this.twitter = page.locator('[data-network="twitter"]');
        this.email = page.locator('[data-network="email"]');
        this.print = page.locator('[data-network="print"]');
    }

    async clickFacebook() {
        await this.facebook.click();
    }

    async clickTwitter() {
        await this.twitter.click();
    }

    async clickEmail() {
        await this.email.click();
    }

    async clickPrint() {
        await this.print.click();
    }
}
