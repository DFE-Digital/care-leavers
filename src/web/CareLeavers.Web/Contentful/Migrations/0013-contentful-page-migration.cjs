module.exports = function (migration) {
    const contentfulPage = migration
        .createContentType("contentfulPage")
        .name("Contentful Page")
        .description("A base content type representing a page with metadata and system properties.")
        .displayField("title");

    contentfulPage
        .createField("sys")
        .name("System Properties")
        .type("Object")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);

    contentfulPage
        .createField("metadata")
        .name("Metadata")
        .type("Object")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);

    contentfulPage
        .createField("title")
        .name("Title")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);

    contentfulPage
        .createField("slug")
        .name("Slug")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);

    contentfulPage
        .createField("fetched")
        .name("Fetched Date")
        .type("Date")
        .localized(false)
        .required(true)
        .validations([])
        .defaultValue({ "en-US": new Date().toISOString() })
        .disabled(false)
        .omitted(false);

    contentfulPage.changeFieldControl("sys", "builtin", "objectEditor", {});
    contentfulPage.changeFieldControl("metadata", "builtin", "objectEditor", {});
    contentfulPage.changeFieldControl("title", "builtin", "singleLine", {});
    contentfulPage.changeFieldControl("slug", "builtin", "singleLine", {});
    contentfulPage.changeFieldControl("fetched", "builtin", "datePicker", {});
};
