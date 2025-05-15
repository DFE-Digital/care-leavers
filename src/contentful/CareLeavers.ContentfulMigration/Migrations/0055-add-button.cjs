module.exports = function (migration) {
    const button = migration
        .createContentType("button")
        .name("Button")
        .description(
            "A GDS Design System Button"
        )
        .displayField("title");

    button
        .createField("title")
        .name("Title")
        .type("Symbol")
        .localized(false)
        .required(true);

    button
        .createField("type")
        .name("Button Type")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([
            {
                in: ["Default", "Secondary", "Warning", "Start"],
            },
        ])
        .defaultValue({ "en-US": "Default" })
        .disabled(false)
        .omitted(false);

    button
        .createField("text")
        .name("Button Text")
        .type("Symbol")
        .localized(false)
        .required(true);

    button
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

    button
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

    button.changeFieldControl("title", "builtin", "singleLine", {});
    button.changeFieldControl("type", "builtin", "dropdown", {});
    button.changeFieldControl("text", "builtin", "singleLine", {});
    button.changeFieldControl("internalDestination", "builtin", "entryLinkEditor", {});
    button.changeFieldControl("externalDestination", "builtin", "urlEditor", {});
    
};
