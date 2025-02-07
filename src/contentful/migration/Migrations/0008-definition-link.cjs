module.exports = function (migration) {
    const definitionLink = migration
        .createContentType("definitionLink")
        .name("Definition Link")
        .description("A content type for storing the link to the definition.")
        .displayField("title");

    definitionLink
        .createField("title")
        .name("Title")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([])
        .disabled(false)
        .omitted(false);

    definitionLink
        .createField("definition")
        .name("Definition")
        .type("Link")
        .localized(false)
        .required(false)
        .linkType("Entry")
        .validations([
            {
                linkContentType: [
                    "definitionBlock"
                ]
            }
        ])
        .disabled(false)
        .omitted(false);

    definitionLink.changeFieldControl("title", "builtin", "singleLine", {});
    definitionLink.changeFieldControl("definition", "builtin", "entryLinkEditor", {});
};
