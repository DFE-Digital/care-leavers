
import {Page, Locator, expect, FrameLocator} from '@playwright/test';
import {QuestionnaireRunBasePage} from "./QuestionnaireRunBasePage";
import {convertColorToHex, getElementInfo} from "../helpers/utils";

export class QuestionnaireStartPage extends QuestionnaireRunBasePage {
    // ===== Locators =====
    readonly outerHeaderSection: Locator;
    readonly outerHeading: Locator;            // Heading when no decorative image & not embedded
    readonly outerImage: Locator;              // Decorative image when present

    readonly innerFieldset: Locator;           // Fieldset when decorative image or embedded
    readonly innerHeading: Locator;            // Inner H1 inside fieldset
    readonly description: Locator;

    readonly errorSummary: Locator;

    readonly startButton: Locator;

    // ===== Constructor =====
    constructor(page: Page) {
        super(page);

        this.outerHeaderSection = this.pageOrFrame.locator('#main-content-header');
        this.outerHeading = this.outerHeaderSection.locator('h1.govuk-heading-xl');
        this.outerImage = this.outerHeaderSection.locator('img.gtaa-preview-image');

        this.innerFieldset = this.pageOrFrame.locator('fieldset.govuk-fieldset');
        this.innerHeading = this.innerFieldset.locator('#start-page-display-title');

        this.description = this.pageOrFrame.locator('#start-page-details').first();

        this.errorSummary = this.pageOrFrame.locator('.govuk-error-summary[role="alert"][tabindex="-1"]');

        this.startButton = this.pageOrFrame.locator(
            '#start-page-start-now-btn'
        );
    }

    // ===== Assertions =====

    async assertStructure(): Promise<void> {
        // One of: header with H1 OR decorative image
        const hasOuterHeading = await this.outerHeading.isVisible().catch(() => false);
        const hasImage = await this.outerImage.isVisible().catch(() => false);
        const hasInnerFieldset = await this.innerFieldset.isVisible().catch(() => false);
        
        if (!this.frame) {
            expect(hasOuterHeading || hasImage).toBeTruthy();
        }

        if (hasOuterHeading) {
            const text = (await this.outerHeading.textContent())?.trim() ?? '';
            expect(text.length).toBeGreaterThan(0);
        }

        if (hasImage) {
            await expect(this.outerImage).toHaveAttribute('src', 
                /\/en\/get-to-an-answer-questionnaires\/.+\/decorative-image/);
        }

        if (hasInnerFieldset) {
            await expect(this.innerHeading).toBeVisible();
            const innerText = (await this.innerHeading.textContent())?.trim() ?? '';
            expect(innerText.length).toBeGreaterThan(0);
        }

        await expect(this.description).toBeVisible();

        await expect(this.startButton).toBeVisible();
        await expect(this.startButton).toHaveText(/Start now/i);
    }

    async expectErrorSummaryIfPresent(): Promise<void> {
        const visible = await this.errorSummary.isVisible().catch(() => false);
        if (!visible) {
            return;
        }

        await expect(this.errorSummary).toHaveAttribute('role', 'alert');
        await expect(this.errorSummary).toHaveAttribute('tabindex', '-1');
    }

    // ===== Actions =====

    async clickStartNow(): Promise<void> {
        await Promise.all([
            this.waitForPageLoad(),
            this.startButton.click(),
        ]);

        await this.pageOrFrame.locator('#question-content').waitFor({ state: 'visible' });
    }

    async assertStartButtonTextAndColor(
        expectedText: string, 
        expectedHexColor: string,
        expectedHexHoverColor?: string
    ): Promise<void> {
        const continueButtonColor = await this.startButton.evaluate((el) =>
            window.getComputedStyle(el).getPropertyValue('background-color')
        );
        expect(continueButtonColor.length).toBeGreaterThan(0);
        expect(convertColorToHex(continueButtonColor)).toBe(expectedHexColor);
        
        if (expectedHexHoverColor) {
            await this.startButton.focus();
            const hoveredButtonColor = await this.startButton.evaluate((el) =>
                window.getComputedStyle(el).getPropertyValue('background-color')
            );
            expect(hoveredButtonColor.length).toBeGreaterThan(0);
            expect(convertColorToHex(hoveredButtonColor)).toBe(expectedHexHoverColor);
        }

        await expect(this.startButton).toHaveText(expectedText);
    }

    async assertTextColor(expectedHexColor: string): Promise<void> {
        // get all text (h1, h2, h3, h4, h5, h6, label, .govuk-body) 
        // and check they match the expected hex color
        // exclude error messages, as they are rendered in a different colour
        const textElements = this.pageOrFrame.locator('h1, h2, h3, h4, h5, h6, label, .govuk-body');

        const count = await textElements.count();
        expect(count).toBeGreaterThan(0);

        for (let i = 0; i < count; i++) {
            const element = textElements.nth(i);

            const isErrorText = await element.evaluate((el) =>
                el.closest('.govuk-error-message, .govuk-error-summary') !== null
            );

            if (isErrorText) {
                continue;
            }

            const color = await element.evaluate((el) =>
                window.getComputedStyle(el).getPropertyValue('color')
            );
            expect(color.length).toBeGreaterThan(0);

            // generate locator for each element to make debugging easier
            const info = await getElementInfo(element);

            const actualHexColor = convertColorToHex(color);
            expect(actualHexColor,
                `For ${info.selector} [tag=${info.tagName}${info.id ? ' id=' + info.id : ''}${info.classes ? ' classes=' + info.classes : ''}], expected: ${expectedHexColor} but actual: ${actualHexColor}`)
                .toBe(expectedHexColor);
        }
    }
}