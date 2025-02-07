module.exports = function (migration) {
    const definitionBlock = migration
        .createContentType("definitionBlock")
        .name("Definition Block")
        .description("A content block that links to a definition.")
        .displayField("title");
    
    definitionBlock
        .createField("title")
        .name("Title")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);
    
    definitionBlock
        .createField("definition")
        .name("Definition")
        .type("Link")
        .localized(false)
        .required(false)
        .validations([
            {
                linkContentType: ["definitionContent"],
            },
        ])
        .linkType("Entry")
        .disabled(false)
        .omitted(false);
    
    definitionBlock
        .createField("showTitle")
        .name("Show Title")
        .type("Boolean")
        .localized(false)
        .required(true)
        .defaultValue({ "en-US": false })
        .disabled(false)
        .omitted(false);
    
    definitionBlock.changeFieldControl("title", "builtin", "singleLine", {});
    definitionBlock.changeFieldControl("definition", "builtin", "entryLinkEditor", {});
    definitionBlock.changeFieldControl("showTitle", "builtin", "boolean", {});
};
