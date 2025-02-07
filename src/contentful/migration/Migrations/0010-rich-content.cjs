module.exports = function (migration) {
    const richContent = migration
        .createContentType("richContent")
        .name("Rich Content")
        .description("A content type for storing rich text with customizable background and width.")
        .displayField("description");

    richContent
        .createField("description")
        .name("Description")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);

    richContent
        .createField("background")
        .name("Background Colour")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([
            {
                in: ["Blue", "Grey", "Green"],
            },
        ])
        .disabled(false)
        .omitted(false);

    richContent
        .createField("width")
        .name("Content Width")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([
            {
                in: ["One Third", "Two Thirds", "Full Width"],
            },
        ])
        .disabled(false)
        .omitted(false);

    richContent
        .createField("content")
        .name("Content")
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
                message: "Only heading 3, heading 4, ordered list, unordered list, block entry, asset, link to entry, link to Url, inline entry, and horizontal rule nodes are allowed"
            },
            {
                nodes: {
                    "embedded-entry-block": [
                        {
                            "linkContentType": [
                                "callToAction",
                                "grid"
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

    richContent.changeFieldControl("description", "builtin", "singleLine", {});
    richContent.changeFieldControl("background", "builtin", "dropdown", {});
    richContent.changeFieldControl("width", "builtin", "dropdown", {});
    richContent.changeFieldControl("content", "builtin", "richTextEditor", {});
};
