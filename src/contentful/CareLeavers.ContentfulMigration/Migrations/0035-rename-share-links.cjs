module.exports = function (migration) {
    const page = migration
        .editContentType("page");

    page
        .changeFieldId('showShareThis', 'showShareLinks')
    
    page
        .editField('showShareLinks')
        .name('Show sharing links on this page')
};