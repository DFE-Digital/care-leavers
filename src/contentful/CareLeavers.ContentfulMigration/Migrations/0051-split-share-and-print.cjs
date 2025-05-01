module.exports = function (migration) {
    const page = migration
        .editContentType("page")

    page
        .createField("showPrintButton")
        .name("Show Print Button")
        .type("Boolean")
        .localized(false)
        .required(true)
        .defaultValue({ "en-US": false })
        .disabled(false)
        .omitted(false)

    page.moveField("showPrintButton").afterField("showLastUpdated")
    page.moveField("showShareLinks").afterField("showPrintButton")
    
    const editorLayout = page.editEditorLayout()
    editorLayout.moveField('showPrintButton').toTheBottomOfFieldGroup('layout')
    editorLayout.moveField('showShareLinks').toTheBottomOfFieldGroup('layout')
    
    // Turn on printing and sharing for ALL pages
    migration.transformEntries({
        contentType: 'page',
        from: ['showPrintButton', 'showShareLinks', 'showLastUpdated'],
        to: ['showPrintButton', 'showShareLinks', 'showLastUpdated'],
        transformEntryForLocale: function (fromFields, currentLocale) {
            return {
                showPrintButton: true,
                showShareLinks: true,
                showLastUpdated: true
            };
        }
    })
    
    
};
