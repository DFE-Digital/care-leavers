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

    /*test('should verify navigation works for each link', async () => {
        await homePage.verifyNavigation();
    });*/

    test('should validate footer links', async () => {
        await homePage.verifyFooterLinks();
    });

    test('should assert page elements are correct', async () => {
        await homePage.assertPageElements();
    });

    test('should verify navigation for all support cards', async () => {
        const supportCards = [
            { title: "Money and benefits", url: "/support-money-and-benefits" },
            { title: "Housing and accommodation", url: "/support-housing-and-accommodation" }
            //add more cards
        ];
        await homePage.verifySupportCardsNavigation(supportCards);
    });
});
