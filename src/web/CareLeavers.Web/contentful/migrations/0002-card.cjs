module.exports = function (migration) {
    const card = migration
        .createContentType("card")
        .name("Card")
        .description(
            "A content block with a title, text, image, link, and additional configuration options."
        )
        .displayField("title");

    // Title field
    card
        .createField("title")
        .name("Title")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([
            {
                unique: true,
            },
        ])
        .disabled(false)
        .omitted(false);

    // Text field
    card
        .createField("text")
        .name("Text")
        .type("Text")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);

    // Image field
    card
        .createField("image")
        .name("Image")
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

    // Link field
    card
        .createField("link")
        .name("Link")
        .type("Link")
        .localized(false)
        .required(false)
        .validations([
            {
                linkContentType: ["page"], // Replace "page" with the Contentful content type ID of your `Page` model
            },
        ])
        .linkType("Entry")
        .disabled(false)
        .omitted(false);

    // Types field
    card
        .createField("types")
        .name("Types")
        .type("Array")
        .localized(false)
        .required(false)
        .validations([])
        .items({
            type: "Symbol",
            validations: [],
        })
        .disabled(false)
        .omitted(false);

    // Position field
    card
        .createField("position")
        .name("Position")
        .type("Integer")
        .localized(false)
        .required(false)
        .validations([])
        .defaultValue({
            "en-US": 0,
        })
        .disabled(false)
        .omitted(false);

    // Change field controls
    card.changeFieldControl("title", "builtin", "singleLine", {});
    card.changeFieldControl("text", "builtin", "multiLine", {});
    card.changeFieldControl("image", "builtin", "assetLinkEditor", {});
    card.changeFieldControl("link", "builtin", "entryLinkEditor", {});
    card.changeFieldControl("types", "builtin", "tagEditor", {});
    card.changeFieldControl("position", "builtin", "numberEditor", {});
};
