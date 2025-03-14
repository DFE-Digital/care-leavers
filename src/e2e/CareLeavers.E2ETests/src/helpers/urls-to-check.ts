// List of pages that will have common functionality across the site
// such as WebsiteName Link, Navigation Bar, Cookies, and Footers
export const commonPagesToTest = [
    '/en/home',  
    '/en/all-support',
    /*
    '/en/money-and-benefits',
    '/en/housing-and-accommodation',
    '/en/work-and-employment',
    '/en/education-and-training',
    '/en/health-and-wellbeing',
    '/en/unaccompanied-asylum-seeking-young-people',
    '/en/pathway-plan',
    '/en/personal-adviser',
    '/en/leaving-care-allowance',
    '/en/higher-education-bursary',
    '/en/local-offer-for-care-leavers',
    '/en/your-rights',
    '/en/eligible-child',
    '/en/relevant-child',
    '/en/former-relevant-child',
    '/en/person-qualifying-for-advice-and-assistance',
    '/en/leaving-care-guides',
    '/en/what-happens-when-you-leave-care',
    '/en/care-terms-explained',
     */
    '/en/helplines',
];

// List of helpline-related links to test-Only certain pages will have this link
export const helplineLinksToTest = [
    '/home',
    '/all-support',
];

// List of Pages that will have the share and print buttons
export const shareAndPrintLinksToTest = [
    '/en/money-and-benefits',
    '/en/housing-and-accommodation',
    '/en/work-and-employment',
    '/en/education-and-training',
    '/en/health-and-wellbeing',
    '/en/unaccompanied-asylum-seeking-young-people',
    '/helplines'
];

// List of URLS and their expected Breadcrumbs
export const breadcrumbTestData = [
    { urls: ['/en/all-support'], expectedBreadcrumbs: ['Home'] },// add '/status','leaving-care-guides','helplines'
    { urls: ['/support-money-and-benefits', '/support-housing-and-accommodation'], expectedBreadcrumbs: ['Home', 'All support'] },// add more as site grows
    // Add more urls cases as needed
];

