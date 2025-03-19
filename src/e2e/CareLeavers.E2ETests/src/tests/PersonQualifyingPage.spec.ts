import { test } from '@playwright/test';
import { PersonQualifyingPage } from '../pages/PersonQualifyingPage';

test.describe('Person Qualifying for Advice and Assistance Page Tests', () => {
    let personQualifyingPage: PersonQualifyingPage;

    // Initialize the page before each test
    test.beforeEach(async ({ page }) => {
        personQualifyingPage = new PersonQualifyingPage(page);
        await personQualifyingPage.openPersonQualifyingPage(); // Navigate to the page
    });

    // Test to verify all main sections are visible on the page
    test('should verify all main sections are visible', async () => {
        await personQualifyingPage.verifySectionsVisibility();
    });

    // Test to assert specific page elements like URL, logo, and section visibility
    test('should assert page elements are correct', async () => {
        await personQualifyingPage.assertPageElements();
    });
});
