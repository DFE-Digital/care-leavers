module.exports = function (migration) {
    migration.createTag('homepage').name('Homepage');
    migration.createTag('status-checker').name('Status Checker');
    migration.createTag('category-money-and-benefits').name('Category: Money and Benefits');
    migration.createTag('category-housing-and-accommodation').name('Category: Housing and Accommodation');
    migration.createTag('category-work-and-employment').name('Category: Work and Employment');
    migration.createTag('category-health-and-wellbeing').name('Category: Health and Wellbeing');
    migration.createTag('category-education-and-training').name('Category: Education and Training');
    migration.createTag('category-uasc').name('Category: UASC');
};
