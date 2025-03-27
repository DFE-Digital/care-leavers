import { test } from '@playwright/test';
import { EligibleChildPage } from '../pages/EligibleChildPage';

test.describe('Eligible Child Page Tests', () => {
    let eligibleChildPage: EligibleChildPage;

    test.beforeEach(async ({ page }) => {
        eligibleChildPage = new EligibleChildPage(page);
        await eligibleChildPage.openEligibleChildPage();
    });

    test('should verify all main sections are present', async () => {
        await eligibleChildPage.verifySectionsVisibility();
    });
    
    test('should assert page elements are correct', async () => {
        await eligibleChildPage.assertPageElements();
    });
});
