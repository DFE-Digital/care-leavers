import {test, expect} from '@playwright/test';
import {QuestionnaireStartPage} from "../pages/QuestionnaireStartPage";

test.describe('Check your Care Leaver Support page', () => {
    let startPage: QuestionnaireStartPage;        
    
    test('ContentFul Get to an answer questionnaire in frame works properly', async ({ page }) => {
        startPage = await QuestionnaireStartPage.create(page);
        await startPage.assertStructure(); 
    });
})