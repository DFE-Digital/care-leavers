import {test, expect} from '@playwright/test';
import {QuestionnaireStartPage} from "../pages/QuestionnaireStartPage";
import {QuestionnaireNextPage} from "../pages/QuestionnaireNextPage";
import {CheckYourCareLeaverSupport} from "../pages/CheckYourCareLeaverSupport";

// only runs locally, for now, 
// comprehensive E2E contentful content changes need to be made manually to test this
test.describe.skip('Check your Care Leaver Support page', () => {
    let checkPage: CheckYourCareLeaverSupport;
    let startPage: QuestionnaireStartPage;
    let nextPage: QuestionnaireNextPage;

    const DefaultColors = {
        textColor: "#0b0c0c",
        backgroundColor: "#ffffff",
        primaryButtonColor: "#00703c",
        secondaryButtonColor: "#1d70b8",
        stateColor: "#ffdd00",
        errorMessageColor: "#c3432b"
    };

    test.beforeEach(async ({ page }) => {
        checkPage = new CheckYourCareLeaverSupport(page);
        await checkPage.openCheckYourCareLeaverSupportPage();
    });
    
    test('Questionnaire in frame works properly', async ({ page }) => {
        startPage = await QuestionnaireStartPage.create(page);
        await startPage.assertStructure();
        await startPage.expectErrorSummaryIfPresent();
        await startPage.clickStartNow();

        nextPage = await QuestionnaireNextPage.create(page);
        const isQuestionMode = await nextPage.isQuestionMode();
        expect(isQuestionMode).toBeTruthy();
    });

    test('Questionnaire in frame works properly with decorative image', async ({ page }) => {
        startPage = await QuestionnaireStartPage.create(page);
        await startPage.assertStructure();
        await expect(startPage.outerImage, '❌ Decorative image not shown on questionnaire start').toBeVisible();
        await startPage.expectErrorSummaryIfPresent();
        await startPage.clickStartNow();

        nextPage = await QuestionnaireNextPage.create(page);
        const isQuestionMode = await nextPage.isQuestionMode();
        expect(isQuestionMode).toBeTruthy();
    });

    test('Questionnaire in iframe uses current custom styling', async ({ request, page, context, browser }) => {
        startPage = await QuestionnaireStartPage.create(page);
        await startPage.assertStructure();

        // assert that the start page button text remains the same
        await startPage.assertStartButtonTextAndColor("Start now", DefaultColors.primaryButtonColor);
        await startPage.clickStartNow();

        nextPage = await QuestionnaireNextPage.create(page);

        // to trigger the error state
        await nextPage.clickContinue();

        await nextPage.waitForErrorStatePresent();

        await nextPage.assertContinueButtonTextAndColor("Continue", DefaultColors.primaryButtonColor);
        await nextPage.assertTextColor(DefaultColors.textColor);
        await nextPage.assertErrorComponentsColor(DefaultColors.errorMessageColor);
    });

    // Check your support for care leavers questionnaire doesn't have any custom pages
    test.skip('Custom content destination is rendered correctly in questionnaire iframe run', async ({ page }) => {
        nextPage = await QuestionnaireNextPage.create(page);
        await nextPage.assertSingleSelectQuestion();

        await nextPage.selectFirstRadioOption();
        await nextPage.clickContinue();

        await nextPage.waitForResultsPageLoad();

        const isCustomContent = await nextPage.isResultsPageMode();
        expect(isCustomContent).toBeTruthy();

        // await nextPage.assertResultsPage(content.title, content.content.replace(/\*\*markdown\*\*/g, 'markdown'));
    });

    test('External-link destination in embedded questionnaire iframe – hidden field is rendered instead of redirect', async ({ page }) => {
        startPage = await QuestionnaireStartPage.create(page);
        await startPage.clickStartNow();
        
        nextPage = await QuestionnaireNextPage.create(page);
        await nextPage.assertMultiSelectQuestion();

        await nextPage.selectLastCheckboxOption()
        await nextPage.clickContinue();
        
        await nextPage.assertRedirectedToExternalLink('/en/if-youre-not-sure');
    });

    test('Multi select question with multiple answers selected is prioritised correctly', async ({ page }) => {
        startPage = await QuestionnaireStartPage.create(page);
        await startPage.clickStartNow();
        
        nextPage = await QuestionnaireNextPage.create(page);
        await nextPage.assertMultiSelectQuestion();

        await nextPage.selectAllCheckboxOptions();

        await nextPage.clickContinue();

        await nextPage.assertRedirectedToExternalLink(
            '/en/results-unaccompanied-asylum-seeking-child')
    })

    test('Multi select question with multiple answers selected (excluding highest priority) is prioritised correctly', async ({ page, request }) => {
        startPage = await QuestionnaireStartPage.create(page);
        await startPage.clickStartNow();

        nextPage = await QuestionnaireNextPage.create(page);
        await nextPage.assertMultiSelectQuestion();

        await nextPage.selectAllCheckboxOptions();

        // highest priority option is unselected
        await nextPage.unselectNthCheckboxOption(-2);

        await nextPage.clickContinue();
        
        // TODO: replace this with the below assertion
        await nextPage.assertSingleSelectQuestion();

        // TODO: switch to this in the morning, as it's priority for the actual flow
        // await nextPage.assertRedirectedToExternalLink(
        //     '/en/if-youre-not-sure')
    })
})