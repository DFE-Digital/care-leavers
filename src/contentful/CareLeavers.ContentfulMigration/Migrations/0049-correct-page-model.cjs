module.exports = function (migration) {
    const page = migration
        .editContentType("page")

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
                                "spacer",
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
                                "banner",
                                "spacer"
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
                                "definitionContent",
                                "spacer"
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
    
    
};
