module.exports = function (migration) {
    const definitionLink = migration
        .createContentType("definitionLink")
        .name("Definition Link")
        .description("A content block that links to a definition.")
        .displayField("title");
    
    definitionLink
        .createField("title")
        .name("Title")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);
    
    definitionLink
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


    definitionLink.changeFieldControl("title", "builtin", "singleLine", {});
    definitionLink.changeFieldControl("definition", "builtin", "entryLinkEditor", {});
};
