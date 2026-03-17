module.exports = function (migration) {
    const richContent = migration
        .editContentType("richContent")

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
                    "hr",
                    "table"
                ],
                message: "Only heading 3, heading 4, ordered list, unordered list, block entry, asset, link to entry, link to Url, inline entry, table, and horizontal rule nodes are allowed"
            },
            {
                nodes: {
                    "embedded-entry-block": [
                        {
                            "linkContentType": [
                                "callToAction",
                                "grid",
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
        ]);
}