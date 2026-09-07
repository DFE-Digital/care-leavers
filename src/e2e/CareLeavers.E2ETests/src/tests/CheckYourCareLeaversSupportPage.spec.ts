import {test, expect} from '@playwright/test';
import {QuestionnaireStartPage} from "../pages/QuestionnaireStartPage";
import {CheckYourCareLeaverSupport} from "../pages/CheckYourCareLeaverSupport";

test.describe('Check your Care Leaver Support page', () => {
    let startPage: QuestionnaireStartPage;
    let checkPage: CheckYourCareLeaverSupport;          
    
    test.beforeEach(async ({ page }) => {
        checkPage = new CheckYourCareLeaverSupport(page);
        await checkPage.openCheckYourCareLeaverSupportPage();
    });  

    test('ContentFul Get to an answer questionnaire in Iframe exists', async ({ page }) => {
        startPage = await QuestionnaireStartPage.create(page);
        await expect(startPage.assertStructure());
    });
})