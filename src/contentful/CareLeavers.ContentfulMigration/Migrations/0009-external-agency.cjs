module.exports = function (migration) {
    const externalAgency = migration
        .createContentType("externalAgency")
        .name("External Agency")
        .description("A content type for storing details about an external agency, including name, URL, logo, and contact details.")
        .displayField("name");
    
    externalAgency
        .createField("name")
        .name("Name")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([
            {
                unique: true,
            }
        ])
        .disabled(false)
        .omitted(false);

    externalAgency
        .createField("url")
        .name("URL")
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

    externalAgency
        .createField("logo")
        .name("Logo")
        .type("Link")
        .localized(false)
        .required(false)
        .validations([])
        .linkType("Asset")
        .disabled(false)
        .omitted(false);

    externalAgency
        .createField("description")
        .name("Description")
        .type("Text")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);

    externalAgency
        .createField("call")
        .name("Call")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);
    
    externalAgency
        .createField("openingTimes")
        .name("Opening Times")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);

    externalAgency
        .createField("free")
        .name("Free")
        .type("Boolean")
        .localized(false)
        .required(false)
        .disabled(false)
        .omitted(false);

    externalAgency
        .createField("accessibility")
        .name("Accessibility")
        .type("Text")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);

    externalAgency.changeFieldControl("name", "builtin", "singleLine", {});
    externalAgency.changeFieldControl("url", "builtin", "urlEditor", {});
    externalAgency.changeFieldControl("logo", "builtin", "assetLinkEditor", {});
    externalAgency.changeFieldControl("description", "builtin", "markdown", {});
    externalAgency.changeFieldControl("call", "builtin", "singleLine", {});
    externalAgency.changeFieldControl("openingTimes", "builtin", "singleLine", {});
    externalAgency.changeFieldControl("free", "builtin", "boolean", {});
    externalAgency.changeFieldControl("accessibility", "builtin", "multipleLine", {});
};
