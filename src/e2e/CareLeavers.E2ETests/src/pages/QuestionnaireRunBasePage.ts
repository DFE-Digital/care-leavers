import {Page, Locator, expect, BrowserContext, Cookie, FrameLocator} from '@playwright/test';
import {convertColorToHex} from "../helpers/utils";
import {BasePage} from "./BasePage";

type LoadState = 'domcontentloaded' | 'load' | 'networkidle';

export class QuestionnaireRunBasePage extends BasePage {
    protected readonly page: Page;
    protected readonly frame?: FrameLocator;
    protected readonly pageOrFrame: Page | FrameLocator;
    
    protected readonly mainContent: Locator;

    // ===== Constructor =====
    constructor(page: Page) {
        super(page);
        
        this.page = page;
        this.frame = page.frameLocator('#gtaaFrame');
        
        this.pageOrFrame = this.frame;
        
        this.mainContent = this.pageOrFrame.locator('.govuk-template__body');
    }

    async navigateTo(url: string, waitUntil: LoadState = 'networkidle'): Promise<void> {
        await this.page.goto(url, {waitUntil});
    }

    // ===== Actions =====
    // Wait for the page to load
    async waitForPageLoad(waitState: LoadState = 'networkidle') {
        await this.page.waitForLoadState(waitState);
    }

    static async create<T extends QuestionnaireRunBasePage>(
        this: new (page: Page) => T,
        page: Page,
        frame?: FrameLocator
    ): Promise<T> {
        const instance = new this(page);
        await instance.waitForPageLoad();
        if (frame) {
           await frame.locator('body').waitFor({ state: 'visible' });
        }
        return instance;
    }

    errorSummaryLink(href: string): Locator {
        return this.pageOrFrame.locator(`[href="${href}"].govuk-link.govuk-error-summary__link`)
    }

    inlineErrorLink(fieldId: string): Locator {
        return this.pageOrFrame.locator(`#${fieldId}.govuk-error-message`)
    }
    
    async assertBackgroundColor(expectedHexColor: string) {
        await expect(this.mainContent).toBeVisible();
        const backgroundColor = await this.mainContent.evaluate((el) =>
            window.getComputedStyle(el).getPropertyValue('background-color')
        );
        expect(backgroundColor.length).toBeGreaterThan(0);
        expect(convertColorToHex(backgroundColor)).toBe(expectedHexColor);
    }
}