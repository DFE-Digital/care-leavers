import { test, expect } from '@playwright/test';
import { ShareButtons } from '../pages/ShareButtons';

const pagesToTest = [
    '/home',
    //'/all-support',
    /* '/page2', // Add additional pages if needed
    '/page3',*/
];

test.describe('Share Buttons', () => {
    let shareButtons: ShareButtons;

    test.beforeEach(async ({ page }) => {
        shareButtons = new ShareButtons(page);  
    });

    // Loop through pagesToTest
    for (const pageUrl of pagesToTest) {
        test(`should work correctly on ${pageUrl}`, async ({ context }) => {
            await shareButtons.navigateTo(pageUrl);  

            // Check visibility of buttons
            await shareButtons.areButtonsVisible();

            // Check Facebook share dialog URL
            const fbUrl = await shareButtons.clickFacebookButton(context);
            expect(fbUrl).toContain('facebook.com/sharer.php');

            // Check Twitter share dialog URL
            const twitterUrl = await shareButtons.clickTwitterButton(context);
            expect(twitterUrl).toContain('twitter.com/intent/tweet');

            // Check Email client popup URL
            const emailUrl = await shareButtons.clickEmailButton();
            expect(emailUrl).toContain('mailto:');

            // Check Print dialog type
            const printDialogType = await shareButtons.clickPrintButton();
            expect(printDialogType).toBe('beforeunload');
        });
    }
});
