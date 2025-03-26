import { test } from '@playwright/test';
import { LeavingCareGuidesPage } from '../pages/LeavingCareGuidesPage';

test.describe('Leaving Care Guides Page Tests', () => {
    let leavingCareGuidesPage: LeavingCareGuidesPage;

    test.beforeEach(async ({ page }) => {
        leavingCareGuidesPage = new LeavingCareGuidesPage(page);
        await leavingCareGuidesPage.openLeavingCareGuidesPage();
    });

    test('should assert page elements are correct', async () => {
        await leavingCareGuidesPage.assertPageElements();
    });

    test('should verify navigation for guide links', async () => {
        await leavingCareGuidesPage.verifyGuideLinksNavigation();
    });
});
