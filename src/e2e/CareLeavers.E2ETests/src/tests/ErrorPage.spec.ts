import { test} from '@playwright/test';
import { ErrorPage } from '../pages/ErrorPage';
test.describe('Error Page Tests', () => {
    
    let errorPage: ErrorPage;
    
    test.beforeEach(async ({ page }) => {
        errorPage = new ErrorPage(page);
    });
    
    test('should assert elements are correct for page not found', async () => {
        await errorPage.openErrorPage(); 
        await errorPage.assertPageElements();
    });

    test('geo-block renders Contentful error page', async ({page}, testInfo) => {
        if(testInfo.project.name === 'Mobile Safari'){
            test.skip(true, '302 redirect not supported by Webkit');
        }
        await page.route('**/en/all-support', route => {
            route.fulfill({
                status: 302,
                headers: {location:'/en/service-unavailable'},
            });
        });
        await errorPage.openErrorPageWithCorrectUrl();
        await errorPage.assertPageElements({checkUrl:true});
    });
});