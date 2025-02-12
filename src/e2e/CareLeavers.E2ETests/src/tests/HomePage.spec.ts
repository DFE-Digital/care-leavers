import { test} from '@playwright/test';
import { HomePage } from '../pages/HomePage';

test.describe('Home Page Tests', () => {
    let homePage: HomePage;

    test.beforeEach(async ({ page }) => {
        homePage = new HomePage(page);
        await homePage.openHomePage();
    });

    test('should verify all main sections are present', async () => {
        await homePage.verifySectionsVisibility();
    });

    test('should verify navigation works for each link', async () => {
        await homePage.verifyNavigation();
    });

    test('should validate footer links', async () => {
        await homePage.verifyFooterLinks();
    });

    test('should assert page elements are correct', async () => {
        await homePage.assertPageElements();
    });
});
