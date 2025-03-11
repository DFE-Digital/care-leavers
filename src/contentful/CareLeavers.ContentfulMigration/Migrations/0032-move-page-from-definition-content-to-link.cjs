module.exports = function (migration) {
    const definitionLink = migration
        .editContentType("definitionLink");
    
    const definitionContent = migration
        .editContentType("definitionContent");
    
    definitionLink
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

    definitionContent
        .deleteField('page')


};
