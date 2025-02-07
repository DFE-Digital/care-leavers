module.exports = function (migration) {
    const callToAction = migration
        .createContentType("callToAction")
        .name("Call To Action")
        .description("A call-to-action block with a title, size, content, text, and destinations.")
        .displayField("title");
    
    callToAction
        .createField("title")
        .name("Title")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([
            {
                unique: true,
            },
        ])
        .disabled(false)
        .omitted(false);
    
    callToAction
        .createField("size")
        .name("Size")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([
            {
                in: ["Small", "Medium", "Large"],
            },
        ])
        .defaultValue({ "en-US": "Small" })
        .disabled(false)
        .omitted(false);
    
    callToAction
        .createField("content")
        .name("Content")
        .type("RichText")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);
    
    callToAction
        .createField("callToActionText")
        .name("Call To Action Text")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);
    
    callToAction
        .createField("internalDestination")
        .name("Internal Destination")
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
    
    callToAction
        .createField("externalDestination")
        .name("External Destination")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);
    
    callToAction.changeFieldControl("title", "builtin", "singleLine", {});
    callToAction.changeFieldControl("size", "builtin", "dropdown", {});
    callToAction.changeFieldControl("content", "builtin", "richTextEditor", {});
    callToAction.changeFieldControl("callToActionText", "builtin", "singleLine", {});
    callToAction.changeFieldControl("internalDestination", "builtin", "entryLinkEditor", {});
    callToAction.changeFieldControl("externalDestination", "builtin", "singleLine", {});
};
