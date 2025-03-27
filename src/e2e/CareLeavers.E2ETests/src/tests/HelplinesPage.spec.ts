import { test } from '@playwright/test';
import { HelplinesPage } from '../pages/HelplinesPage';

test.describe('Helplines Page Tests', () => {
    let helplinesPage: HelplinesPage;

    test.beforeEach(async ({ page }) => {
        helplinesPage = new HelplinesPage(page);
        await helplinesPage.openHelplinesPage();
    });

    test('should display all sections', async () => {
        await helplinesPage.verifySectionsVisibility();
    });
    
    test('should assert page elements are correct', async () => {
        await helplinesPage.assertPageElements();
    });

});
