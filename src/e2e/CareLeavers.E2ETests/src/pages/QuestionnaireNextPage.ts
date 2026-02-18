import {Page, Locator, expect, FrameLocator} from '@playwright/test';
import {QuestionnaireRunBasePage} from "./QuestionnaireRunBasePage";
import {convertColorToHex, getElementInfo, waitForRedirectTo} from "../helpers/utils";

type DestinationType = 'Question' | 'CustomContent' | 'ExternalLink';

export class QuestionnaireNextPage extends QuestionnaireRunBasePage {
    // ===== Common Locators =====

    // Error state
    readonly errorSummary: Locator;
    readonly errorFormGroup: Locator;
    readonly errorFieldMessage: Locator;
    readonly errorSummaryListLink: Locator;

    // Question mode
    readonly questionForm: Locator;
    readonly questionFieldset: Locator;
    readonly questionHeading: Locator;
    readonly questionHint: Locator;

    readonly singleSelectRadios: Locator;
    readonly multiSelectCheckboxes: Locator;
    readonly dropdownSelect: Locator;

    readonly continueButton: Locator;

    // Results page mode
    readonly resultsPageFieldset: Locator;           // Fieldset when decorative image or embedded
    readonly resultsPageHeading: Locator;            // Inner H1 inside fieldset
    readonly resultsPageDescription: Locator;

    // External link mode
    readonly externalLinkHiddenInput: Locator;

    // ===== Constructor =====
    constructor(page: Page) {
        super(page);

        this.errorSummary = this.pageOrFrame.locator('.govuk-error-summary[role="alert"][tabindex="-1"]');
        this.errorFormGroup = this.pageOrFrame.locator('.govuk-form-group--error');
        this.errorFieldMessage = this.errorFormGroup.locator('.govuk-error-message');
        this.errorSummaryListLink = this.errorSummary.locator('a[href^="#"]');

        this.questionForm = this.pageOrFrame.locator('#question-form');
        this.questionFieldset = this.questionForm.locator('fieldset.govuk-fieldset');
        this.questionHeading = this.questionFieldset.locator('h1.govuk-fieldset__heading');
        this.questionHint = this.questionFieldset.locator('.govuk-hint').first();

        this.singleSelectRadios = this.questionFieldset.locator('.govuk-radios');
        this.multiSelectCheckboxes = this.questionFieldset.locator('.govuk-checkboxes');
        this.dropdownSelect = this.questionFieldset.locator('select.govuk-select');

        this.continueButton = this.questionForm.locator(
            'button.govuk-button[type="submit"]'
        );

        this.externalLinkHiddenInput = this.pageOrFrame.locator('#external-link-dest');

        this.resultsPageFieldset = this.pageOrFrame.locator('fieldset.govuk-fieldset');
        this.resultsPageHeading = this.pageOrFrame.locator('#results-page-title');
        this.resultsPageDescription = this.pageOrFrame.locator('#results-page-details').first();
    }

    // ===== Type detection helpers =====

    async isQuestionMode(): Promise<boolean> {
        return this.questionForm.isVisible();
    }

    async isResultsPageMode(): Promise<boolean> {
        const isResultsPageTitleVisible = await this.resultsPageHeading.isVisible();
        const isResultsPageDescVisible = await this.resultsPageDescription.isVisible();
        
        return isResultsPageTitleVisible && isResultsPageDescVisible;
    }

    async isExternalLinkMode(): Promise<boolean> {
        return this.externalLinkHiddenInput.isVisible();
    }

    // ===== Assertions =====

    async assertQuestionStructure(embed: boolean = false): Promise<void> {
        await expect(this.questionForm, '❌ Question form missing').toBeVisible();
        await expect(this.questionFieldset, '❌ Question fieldset missing').toBeVisible();
        await expect(this.questionHeading, '❌ Question heading missing').toBeVisible();

        const headingText = (await this.questionHeading.textContent())?.trim() ?? '';
        expect(headingText.length).toBeGreaterThan(0);

        const hasHint = await this.questionHint.isVisible().catch(() => false);
        if (hasHint) {
            const hintText = (await this.questionHint.textContent())?.trim() ?? '';
            expect(hintText.length).toBeGreaterThan(0);
        }

        await expect(this.continueButton).toBeVisible();
        await expect(this.continueButton).toHaveText(/Continue/i);
    }

