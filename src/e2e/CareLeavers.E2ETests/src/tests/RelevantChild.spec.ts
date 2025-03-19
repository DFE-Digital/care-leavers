import { test } from '@playwright/test';
import { RelevantChildPage } from '../pages/RelevantChildPage';

test.describe('Relevant Child Page Tests', () => {
    let relevantChildPage: RelevantChildPage;

    test.beforeEach(async ({ page }) => {
        relevantChildPage = new RelevantChildPage(page);
        await relevantChildPage.openRelevantChildPage();
    });

    test('should verify all main sections are present', async () => {
        await relevantChildPage.verifySectionsVisibility();
    });

    test('should assert page elements are correct', async () => {
        await relevantChildPage.assertPageElements();
    });
});
