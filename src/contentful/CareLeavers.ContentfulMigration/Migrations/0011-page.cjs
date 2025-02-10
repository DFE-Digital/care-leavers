module.exports = function (migration) {
    const page = migration
        .createContentType("page")
        .name("Page")
        .description("A content type representing a webpage with SEO, layout, and content fields.")
        .displayField("title");

    page
        .createField("title")
        .name("Title")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([])
        .disabled(false)
        .omitted(false);

    page
        .createField("slug")
        .name("Slug")
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

    page
        .createField("parent")
        .name("Parent Page")
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

    page
        .createField("seoTitle")
        .name("SEO Title")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([
            {
                size: {
                    "min": null,
                    "max": 60
                }
            }
        ])
        .disabled(false)
        .omitted(false);

    page
        .createField("seoDescription")
        .name("SEO Description")
        .type("Text")
        .localized(false)
        .required(false)
        .validations([
            {
                size: {
                    "min": null,
                    "max": 160
                }
            }
        ])
        .disabled(false)
        .omitted(false);

    page
        .createField("seoImage")
        .name("SEO Image")
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

    page
        .createField("width")
        .name("Page Width")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([
            {
                in: ["Full Width", "Two Thirds"],
            },
        ])
        .defaultValue({ "en-US": "TwoThirds" })
        .disabled(false)
        .omitted(false);

    page
        .createField("type")
        .name("Page Type")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([
            {
                in: ["Guide", "Advice"],
            },
        ])
        .disabled(false)
        .omitted(false);

    page
        .createField("showContentsBlock")
        .name("Show Contents Block")
        .type("Boolean")
        .localized(false)
        .required(true)
        .defaultValue({ "en-US": true })
        .disabled(false)
        .omitted(false);

    page
        .createField("showLastUpdated")
        .name("Show Last Updated")
        .type("Boolean")
        .localized(false)
        .required(true)
        .defaultValue({ "en-US": true })
        .disabled(false)
        .omitted(false);

    page
        .createField("showFooter")
        .name("Show Footer")
        .type("Boolean")
        .localized(false)
        .required(true)
        .defaultValue({ "en-US": true })
        .disabled(false)
        .omitted(false);

    page
        .createField("contentsHeadings")
        .name("Contents Headings")
        .type("Array")
        .localized(false)
        .required(false)
        .validations([])
        .items({
            type: "Symbol",
            validations: [{ in: ["H2", "H3", "H4"] }],
        })
        .disabled(false)
        .omitted(false);

    page
        .createField("header")
        .name("Header")
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
                                "definitionBlock",
                                "callToAction",
                                "grid",
                                "richContentBlock"
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

    page
        .createField("mainContent")
        .name("Main Content")
        .type("RichText")
        .localized(false)
        .required(true)
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
                                "richContentBlock"
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

    page
        .createField("secondaryContent")
        .name("Secondary Content")
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
                                "definitionBlock",
                                "callToAction",
                                "grid",
                                "richContentBlock"
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
    
    page.changeFieldControl("seoTitle", "builtin", "singleLine", {});
    page.changeFieldControl("seoDescription", "builtin", "multipleLine", {});
    page.changeFieldControl("seoImage", "builtin", "assetLinkEditor", {});
    page.changeFieldControl("width", "builtin", "dropdown", {});
    page.changeFieldControl("type", "builtin", "dropdown", {});
    page.changeFieldControl("showContentsBlock", "builtin", "boolean", {});
    page.changeFieldControl("showLastUpdated", "builtin", "boolean", {});
    page.changeFieldControl("showFooter", "builtin", "boolean", {});
    page.changeFieldControl("contentsHeadings", "builtin", "tagEditor", {});
    page.changeFieldControl("header", "builtin", "richTextEditor", {});
    page.changeFieldControl("mainContent", "builtin", "richTextEditor", {});
    page.changeFieldControl("secondaryContent", "builtin", "richTextEditor", {});
};