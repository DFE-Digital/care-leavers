module.exports = function (migration) {
    const configuration = migration
        .editContentType("configuration")

    configuration
        .createField("printableCollectionPage")
        .name("Printable Collection Page")
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

    configuration
        .createField("printableCollectionCallToAction")
        .name("Printable Collection Call To Action")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);

    configuration.changeFieldControl("printableCollectionCallToAction", "builtin", "singleLine", {
        helpText: "The text to use for the 'This page is in a printable collection' link",
    });
    configuration.changeFieldControl("printableCollectionPage", "builtin", "entryLinkEditor", {
        helpText: "The page that lists all of the printable collections",
    });
};
