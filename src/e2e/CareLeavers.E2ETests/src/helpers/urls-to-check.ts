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
    '/en/your-rights',
    '/en/leaving-care-guides',
    '/en/helplines',
];

// List of helpline-related links to test-Only certain pages will have this link
export const helplineLinksToTest = commonPagesToTest.filter(path => path !== '/helplines');

// List of Pages that will have the share and print buttons
export const shareAndPrintLinksToTest = commonPagesToTest.filter(
    path => !['/home', '/all-support', '/your-rights', '/leaving-care-guides'].includes(path)
);

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

// List of Pages that will have the Metadata
export const metaDataLinksToTest = commonPagesToTest.filter(
    path => !['/home', '/all-support', '/your-rights', '/leaving-care-guides','/helplines'].includes(path)
);

// List of support cards
export const supportCards = [
    { title: "Money and benefits", url: "/money-and-benefits" },
    { title: "Housing and accommodation", url: "/housing-and-accommodation" },
    { title: "Work and employment", url: "/work-and-employment" },
    { title: "Education and training", url: "/education-and-training" },
    { title: "Health and wellbeing", url: "/health-and-wellbeing" },
    { title: "Unaccompanied asylum-seeking young people", url: "/unaccompanied-asylum-seeking-young-people" }
];

