import { test } from '@playwright/test';
import { HomePage } from '../pages/HomePage';

test.describe('Home Page Tests', () => {

    test('should load the homepage and verify elements', async ({ page }) => {
        const homePage = new HomePage(page);

        await homePage.navigate();
        await homePage.assertPageElements();
    });

});
