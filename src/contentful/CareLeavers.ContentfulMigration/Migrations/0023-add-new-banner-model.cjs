module.exports = function (migration) {
    const banner = migration
        .createContentType("banner")
        .name("Banner")
        .description(
            "A call to action banner to be used across the site."
        )
        .displayField("title");


    banner
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

    banner
        .createField("text")
        .name("Text")
        .type("Text")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);

    banner
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

    banner
        .createField("linkText")
        .name("Link Text")
        .type("Symbol")
        .localized(false)
        .required(true)
        .disabled(false)
        .omitted(false);
    
    banner
        .createField("link")
        .name("Link")
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

    banner
        .createField("background")
        .name("Background Colour")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([
            {
                in: ["Blue"],
            },
        ])
        .disabled(false)
        .omitted(false);

    banner.changeFieldControl("text", "builtin", "multipleLine", {});
    banner.changeFieldControl("background", "builtin", "dropdown", {});

    const grid = migration
        .editContentType("grid");

    grid
        .editField("gridType")
        .validations([
            {
                in: ["Cards", "Alternating Image and Text", "External Links"],
            },
        ]);

};
