module.exports = function (migration) {
    const navigationElement = migration
        .createContentType("navigationElement")
        .name("Navigation Element")
        .description("A navigation element that links to a page.")
        .displayField("title");

    navigationElement
        .createField("title")
        .name("Title")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([
            {
                size: {
                    "min": null,
                    "max": 25
                }
            }
        ])
        .disabled(false)
        .omitted(false);

    navigationElement
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


    navigationElement.changeFieldControl("title", "builtin", "singleLine", {});
    navigationElement.changeFieldControl("link", "builtin", "entryLinkEditor", {});
};