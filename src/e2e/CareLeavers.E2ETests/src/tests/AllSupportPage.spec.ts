import { test } from '@playwright/test';
import { AllSupportPage } from '../pages/AllSupportPage';
import {supportCards} from "../helpers/urls-to-check";

test.describe('All Support Page Tests', () => {
    let allSupportPage: AllSupportPage;

    test.beforeEach(async ({ page }) => {
        allSupportPage = new AllSupportPage(page);
        await allSupportPage.openAllSupportPage();
    });

    test('should assert page elements are correct', async () => {
        await allSupportPage.assertPageElements();
    });

    test('should verify navigation for all support cards', async () => {
        await allSupportPage.verifySupportCardsNavigation(supportCards);
    });

    test('should verify "Know what support you can get" section', async () => {
        await allSupportPage.verifyKnowWhatSupportSection();
    });
});
