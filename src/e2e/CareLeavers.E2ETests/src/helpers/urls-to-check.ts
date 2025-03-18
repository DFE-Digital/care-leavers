// List of pages that will have common functionality across the site
// such as WebsiteName Link, Navigation Bar, Cookies, and Footers
export const commonPagesToTest = [
    '/en/home',  
    '/en/all-support',
    '/en/money-and-benefits',
    '/en/housing-and-accommodation',
    '/en/work-and-employment',
    '/en/education-and-training',
    '/en/health-and-wellbeing',
    '/en/unaccompanied-asylum-seeking-young-people',
    /*
   '/en/pathway-plan',
   '/en/personal-adviser',
   '/en/leaving-care-allowance',
   '/en/higher-education-bursary',
   '/en/local-offer-for-care-leavers',
   
   '/en/eligible-child',
   '/en/relevant-child',
   '/en/former-relevant-child',
   '/en/person-qualifying-for-advice-and-assistance',
   '/en/what-happens-when-you-leave-care',
   '/en/care-terms-explained',
    */
    '/en/your-rights',
    '/en/leaving-care-guides',
    '/en/helplines',
];

// List of helpline-related links to test-Only certain pages will have this link
export const helplineLinksToTest = [
    '/en/home',
    '/en/all-support',
];

// List of Pages that will have the share and print buttons
export const shareAndPrintLinksToTest = [
    '/en/money-and-benefits',
    '/en/housing-and-accommodation',
    '/en/work-and-employment',
    '/en/education-and-training',
    '/en/health-and-wellbeing',
    '/en/unaccompanied-asylum-seeking-young-people',
    '/en/helplines'
];

// List of URLS and their expected Breadcrumbs
export const breadcrumbTestData = [
    { urls: ['/en/all-support','/en/your-rights','/en/leaving-care-guides','/en/helplines'], expectedBreadcrumbs: ['Home'] },
    { urls: ['/en/money-and-benefits', '/en/housing-and-accommodation','/en/work-and-employment','/en/education-and-training',
            '/en/health-and-wellbeing','/en/unaccompanied-asylum-seeking-young-people','/en/pathway-plan','/en/personal-adviser',
            '/en/higher-education-bursary','/en/local-offer-for-care-leavers', '/en/leaving-care-allowance'], expectedBreadcrumbs: ['Home', 'All support'] },// add more as site grows
    { urls: ['/en/eligible-child','/en/relevant-child','/en/former-relevant-child','/en/person-qualifying-for-advice-and-assistance'], expectedBreadcrumbs: ['Home','Your rights'] },
    { urls: ['/en/what-happens-when-you-leave-care','/en/care-terms-explained'], expectedBreadcrumbs: ['Home','Leaving care guides'] },
    // Add more urls cases as needed
];