    async assertSingleSelectQuestion(embed: boolean = false): Promise<void> {
        await this.assertQuestionStructure(embed);
        await expect(this.singleSelectRadios, '❌ Radios container missing for single-select question').toBeVisible();

        const radioInputs = this.singleSelectRadios.locator('input.govuk-radios__input[type="radio"]');
        const count = await radioInputs.count();
        expect(count).toBeGreaterThan(0);
    }

    async assertMultiSelectQuestion(): Promise<void> {
        await this.assertQuestionStructure();
        await expect(this.multiSelectCheckboxes, '❌ Checkboxes container missing for multi-select question').toBeVisible();

        const checkboxInputs = this.multiSelectCheckboxes.locator('input.govuk-checkboxes__input[type="checkbox"]');
        const count = await checkboxInputs.count();
        expect(count).toBeGreaterThan(0);
    }

    async assertDropdownQuestion(): Promise<void> {
        await this.assertQuestionStructure();
        await expect(this.dropdownSelect, '❌ Dropdown select missing for dropdown question').toBeVisible();

        const options = this.dropdownSelect.locator('option');
        const count = await options.count();
        expect(count).toBeGreaterThan(1); // includes placeholder + at least one answer
    }

    async assertResultsPage(title: string, description: string): Promise<void> {
        await expect(this.resultsPageHeading).toBeVisible();
        const headingText = (await this.resultsPageHeading.textContent())?.trim() ?? '';
        expect(headingText).toBe(title);

        await expect(this.resultsPageDescription).toBeVisible();
        const text = (await this.resultsPageDescription.textContent())?.trim() ?? '';
        expect(text).toBe(description);
    }

    async assertExternalLinkModeHasHiddenDestination(): Promise<void> {
        await expect(this.externalLinkHiddenInput).toBeVisible();
        const value = await this.externalLinkHiddenInput.getAttribute('value');
        expect(value).not.toBeNull();
        expect(value!.length).toBeGreaterThan(0);
    }

    async expectErrorSummary(): Promise<void> {
        await expect(this.errorSummary).toBeVisible();
        await expect(this.errorSummary).toHaveAttribute('role', 'alert');
        await expect(this.errorSummary).toHaveAttribute('tabindex', '-1');
    }

    // ===== Actions =====

    async selectFirstRadioOption(): Promise<void> {
        const firstRadio = this.singleSelectRadios
            .locator('input.govuk-radios__input[type="radio"]')
            .first();

        await firstRadio.check();
        await expect(firstRadio).toBeChecked();
    }

    async selectFirstCheckboxOption(): Promise<void> {
        const firstCheckbox = this.multiSelectCheckboxes
            .locator('input.govuk-checkboxes__input[type="checkbox"]')
            .first();

        await firstCheckbox.check();
        await expect(firstCheckbox).toBeChecked();
    }

    async selectLastCheckboxOption() {
        const lastCheckbox = this.multiSelectCheckboxes
            .locator('input.govuk-checkboxes__input[type="checkbox"]')
            .last();

        await lastCheckbox.check();
        await expect(lastCheckbox).toBeChecked();
    }

    async unselectLastCheckboxOption(): Promise<void> {
        const firstCheckbox = this.multiSelectCheckboxes
            .locator('input.govuk-checkboxes__input[type="checkbox"]')
            .last();

        await firstCheckbox.uncheck();
        await expect(firstCheckbox).not.toBeChecked();
    }

    /**
     * Unselect nth checkbox option.
     * 
     * @param nth 1-based index of checkbox option to unselect.
     * 
     * Note: negative nth values, find in reverse order of the list. 
     * e.g. 
     * nth=-1 unselects last checkbox option, 
     * nth=-2 unselects second-to-last checkbox option.
     */
    async unselectNthCheckboxOption(nth: number): Promise<void> {
        const allCheckboxes = this.multiSelectCheckboxes
            .locator('input.govuk-checkboxes__input[type="checkbox"]');
        
        const count = await allCheckboxes.count();
        const nthCheckbox = allCheckboxes.nth(nth < 0 ? count + nth : nth -1);
        await nthCheckbox.uncheck();
        await expect(nthCheckbox).not.toBeChecked();
    }

