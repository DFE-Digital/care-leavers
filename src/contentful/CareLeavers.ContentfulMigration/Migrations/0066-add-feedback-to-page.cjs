module.exports = function (migration) {
    const page = migration.editContentType("page")

    page
        .createField("showQuickfeedback")
        .name("Show Feedback")
        .type("Boolean")
        .localized(false)
        .required(false)
        .disabled(false)
        .omitted(false)

    migration.transformEntries({
        contentType: 'page',
        from: [],
        to: ['showQuickfeedback'],
        transformEntryForLocale: function (fromFields) {
            return {
                showQuickfeedback: true
            };
        },
        shouldPublish: true
    });
};