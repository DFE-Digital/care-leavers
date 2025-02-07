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
        .required(true)
        .validations([
            {
                in: ["Small", "Large"],
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
        .required(true)
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
                    "unordered-list"
                ]
            }
        ])
        .disabled(false)
        .omitted(false);
    
    callToAction
        .createField("callToActionText")
        .name("Call To Action Text")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([])
        .disabled(false)
        .defaultValue({"en-US": "Start now"})
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
        .validations([
            {
                regexp:{
                    pattern: "^(ftp|http|https):\\/\\/(\\w+:{0,1}\\w*@)?(\\S+)(:[0-9]+)?(\\/|\\/([\\w#!:.?+=&%@!\\-/]))?$",
                    flags: null
                }
            }
        ])
        .disabled(false)
        .omitted(false);
    
    callToAction.changeFieldControl("title", "builtin", "singleLine", {});
    callToAction.changeFieldControl("size", "builtin", "dropdown", {});
    callToAction.changeFieldControl("content", "builtin", "richTextEditor", {});
    callToAction.changeFieldControl("callToActionText", "builtin", "singleLine", {});
    callToAction.changeFieldControl("internalDestination", "builtin", "entryLinkEditor", {});
    callToAction.changeFieldControl("externalDestination", "builtin", "singleLine", {});
};
