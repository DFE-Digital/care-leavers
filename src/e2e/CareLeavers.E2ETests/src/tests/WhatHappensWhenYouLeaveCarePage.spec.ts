import { test } from '@playwright/test';
import { WhatHappensWhenYouLeaveCarePage } from '../pages/WhatHappensWhenYouLeaveCarePage';

test.describe('What Happens When You Leave Care Page Tests', () => {
    let whatHappensPage: WhatHappensWhenYouLeaveCarePage;

    test.beforeEach(async ({ page }) => {
        whatHappensPage = new WhatHappensWhenYouLeaveCarePage(page);
        await whatHappensPage.openWhatHappensPage();
    });

    test('should assert page elements are correct', async () => {
        await whatHappensPage.assertPageElements();
    });

    test('should verify table of contents navigation', async () => {
        await whatHappensPage.verifyTOCNavigation();
    });
});
