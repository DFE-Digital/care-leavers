module.exports = function (migration) {
    const redirectionRule = migration
        .createContentType("redirectionRules")
        .name("Redirection Rules")
        .description("A content type representing a page redirections")
        .displayField("title");

    redirectionRule
        .createField("title")
        .name("Title")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([])
        .disabled(false)
        .omitted(false);

    redirectionRule
        .createField("rules")
        .name("Rules")
        .type("Object")
        .localized(false)
        .required(true)
        .validations([])
        .disabled(false)
        .omitted(false);

    redirectionRule.changeFieldControl("title", "builtin", "singleLine", {});
};