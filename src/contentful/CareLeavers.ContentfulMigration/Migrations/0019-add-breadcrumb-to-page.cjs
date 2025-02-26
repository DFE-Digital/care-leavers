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

    page.changeFieldControl("showBreadcrumb", "builtin", "boolean", {});
};
