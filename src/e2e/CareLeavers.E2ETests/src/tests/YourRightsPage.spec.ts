import { expect, test } from '@playwright/test';
import { YourRightsPage } from '../pages/YourRightsPage';

test.describe('Your Rights Page Tests', () => {
    let yourRightsPage: YourRightsPage;

    test.beforeEach(async ({ page }) => {
        yourRightsPage = new YourRightsPage(page);
        await yourRightsPage.openYourRightsPage();
    });

    test('should verify all main sections are present', async () => {
        await expect(yourRightsPage.verifySectionsVisibility());
    });

    test('should assert page elements are correct', async () => {
        await expect(yourRightsPage.assertPageElements());
    });

    test('should check contentful banner exists on page', async () => {
        await expect(yourRightsPage.assertBannerExists());
    });

    test('should verify Contentful Definition link exists', async () => {
        await expect(yourRightsPage.verifyContentfulDefinitionLink());
    });

    test('should verify Contentful Card exists', async () => {
        await expect(yourRightsPage.verifyContentfulCardExists());
    });

    test('should verify Contentful Definition exists', async () => {
        await expect(yourRightsPage.verifyContentfulDefinitionExists());
    });

    test('should verify Contentful Grid exists', async () => {
        await expect(yourRightsPage.verifyContentfulGridExists());
    });

    test('should verify Contentful NavigationLink exists', async () => {
        await expect(yourRightsPage.verifyContentfulNavigationLinkExists());
    });
});
    