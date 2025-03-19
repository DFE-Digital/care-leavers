module.exports = function (migration) {
    const checker = migration
        .editContentType("statusChecker")

    // Make sure answers shows as the right field type
    checker.changeFieldControl("answers", "builtin", "entryCardsEditor", { helpText: 'The answers to show within the checker' });
    
    const page = migration
        .editContentType("page")
    
    // Allow primary content to also contain status checker and Riddle
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
                                "page"
                            ],
                            "message": null
                        }
                    ]
                }
            }
        ])
    
};
