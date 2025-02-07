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
        .required(true)
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
                    linkContentType: ["navigationElement"],
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
        .validations([
            {
                enabledMarks: [
                    "bold",
                    "italic"
                ]
            },
            {
                enabledNodeTypes: [
                    "heading-2",
                    "heading-3",
                    "heading-4",
                    "ordered-list",
                    "unordered-list",
                    "embedded-entry-block",
                    "embedded-asset-block",
                    "entry-hyperlink",
                    "hyperlink",
                    "embedded-entry-inline",
                    "hr"
                ],
                message: "Only heading 2, heading 3, heading 4, ordered list, unordered list, block entry, asset, link to entry, link to Url, inline entry, and horizontal rule nodes are allowed"
            },
            {
                nodes: {
                    "embedded-entry-block": [
                        {
                            "linkContentType": [
                                "callToAction",
                                "grid",
                                "richContentBlock",
                                "definitionBlock"
                            ],
                            "message": null
                        }
                    ],
                    "embedded-entry-inline": [
                        {
                            "linkContentType": [
                                "definitionLink"
                            ],
                            "message": null
                        }
                    ],
                    "entry-hyperlink": [
                        {
                            "linkContentType": [
                                "page"
                            ],
                            "message": null
                        }
                    ]
                }
            }
        ])
        .disabled(false)
        .omitted(false);
    
    configuration.changeFieldControl("serviceName", "builtin", "singleLine", {});
    configuration.changeFieldControl("phase", "builtin", "dropdown", {});
    configuration.changeFieldControl("homePage", "builtin", "entryLinkEditor", {});
    configuration.changeFieldControl("navigation", "builtin", "entryLinksEditor", {});
    configuration.changeFieldControl("footer", "builtin", "richTextEditor", {});
};
