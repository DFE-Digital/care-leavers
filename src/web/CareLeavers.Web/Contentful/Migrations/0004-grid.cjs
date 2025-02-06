module.exports = function (migration) {
    const grid = migration
        .createContentType("grid")
        .name("Grid")
        .description("A grid layout containing multiple content blocks with a configurable title and type.")
        .displayField("title");
    
    grid
        .createField("title")
        .name("Title")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);
    
    grid
        .createField("gridType")
        .name("Grid Type")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([
            {
                in: ["Alternating Image and Text", "External Links", "Small Banner"],
            },
        ])
        .disabled(false)
        .omitted(false);

    
    grid
        .createField("showTitle")
        .name("Show Title")
        .type("Boolean")
        .localized(false)
        .required(true)
        .defaultValue({ "en-US": false })
        .disabled(false)
        .omitted(false);


    grid
        .createField("content")
        .name("Content")
        .type("Array")
        .localized(false)
        .required(false)
        .validations([])
        .items({
            type: "Link",
            linkType: "Entry",
            validations: [
                {
                    linkContentType: ["contentBlock"],
                },
            ],
        })
        .disabled(false)
        .omitted(false);


    grid
        .createField("cssClass")
        .name("CSS Class")
        .type("Symbol")
        .localized(false)
        .required(false)
        .defaultValue({ "en-US": "govuk-grid-column-full" })
        .validations([])
        .disabled(false)
        .omitted(false);


    grid.changeFieldControl("title", "builtin", "singleLine", {});
    grid.changeFieldControl("gridType", "builtin", "dropdown", {});
    grid.changeFieldControl("showTitle", "builtin", "boolean", {});
    grid.changeFieldControl("content", "builtin", "entryLinksEditor", {});
    grid.changeFieldControl("cssClass", "builtin", "singleLine", {});
};
