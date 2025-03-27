import { Page,Locator, expect, BrowserContext, Cookie } from '@playwright/test';

export class BasePage {
    protected readonly page: Page;

    //Locator for the Website Title navigation Link
    public readonly WebsiteNameLink: Locator;

    // Locators for the logo link and logo images
    public logoLink: Locator;
    public defaultLogo: Locator;

    // Locators for cookie banner and buttons
    public readonly cookieBanner: Locator;
    public readonly acceptButton: Locator;
    public readonly rejectButton: Locator;

    // Main Header Sections Locators 
    public readonly mainHeading: Locator;
    public readonly firstHeaderParagraph: Locator;
    public readonly supportHeading: Locator;

    //Locators for Navigation Bar
    public readonly navLinkHome: Locator;
    public readonly navLinkAllSupport: Locator;
    public readonly navLinkYourRights: Locator;
    public readonly navLinkLeavingCareGuides: Locator;
    public readonly navLinkHelplines: Locator;

    //Locators for Navigation Bar-Mobile Menu
    public readonly menuToggle: Locator;
    public readonly mobileMenuLinks: Locator;
    public readonly mobileMenuContainer: Locator;

    // Locators for social media share buttons
    public readonly shareButtonsContainer: Locator;
    public readonly printShareButton: Locator;

    // Locators for Metadata column
    public readonly metadataDefinitions: Locator;

    //Locators for helplines-If you need Help at the bottom of the page
    public readonly helplineLink: Locator;
    public readonly ifYouNeedHelpSection: Locator;

    //Locators for web page Footers
    public readonly footer: Locator;
    public readonly footerLinks: Locator;
    public readonly cookiePolicyLinkInFooter: Locator;
    public readonly licenceLogo: Locator;

    constructor(page: Page) {
        this.page = page;
        //Locator for the Website Title navigation Link
        this.WebsiteNameLink = page.locator('a.dfe-header__link--service');

        //Logo 
        this.logoLink = page.locator('a.dfe-header__link');
        this.defaultLogo = page.locator('img.dfe-logo');

        // Locators for cookie banner and buttons
        this.cookieBanner = page.locator('.govuk-cookie-banner');
        this.acceptButton = page.locator('#accept-cookie');
        this.rejectButton = page.locator('#reject-cookie');

        // Main Header section  
        this.mainHeading = page.locator('h1');
        this.supportHeading = page.locator('h1.govuk-heading-xl');
        let headerSection = page.locator('div#main-header-container')
        this.firstHeaderParagraph = headerSection.locator('p.govuk-body-l').first();

        //Locators for Navigation Bar
        this.navLinkHome = page.locator('a.govuk-service-navigation__link', { hasText: "Home" });
        this.navLinkAllSupport = page.locator('a.govuk-service-navigation__link', { hasText: "All support" });
        //update locators 
        this.navLinkYourRights = page.locator('[role="link"][aria-label="Your rights"]');
        this.navLinkLeavingCareGuides = page.locator('[role="link"][aria-label="Leaving care guides"]');
        this.navLinkHelplines = page.locator('[role="link"][aria-label="Helplines"]');

        //Locators for Navigation Bar-Mobile Menu
        this.menuToggle = page.locator('.govuk-js-service-navigation-toggle');
        this.mobileMenuContainer = page.locator('#navigation');
        this.mobileMenuLinks = page.locator('#navigation a');

        // Locators for social media share buttons
        this.shareButtonsContainer = page.locator('.shareaholic-share-buttons');
        this.printShareButton = page.locator('#print-link');

        // Locators for Metadata definitions
        this.metadataDefinitions = page.locator('.gem-c-metadata__definition');

        //Locators for helplines-If you need Help at the bottom of the page
        this.helplineLink = page.locator('p.govuk-body a.govuk-link[href="helplines"]');
        this.ifYouNeedHelpSection = page.locator('#Talk-to-someone');

        //Locators for web page Footers
        this.footer = page.locator('footer');
        this.footerLinks = page.locator('footer a');
        this.cookiePolicyLinkInFooter = page.locator('a.govuk-footer__link[href="/en/pages/cookie-policy"]');
        this.licenceLogo = page.locator('svg.govuk-footer__licence-logo');

    }

