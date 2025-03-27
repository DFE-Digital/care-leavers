module.exports = function (migration) {
    // Get our content types we want to change
    const definitionLink = migration
        .editContentType("definitionLink");
    
    const definitionContent = migration
        .editContentType("definitionContent");
    
    const richContent = migration
        .editContentType("richContent");

    const page = migration
        .editContentType("page");
    
    // Remove the redundant definition block
    migration.deleteContentType("definitionBlock");

    // Rename definition content to definition
    definitionContent
        .name('Definition')
    
    // Rename the field
    definitionLink
        .changeFieldId('definitionBlock', 'definition')
    
    // Change the name to match
    definitionLink
        .editField('definition')
        .name('Definition')
    
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
                                "page"
                            ],
                            "message": null
                        }
                    ]
                }
            }
        ])

    // Allow primary page content to link to a definition
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
                                "page"
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
                                "page"
                            ],
                            "message": null
                        }
                    ]
                }
            }
        ])
};
