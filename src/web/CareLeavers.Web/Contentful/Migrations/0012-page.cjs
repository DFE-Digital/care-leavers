module.exports = function (migration) {
    const page = migration
        .createContentType("page")
        .name("Page")
        .description("A content type representing a webpage with SEO, layout, and content fields.")
        .displayField("seoTitle");

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
        .createField("title")
        .name("Title")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);

    page
        .createField("slug")
        .name("Slug")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);

    page
        .createField("seoTitle")
        .name("SEO Title")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);

    page
        .createField("seoDescription")
        .name("SEO Description")
        .type("Text")
        .localized(false)
        .required(false)
        .validations([])
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
                in: ["Full", "TwoThirds"],
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
                nodes: {
                    "heading-2": {},
                    "heading-3": {},
                    "unordered-list": {},
                    "list-item": {},
                    "hr": {},
                    "hyperlink": {},
                    "entry-hyperlink": {},
                    "embedded-entry-block": {},
                    "embedded-entry-inline": {},
                    "embedded-asset-block": {},
                },
                marks: {
                    type: ["bold", "italic"],
                },
            },
        ])
        .disabled(false)
        .omitted(false);

    page
        .createField("mainContent")
        .name("Main Content")
        .type("RichText")
        .localized(false)
        .required(false)
        .validations([
            {
                nodes: {
                    "heading-2": {},
                    "heading-3": {},
                    "unordered-list": {},
                    "list-item": {},
                    "hr": {},
                    "hyperlink": {},
                    "entry-hyperlink": {},
                    "embedded-entry-block": {},
                    "embedded-entry-inline": {},
                    "embedded-asset-block": {},
                },
                marks: {
                    type: ["bold", "italic"],
                },
            },
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
                nodes: {
                    "heading-2": {},
                    "heading-3": {},
                    "unordered-list": {},
                    "list-item": {},
                    "hr": {},
                    "hyperlink": {},
                    "entry-hyperlink": {},
                    "embedded-entry-block": {},
                    "embedded-entry-inline": {},
                    "embedded-asset-block": {},
                },
                marks: {
                    type: ["bold", "italic"],
                },
            },
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