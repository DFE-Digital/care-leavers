// List of pages that will have common functionality across the site
// such as WebsiteName Link, Navigation Bar, Cookies, and Footers
export const commonPagesToTest = [
    '/home',  
    '/all-support',
    '/money-and-benefits',
    '/housing-and-accommodation',
    '/work-and-employment',
    '/education-and-training',
    '/health-and-wellbeing',
    '/unaccompanied-asylum-seeking-young-people',
    '/pathway-plan',
    '/personal-adviser',
    '/leaving-care-allowance',
    '/higher-education-bursary',
    '/local-offer-for-care-leavers',
    '/your-rights',
    '/eligible-child',
    '/relevant-child',
    '/former-relevant-child',
    '/person-qualifying-for-advice-and-assistance',
    '/leaving-care-guides',
    /*
    '/en/what-happens-when-you-leave-care',
    '/en/care-terms-explained',
    */
    '/helplines',
];

// List of helpline-related links to test-Only certain pages will have this link
export const helplineLinksToTest = [
    '/home',
    '/all-support',
    '/money-and-benefits',
    '/housing-and-accommodation',
    '/work-and-employment',
    '/education-and-training',
    '/health-and-wellbeing',
    '/unaccompanied-asylum-seeking-young-people',
    '/pathway-plan',
    '/personal-adviser',
    '/leaving-care-allowance',
    '/higher-education-bursary',
    '/local-offer-for-care-leavers',
    '/your-rights',
    '/eligible-child',
    '/relevant-child',
    '/former-relevant-child',
    '/person-qualifying-for-advice-and-assistance',
    '/leaving-care-guides',
    '/en/what-happens-when-you-leave-care',
    '/en/care-terms-explained'
];

// List of Pages that will have the share and print buttons
export const shareAndPrintLinksToTest = [
    '/money-and-benefits',
    '/housing-and-accommodation',
    '/work-and-employment',
    '/education-and-training',
    '/health-and-wellbeing',
    '/unaccompanied-asylum-seeking-young-people',
    '/pathway-plan',
    '/personal-adviser',
    '/leaving-care-allowance',
    '/higher-education-bursary',
    '/local-offer-for-care-leavers',
    '/eligible-child',
    '/relevant-child',
    '/former-relevant-child',
    '/person-qualifying-for-advice-and-assistance',
    '/en/what-happens-when-you-leave-care',
    '/en/care-terms-explained',
    '/helplines'
];

// List of URLS and their expected Breadcrumbs
export const breadcrumbTestData = [
    { urls: ['/all-support','/your-rights','/leaving-care-guides','/helplines'], expectedBreadcrumbs: ['Home'] },
    { urls: ['/money-and-benefits', '/housing-and-accommodation','/work-and-employment','/education-and-training','/health-and-wellbeing','/unaccompanied-asylum-seeking-young-people','/pathway-plan','/personal-adviser','/higher-education-bursary','/local-offer-for-care-leavers'], expectedBreadcrumbs: ['Home', 'All support'] },// add more as site grows
    { urls: ['/leaving-care-allowance',], expectedBreadcrumbs: ['Home', 'All support','Housing and accommodation'] },
    { urls: ['/eligible-child','/relevant-child','/former-relevant-child','/person-qualifying-for-advice-and-assistance'], expectedBreadcrumbs: ['Home','Your rights'] },
    { urls: ['/what-happens-when-you-leave-care','/care-terms-explained'], expectedBreadcrumbs: ['Home','Leaving care guides'] },
    // Add more urls cases as needed
];

// List of support cards
export const supportCards = [
    { title: "Money and benefits", url: "/money-and-benefits" },
    { title: "Housing and accommodation", url: "/housing-and-accommodation" },
    { title: "Work and employment", url: "/work-and-employment" },
    { title: "Education and training", url: "/education-and-training" },
    { title: "Health and wellbeing", url: "/health-and-wellbeing" },
    { title: "Unaccompanied asylum-seeking young people", url: "/unaccompanied-asylum-seeking-young-people" }
];

