module.exports = function (migration) {
    const printableCollection = migration
        .createContentType("printableCollection")
        .name("Printable Collection")
        .description("A collection of pages for visitors of the site to print off.")
        .displayField("title");

    printableCollection
        .createField("title")
        .name("Title")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([])
        .disabled(false)
        .omitted(false);

    printableCollection
        .createField("identifier")
        .name("Identifier")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([
            {
                unique: true,
                message: 'The slug for the site must be unique'
            },
            {
                "prohibitRegexp": {
                    "pattern": "^(translate-this-page|cookie-policy|cookies-policy|privacy-policies|accessibility-statement|service-unavailable|error|sitemap|page-not-found|statuschecker)$",
                    "flags": "ium"
                },
                message: 'You cannot use any of the following identifiers: translate-this-page, cookie-policy, cookies-policy, privacy-policies, accessibility-statement, service-unavailable, error, sitemap, page-not-found, statuschecker'
            }
        ])
        .disabled(false)
        .omitted(false);

    printableCollection
        .createField("summary")
        .name("Summary")
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
                    "embedded-asset-block"
                ],
                message: "Only heading 3, heading 4, ordered list, unordered list, and asset nodes are allowed"
            }
        ])
        .disabled(false)
        .omitted(false);

    printableCollection
        .createField("content")
        .name("Content")
        .type("Array")
        .localized(false)
        .required(false)
        .validations([])
        .items({
            type: "Link",
            linkType: "Entry",
            validations: [
                {
                    linkContentType: ["page"],
                },
            ],
        })
        .disabled(false)
        .omitted(false);

    printableCollection.changeFieldControl("summary", "builtin", "richTextEditor", {});
    printableCollection.changeFieldControl("content", "builtin", "entryLinksEditor", {});
    printableCollection.changeFieldControl("identifier", "builtin", "slugEditor", {
            helpText: "Unique identifier for the collection - for example \"find-housing\" will then be available at /print/en/find-housing",
            trackingFieldId: "title" });
    
    
    const page = migration
        .editContentType("page")

    // Allow primary and secondary content to also contain direct links to printable content
    page
        .editField("mainContent")
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
                                "definitionBlock",
                                "callToAction",
                                "grid",
                                "richContentBlock",
                                "banner",
                                "statusChecker",
                                "riddle"
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
                                "page",
                                "printableCollection"
                            ],
                            "message": null
                        }
                    ]
                }
            }
        ])

    // Allow secondary page content to link to a definition
    page
        .editField("secondaryContent")
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
                                "definitionContent",
                                "callToAction",
                                "grid",
                                "richContentBlock",
                                "banner"
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
                                "page",
                                "printableCollection"
                            ],
                            "message": null
                        }
                    ]
                }
            }
        ])
    
    const richContent = migration
        .editContentType("richContent")

    // Allow rich content to link directly to the definition
    richContent
        .editField("content")
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
                                "banner",
                                "definitionContent"
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
                                "page",
                                "printableCollection"
                            ],
                            "message": null
                        }
                    ]
                }
            }
        ])
    
    const card = migration
        .editContentType("card")

    card
        .editField("link")
        .validations([
            {
                linkContentType: ["page", "printableCollection"],
            },
        ])

    const configuration = migration
        .editContentType("configuration")

    configuration.editField("footer")
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
                    "embedded-asset-block",
                    "entry-hyperlink",
                    "hyperlink",
                    "hr"
                ],
                message: "Only heading 2, heading 3, heading 4, ordered list, unordered list, block entry, asset, link to entry, link to Url, inline entry, and horizontal rule nodes are allowed"
            },
            {
                nodes: {
                    "entry-hyperlink": [
                        {
                            "linkContentType": [
                                "page",
                                "printableCollection"
                            ],
                            "message": null
                        }
                    ]
                }
            }
        ])
};
