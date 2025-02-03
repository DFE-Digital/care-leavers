module.exports = function (migration) {
    const configuration = migration
        .createContentType("configuration")
        .name("Configuration")
        .description("A configuration entity that includes service details, navigation, and footer content.")
        .displayField("serviceName");
    
    configuration
        .createField("serviceName")
        .name("Service Name")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([])
        .disabled(false)
        .omitted(false);
    
    configuration
        .createField("phase")
        .name("Phase")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([
            {
                in: ["Alpha", "Beta", "Live"],
            },
        ])
        .defaultValue({ "en-US": "Beta" })
        .disabled(false)
        .omitted(false);
    
    configuration
        .createField("homePage")
        .name("Home Page")
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
    
    configuration
        .createField("navigation")
        .name("Navigation")
        .type("Array")
        .localized(false)
        .required(false)
        .validations([])
        .items({
            type: "Link",
            linkType: "Entry",
            validations: [
                {
                    linkContentType: ["navigationElement"], // Assuming "navigationElement" is the content type ID for NavigationElement
                },
            ],
        })
        .disabled(false)
        .omitted(false);
    
    configuration
        .createField("footer")
        .name("Footer")
        .type("RichText")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);
    
    configuration.changeFieldControl("serviceName", "builtin", "singleLine", {});
    configuration.changeFieldControl("phase", "builtin", "dropdown", {});
    configuration.changeFieldControl("homePage", "builtin", "entryLinkEditor", {});
    configuration.changeFieldControl("navigation", "builtin", "entryLinksEditor", {});
    configuration.changeFieldControl("footer", "builtin", "richTextEditor", {});
};
