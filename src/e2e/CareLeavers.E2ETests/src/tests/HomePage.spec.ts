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
    
    test('should assert page elements are correct', async () => {
        await homePage.assertPageElements();
    });

    test('should verify navigation for all support cards', async () => {
        const supportCards = [
            { title: "Money and benefits", url: "/money-and-benefits" },
            { title: "Housing and accommodation", url: "/housing-and-accommodation-support" }
            //add more cards
        ];
        await homePage.verifySupportCardsNavigation(supportCards);
    });
});
