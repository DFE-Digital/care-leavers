module.exports = function (migration) {
    const richContent = migration
        .createContentType("richContentBlock")
        .name("Rich Content Block")
        .description("A block to contain multiple rows of rich content.")
        .displayField("title");

    richContent
        .createField("title")
        .name("Title")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([])
        .disabled(false)
        .omitted(false);

    richContent
        .createField("entries")
        .name("Entries")
        .type("Array")
        .localized(false)
        .required(false)
        .items({
            type: "Link",
            linkType: "Entry",
            validations: [
                {
                    "linkContentType": [
                        "richContent"
                    ]
                }
            ],
        })
        .validations([])
        .disabled(false)
        .omitted(false);

    richContent.changeFieldControl("title", "builtin", "singleLine", {});
};
