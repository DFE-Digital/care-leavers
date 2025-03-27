import { test } from '@playwright/test';
import { YourRightsPage } from '../pages/YourRightsPage';

test.describe('Your Rights Page Tests', () => {
    let yourRightsPage: YourRightsPage;

    test.beforeEach(async ({ page }) => {
        yourRightsPage = new YourRightsPage(page);
        await yourRightsPage.openYourRightsPage();
    });

    test('should verify all main sections are present', async () => {
        await yourRightsPage.verifySectionsVisibility();
    });

    test('should assert page elements are correct', async () => {
        await yourRightsPage.assertPageElements();
    });
});
