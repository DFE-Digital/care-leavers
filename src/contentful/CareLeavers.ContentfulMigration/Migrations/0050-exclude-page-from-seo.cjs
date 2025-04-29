module.exports = function (migration) {
    const page = migration
        .editContentType("page")

    page
        .createField("excludeFromSitemap")
        .name("Exclude from sitemap")
        .type("Boolean")
        .localized(false)
        .required(true)
        .defaultValue({ "en-US": false })
        .disabled(false)
        .omitted(false)

    page.moveField("excludeFromSitemap").afterField("seoImage")

    const editorLayout = page.editEditorLayout()
    editorLayout.moveField('excludeFromSitemap').toTheBottomOfFieldGroup('seo')
    
    migration.transformEntries({
        contentType: 'page',
        from: ['excludeFromSitemap'],
        to: ['excludeFromSitemap'],
        transformEntryForLocale: function (fromFields, currentLocale) {
            return { excludeFromSitemap: false };
        }
    })
    
    
};
