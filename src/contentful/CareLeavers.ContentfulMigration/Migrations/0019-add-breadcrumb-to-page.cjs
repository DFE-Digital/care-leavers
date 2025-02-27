module.exports = function (migration) {
    const page = migration
        .editContentType("page");

    page
        .createField("showBreadcrumb")
        .name("Show Breadcrumb")
        .type("Boolean")
        .localized(false)
        .required(true)
        .defaultValue({ "en-US": true })
        .disabled(false)
        .omitted(false);

    
    page.moveField("showBreadcrumb").afterField("type");

    page.changeFieldControl("showBreadcrumb", "builtin", "boolean", { helpText: 'Whether or not to show the breadcrumb links at the top of the page' });

    const editorLayout = page.editEditorLayout()
    editorLayout.moveField('showBreadcrumb').toTheBottomOfFieldGroup('layout')
    editorLayout.moveField('showBreadcrumb').afterField('type')
};
