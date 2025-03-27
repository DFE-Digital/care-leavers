import { test } from '@playwright/test';
import { CareTermsExplainedPage } from '../pages/CareTermsExplainedPage';

test.describe('Care Terms Explained Page Tests', () => {
    let careTermsPage: CareTermsExplainedPage;

    test.beforeEach(async ({ page }) => {
        careTermsPage = new CareTermsExplainedPage(page);
        await careTermsPage.openCareTermsPage();
    });

    test('should assert page elements are correct', async () => {
        await careTermsPage.assertPageElements();
    });

    test('should verify table of contents navigation', async () => {
        await careTermsPage.verifyTOCNavigation();
    });
});
