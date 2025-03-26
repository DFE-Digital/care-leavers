module.exports = function (migration) {
    const configuration = migration
        .editContentType("configuration")

    configuration
        .createField("defaultSeoImage")
        .name("Default SEO Image")
        .type("Link")
        .localized(false)
        .required(false)
        .validations([
            {
                linkMimetypeGroup: ["image"],
            },
        ])
        .linkType("Asset")
        .disabled(false)
        .omitted(false);

    configuration.changeFieldControl("defaultSeoImage", "builtin", "assetLinkEditor", {});
    
};
