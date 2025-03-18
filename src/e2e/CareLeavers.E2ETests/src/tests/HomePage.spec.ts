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
            { title: "Money and benefits", url: "/en/money-and-benefits" },
            { title: "Housing and accommodation", url: "/en/housing-and-accommodation" },
            { title: "Work and employment", url: "/en/work-and-employment" },
            { title: "Education and training", url: "/en/education-and-training" },
            { title: "Health and wellbeing", url: "/en/health-and-wellbeing" },
            {title: "Unaccompanied asylum-seeking young people", url: "/en/unaccompanied-asylum-seeking-young-people"}
           //add more cards if and when needed
        ];
        await homePage.verifySupportCardsNavigation(supportCards);
    });
});
