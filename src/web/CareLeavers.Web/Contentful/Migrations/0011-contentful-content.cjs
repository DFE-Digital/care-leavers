module.exports = function (migration) {
    const contentfulContent = migration
        .createContentType("contentfulContent")
        .name("Contentful Content")
        .description("Base content type with system properties and metadata.")

    contentfulContent
        .createField("sys")
        .name("System Properties")
        .type("Object")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);

    contentfulContent
        .createField("metadata")
        .name("Metadata")
        .type("Object")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);

    contentfulContent
        .createField("fetched")
        .name("Fetched Date")
        .type("Date")
        .localized(false)
        .required(true)
        .validations([])
        .defaultValue({ "en-US": new Date().toISOString() })
        .disabled(false)
        .omitted(false);

    contentfulContent.changeFieldControl("sys", "builtin", "objectEditor", {});
    contentfulContent.changeFieldControl("metadata", "builtin", "objectEditor", {});
    contentfulContent.changeFieldControl("fetched", "builtin", "datePicker", {});
};
