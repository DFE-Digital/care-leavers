import { test } from '@playwright/test';
import { FormerRelevantPage } from '../pages/FormerRelevantPage';

test.describe('Former Relevant Child Page Tests', () => {
    let formerRelevantPage: FormerRelevantPage;

    test.beforeEach(async ({ page }) => {
        formerRelevantPage = new FormerRelevantPage(page);
        await formerRelevantPage.openFormerRelevantPage();
    });

    test('should verify all main sections are present', async () => {
        await formerRelevantPage.verifySectionsVisibility();
    });

    test('should assert page elements are correct', async () => {
        await formerRelevantPage.assertPageElements();
    });
});
