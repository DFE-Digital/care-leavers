module.exports = function (migration) {
    const definitionContent = migration
        .createContentType("definitionContent")
        .name("Definition Content")
        .description("Stores a definition of a certain term.")
        .displayField("title");

    definitionContent
        .createField("title")
        .name("Title")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([])
        .disabled(false)
        .omitted(false);

    definitionContent
        .createField("definition")
        .name("Definition")
        .type("RichText")
        .localized(false)
        .required(false)
        .validations([
            {
                enabledMarks: [
                    "bold",
                    "italic"
                ]
            },
            {
                enabledNodeTypes: [
                    "ordered-list",
                    "unordered-list",
                    "asset-hyperlink",
                    "entry-hyperlink",
                    "hyperlink"
                ]
            }
        ])
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
