import {test} from '@playwright/test';
import {ErrorPage} from '../pages/ErrorPage';

test.describe('Error Page Tests', () => {
    let errorPage: ErrorPage;
    let serviceUnavailableUrl: string
    let forbiddenUrl: string

    test.beforeEach(async ({page}) => {
        errorPage = new ErrorPage(page);
    });

    test('should assert elements are correct for page not found', async () => {
        await errorPage.openErrorPage();
        await errorPage.assertPageElements();
    });

    test('geo-block renders Contentful error page', async ({page}, testInfo) => {
        serviceUnavailableUrl = '/en/service-unavailable';

        if (testInfo.project.name === 'Mobile Safari') {
            test.skip(true, '302 redirect not supported by Webkit');
        }
        await page.route('**/en/all-support', route => {
            route.fulfill({
                status: 302,
                headers: {location: serviceUnavailableUrl},
            });
        });

        await errorPage.openErrorPageWithCorrectUrl();
        await errorPage.assertPageElements({urlPath: serviceUnavailableUrl});
    });

    test('Forbidden error page validation', async ({page}, testInfo) => {
        forbiddenUrl = '/en/error?statusCode=403';

        if (testInfo.project.name === 'Mobile Safari') {
            test.skip(true, '302 redirect not supported by Webkit');
        }

        await page.route('**/en/all-support', route => {
            route.fulfill({
                status: 302,
                headers: {location: forbiddenUrl},
            });
        });

        await errorPage.openErrorPageWithCorrectUrl();
        await errorPage.assertPageElements({urlPath: forbiddenUrl});
    });
});