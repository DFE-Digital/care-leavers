module.exports = function (migration) {
    const definitionContent = migration
        .createContentType("definition")
        .name("Definition Content")
        .description("A content type for storing definitions with a title, content, and linked page.")
        .displayField("title");

    definitionContent
        .createField("title")
        .name("Title")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);

    definitionContent
        .createField("definition")
        .name("Definition")
        .type("RichText")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);
    
    definitionContent
        .createField("page")
        .name("Page")
        .type("Link")
        .localized(false)
        .required(false)
        .validations([
            {
                linkContentType: ["page"],
            },
        ])
        .linkType("Entry")
        .disabled(false)
        .omitted(false);
    
    definitionContent.changeFieldControl("title", "builtin", "singleLine", {});
    definitionContent.changeFieldControl("definition", "builtin", "richTextEditor", {});
    definitionContent.changeFieldControl("page", "builtin", "entryLinkEditor", {});
};