    // Navigates to the specified URL and waits for the page to load.
    async navigateTo(url: string) {
        await this.page.goto(url, { waitUntil: 'networkidle' });
    }

    // Validate URL contains a specific path
    async validateURLContains(path: string) {
        await expect(this.page).toHaveURL(new RegExp(path));
    }

    // Wait for the page to load
    async waitForPageLoad() {
        await this.page.waitForLoadState('load');
    }

    // Cookie banner functionality
    async acceptCookies() {
        await this.acceptButton.click();
        await expect(this.cookieBanner).not.toBeVisible();
    }

    async rejectCookies() {
        await expect(this.rejectButton).toBeVisible();
        await this.rejectButton.click();
        // Ensure the banner disappears
        await expect(this.cookieBanner).not.toBeVisible();
    }

    // Verify that the consent cookie exists
    async verifyConsentCookie(context: BrowserContext): Promise<boolean> {
        const cookies: Cookie[] = await context.cookies();
        return cookies.some((cookie: Cookie) => cookie.name === '.AspNet.Consent');

    }

    async verifyLogoPresence() {
        await expect(this.logoLink).toBeVisible();
        await expect(this.defaultLogo).toBeVisible();
    }

    // Clear cookies
    async clearCookies(context: BrowserContext) {
        await context.clearCookies();
        await this.page.reload();
        await this.page.waitForLoadState('load');
        await expect(this.cookieBanner).toBeVisible();// Ensure the cookie banner appears again after clearing cookies
    }

    // Set an expired consent cookie to trigger re-prompt
    async setExpiredConsentCookie(context: BrowserContext) {
        const baseURL = process.env.BASE_URL || 'http://localhost:7050';

        if (!baseURL) {
            throw new Error("BASE_URL is not defined");
        }

        await context.addCookies([
            {
                name: '.AspNet.Consent',
                value: 'expired',
                domain: new URL(baseURL).hostname, // Extract domain from BASE_URL
                path: '/',
                expires: Date.now() / 1000 - 10, // Set to Pastime to expire
                secure: true,
                httpOnly: true,
                sameSite: 'Strict'
            }
        ]);
    }

    // Generic method to verify PAGE Main heading and its paragraph text
    async verifyHeading(expectedHeading: string, expectedParagraph: string) {
        await expect(this.mainHeading).toBeVisible();
        const actualHeading = await this.mainHeading.innerText();
        expect(actualHeading.trim()).toContain(expectedHeading.trim());
        await expect(this.firstHeaderParagraph).toBeVisible();
        await expect(this.firstHeaderParagraph).toContainText(expectedParagraph.trim());
        await expect(this.supportHeading).toBeVisible();
    }

    // Navigation Bar functionality
    // Helper method to ensure the menu is visible and reopens it if needed
    async ensureMenuIsVisible() {
        // Check if the menu is visible, if not, click the hamburger menu to open it
        const isMenuVisible = await this.mobileMenuContainer.isVisible();
        if (!isMenuVisible) {
            await this.menuToggle.click();
            await expect(this.mobileMenuContainer).toBeVisible(); // Ensure it becomes visible
        }
    }

    //Verify Navigation Bar functionality
    async verifyNavigation(isDesktop: boolean) {
        if (isDesktop) {
            // Verify desktop navigation is visible
            await expect(this.page.locator('#header-navigation')).toBeVisible();

            const navLinks = [this.navLinkHome, this.navLinkAllSupport /* add other nav links here */];
            for (const link of navLinks) {
                const href = await link.getAttribute('href');
                if (!href) throw new Error('Link does not have an href attribute');

                await link.click();
                await this.page.waitForURL(new RegExp(href));
            }
        }
        else {
            // Click on the hamburger menu to open the mobile menu
            await expect(this.menuToggle).toBeVisible();
            await this.menuToggle.click();

            // Wait and verify that the mobile menu is visible
            await expect(this.mobileMenuContainer).toBeVisible();
            await expect(this.menuToggle).toHaveAttribute("aria-expanded", "true");

            // Verify the mobile menu links
            const mobileLinksCount = await this.mobileMenuLinks.count();
            expect(mobileLinksCount).toBeGreaterThan(0);

            // Close the mobile menu
            await this.menuToggle.click();

            // Ensure the menu is closed
            await expect(this.mobileMenuContainer).not.toBeVisible();

            // click each link and ensure the menu is visible each time
            const links = [
                { index: 0, href: '/en/home' },
                { index: 1, href: '/en/all-support' },
                /*{ index: 2, href: '/en/status' },
                { index: 3, href: '/en/guides-advice' },
                { index: 4, href: '/en/helplines' },*/
            ];

            for (const link of links) {
                await this.ensureMenuIsVisible(); // Ensure the menu is visible before clicking
                await expect(this.mobileMenuLinks.nth(link.index)).toHaveAttribute('href', link.href);
                await this.mobileMenuLinks.nth(link.index).click();
                await this.page.waitForURL(new RegExp(link.href));
            }
        }
    }

