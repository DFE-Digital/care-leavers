// List of pages that will have common functionality across the site
// such as WebsiteName Link, Navigation Bar, Cookies, and Footers
export const commonPagesToTest = [
    '/home',  
    '/all-support',
    '/support-money-and-benefits',
    '/support-housing-and-accommodation',
    /*'/your-rights',
    '/guides-advice',
    '/helpline',*/
];

// List of helpline-related links to test-Only certain pages will have this link
export const helplineLinksToTest = [
    '/home',
    '/all-support',
];

// List of Pages that will have the share and print buttons
export const shareAndPrintLinksToTest = [
    '/category-money',
   // '/all-support',- commenting this for now as the buttons aren't visible on this page
];

// List of URLS and their expected Breadcrumbs
export const breadcrumbTestData = [
    { urls: ['/all-support'], expectedBreadcrumbs: ['Home'] },// add '/status','leaving-care-guides','helplines'
    { urls: ['/support-money-and-benefits', '/support-housing-and-accommodation'], expectedBreadcrumbs: ['Home', 'All support'] },// add more as site grows
    // Add more urls cases as needed
];