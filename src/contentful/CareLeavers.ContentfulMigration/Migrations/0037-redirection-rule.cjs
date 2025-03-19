module.exports = function (migration) {
    const page = migration
        .createContentType("redirectionRule")
        .name("Redirection Rule")
        .description("A content type representing a page redirection")
        .displayField("fromSlug");

    page
        .createField("fromSlug")
        .name("From Slug")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([
            {
                unique: true,
            }
        ])
        .disabled(false)
        .omitted(false);

    page
        .createField("toSlug")
        .name("To Slug")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([])
        .disabled(false)
        .omitted(false);

    page.changeFieldControl("fromSlug", "builtin", "slugEditor", {});
    page.changeFieldControl("toSlug", "builtin", "slugEditor", {});
};