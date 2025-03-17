import { test} from '@playwright/test';
import { HomePage } from '../pages/HomePage';
import {supportCards} from "../helpers/urls-to-check";


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
        await homePage.verifySupportCardsNavigation(supportCards);
    });

    test('should verify "Know what support you can get" section', async () => {
        await homePage.verifyKnowWhatSupportSection();
    });

    test('should verify "Guides" section', async () => {
        await homePage.verifyGuidesSection();
    });
});