    // Locate breadcrumb items on the page
    getBreadcrumbItems(): Locator {
        return this.page.locator('.govuk-breadcrumbs__link');
    }

    // Get text of a breadcrumb link by index
    async getBreadcrumbLinkText(index: number): Promise<string> {
        const breadcrumb = this.getBreadcrumbItems().nth(index);
        return await breadcrumb.innerText();
    }

    //Method to check breadcrumbs
    async checkBreadcrumbs(url: string, expectedBreadcrumbs: string[]) {
        await this.navigateTo(url);
        await this.waitForPageLoad();

        const breadcrumbItems = this.getBreadcrumbItems();
        const breadcrumbCount = await breadcrumbItems.count();
        if (breadcrumbCount !== expectedBreadcrumbs.length) {
            throw new Error(`Expected ${expectedBreadcrumbs.length} breadcrumbs, but found ${breadcrumbCount}`);
        }
        for (let i = 0; i < breadcrumbCount; i++) {

            const actualText = await this.getBreadcrumbLinkText(i);

            if (actualText.trim().toLowerCase() === 'home') {
                const link = breadcrumbItems.nth(i);
                await link.click(); // Click the Home breadcrumb
                await this.waitForPageLoad();
                const breadcrumbItemsAfterHome = this.getBreadcrumbItems();
                const breadcrumbCountAfterHome = await breadcrumbItemsAfterHome.count();
                if (breadcrumbCountAfterHome !== 0) {
                    throw new Error("Expected no breadcrumbs on the home page.");
                }
                return; // Skip further checks if Home was clicked
            }

            expect(actualText.trim()).toBe(expectedBreadcrumbs[i]);

            const expectedURL = `/${expectedBreadcrumbs[i].toLowerCase().replace(/\s+/g, '-')}`;
            const link = breadcrumbItems.nth(i);
            await link.click();
            await this.page.waitForURL(expectedURL);
        }
    }

    //Verify that the Social Media and Share buttons are visible 
    async verifyShareButtonsVisibility() {
        await Promise.all([
            expect(this.shareButtonsContainer).toBeVisible(),
            expect(this.printShareButton).toBeVisible()
        ]);
    }

    //Verify Metadata(Page Published and Last Updated) are visible 
    async verifyMetadataIsPopulated() {
        const count = await this.metadataDefinitions.count();
        for (let i = 0; i < count; i++) {
            await expect(this.metadataDefinitions.nth(i)).not.toBeEmpty();
        }
    }

    //verify footer Links are visible
    async verifyFooterLinks() {
        //Ensure the footer is visible 
        await expect(this.footer).toBeVisible();

        // Verify the "Cookie Policy" link(in Footer)
        await expect(this.cookiePolicyLinkInFooter).toBeVisible();
        await expect(this.cookiePolicyLinkInFooter).toContainText('Cookie Policy');
        await expect(this.cookiePolicyLinkInFooter).toHaveAttribute('href', '/en/pages/cookie-policy');

        // Verify the footer logo and licence description
        await expect(this.licenceLogo).toBeVisible();
        const licenceDescription = this.footer.locator('.govuk-footer__licence-description');
        await expect(licenceDescription).toBeVisible();
        await expect(licenceDescription).toContainText(/All content is available under the/);

        // Verify the Footer links-Crown copyright link and Open Government Licence link
        const footerLinksCount = await this.footerLinks.count();
        expect(footerLinksCount).toBeGreaterThan(0);

        for (let i = 0; i < footerLinksCount; i++) {
            const link = this.footerLinks.nth(i);
            await expect(link).toBeVisible();
            const href = await link.getAttribute('href');
            expect(href).not.toBeNull();
        }
    }
}