    async selectAllCheckboxOptions(): Promise<void> {
        const checkboxes = await this.multiSelectCheckboxes
            .locator('input.govuk-checkboxes__input[type="checkbox"]')
            .all()

        for (const checkbox of checkboxes) {
            await checkbox.check();
            await expect(checkbox).toBeChecked();
        }
    }

    async selectDropdownByIndex(index: number): Promise<void> {
        // index is 0-based including placeholder; callers should pass >=1 to skip placeholder
        await this.dropdownSelect.selectOption({ index });
    }

    async clickContinue(): Promise<void> {
        await this.continueButton.click();
        await this.waitForPageLoad();
    }

    async assertContinueButtonTextAndColor(
        expectedText: string, 
        expectedHexColor: string,
        expectedHexHoverColor?: string
    ): Promise<void> {
        const continueButtonColor = await this.continueButton.evaluate((el) =>
            window.getComputedStyle(el).getPropertyValue('background-color')
        );
        expect(continueButtonColor.length).toBeGreaterThan(0);
        expect(convertColorToHex(continueButtonColor)).toBe(expectedHexColor);

        if (expectedHexHoverColor) {
            await this.continueButton.focus();
            const hoveredButtonColor = await this.continueButton.evaluate((el) =>
                window.getComputedStyle(el).getPropertyValue('background-color')
            );
            expect(hoveredButtonColor.length).toBeGreaterThan(0);
            expect(convertColorToHex(hoveredButtonColor)).toBe(expectedHexHoverColor);
        }

        await expect(this.continueButton).toHaveText(expectedText);
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
                el.closest('.govuk-error-message, .govuk-error-summary, .govuk-hint') !== null
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

    async assertErrorComponentsColor(expectedHexColor: string): Promise<void> {
        // Error summary should have same color as border of error field group
        const errorSummaryColor = await this.errorSummary.evaluate((el) =>
            window.getComputedStyle(el).getPropertyValue('border-color')
        );
        expect(errorSummaryColor.length).toBeGreaterThan(0);
        expect(convertColorToHex(errorSummaryColor)).toBe(expectedHexColor);

        // Error field group should have same color as summary
        const errorFormGroupColor = await this.errorFormGroup.evaluate((el) =>
            window.getComputedStyle(el).getPropertyValue('border-left-color')
        );
        expect(errorFormGroupColor.length).toBeGreaterThan(0);
        expect(convertColorToHex(errorFormGroupColor)).toBe(expectedHexColor);

        // Error field message should have same color as summary
        const errorFieldMessageColor = await this.errorFieldMessage.evaluate((el) =>
            window.getComputedStyle(el).getPropertyValue('color')
        );
        expect(errorFieldMessageColor.length).toBeGreaterThan(0);
        expect(convertColorToHex(errorFieldMessageColor)).toBe(expectedHexColor);

        // Error summary list link should have same color as summary
        const errorSummaryListLinkColor = await this.errorSummaryListLink.evaluate((el) =>
            window.getComputedStyle(el).getPropertyValue('color')
        );
        expect(errorSummaryListLinkColor.length).toBeGreaterThan(0);
        expect(convertColorToHex(errorSummaryListLinkColor)).toBe(expectedHexColor);
    }
    
    async assertRedirectedToExternalLink(externalUrlPath: string): Promise<void> {
        // wait for page to redirect to 'externalUrl'
        await waitForRedirectTo(this.page, externalUrlPath);

        const url = this.page.url();

        expect(url).toContain(externalUrlPath);
    }

    async waitForResultsPageLoad() {
        await this.resultsPageHeading.waitFor({ state: 'visible' });
    }

    async waitForErrorStatePresent() {
        await this.errorSummary.waitFor({ state: 'visible' });
    }
}