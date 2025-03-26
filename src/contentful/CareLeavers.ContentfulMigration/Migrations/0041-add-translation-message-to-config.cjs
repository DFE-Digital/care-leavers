module.exports = function (migration) {
    const configuration = migration
        .editContentType("configuration")

    configuration
        .createField("translationHeader")
        .name("Translation Header Content")
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
                    "embedded-asset-block",
                    "hyperlink",
                    "hr"
                ],
                message: "Only heading 3, heading 4, ordered list, unordered list, asset, link to Url, and horizontal rule nodes are allowed"
            }
        ])
        .disabled(false)
        .omitted(false);

    configuration.changeFieldControl("translationHeader", "builtin", "richTextEditor", {});
};
