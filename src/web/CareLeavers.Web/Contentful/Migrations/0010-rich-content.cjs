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
                in: ["Light", "Dark", "None"], 
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
                in: ["Full", "Medium", "Narrow"],
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

    richContent.changeFieldControl("description", "builtin", "singleLine", {});
    richContent.changeFieldControl("background", "builtin", "dropdown", {});
    richContent.changeFieldControl("width", "builtin", "dropdown", {});
    richContent.changeFieldControl("content", "builtin", "richTextEditor", {});
};
