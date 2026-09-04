import { test } from '@playwright/test';
import { YourRightsPage } from '../pages/YourRightsPage';

test.describe('Your Rights Page Tests', () => {
    let yourRightsPage: YourRightsPage;

    test.beforeEach(async ({ page }) => {
        yourRightsPage = new YourRightsPage(page);
        await yourRightsPage.openYourRightsPage();
    });

    test('should verify all main sections are present', async () => {
        await yourRightsPage.verifySectionsVisibility();
    });

    test('should assert page elements are correct', async () => {
        await yourRightsPage.assertPageElements();
    });

    test('should check contentful banner exists on page', async () => {
        await yourRightsPage.assertBannerExists();
    });

    test('should verify Contentful Definition link exists', async () => {
        await yourRightsPage.verifyContentfulDefinitionLink();
    });

    test('should verify Contentful Card exists', async () => {
        await yourRightsPage.verifyContentfulCardExists();
    });

    test('should verify Contentful Definition exists', async () => {
        await yourRightsPage.verifyContentfulDefinitionExists();
    });

    test('should verify Contentful Grid exists', async () => {
        await yourRightsPage.verifyContentfulGridExists();
    });

    test('should verify Contentful NavigationLink exists', async () => {
        await yourRightsPage.verifyContentfulNavigationLinkExists();
    });
});
    