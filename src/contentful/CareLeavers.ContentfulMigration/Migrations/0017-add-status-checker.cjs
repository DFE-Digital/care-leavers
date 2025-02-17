module.exports = function (migration) {
    const checker = migration
        .createContentType("statusChecker")
        .name("Status Checker")
        .description("An initial status checker with answers that redirect to a page per answer")
        .displayField("title");

    checker.createField("title")
        .name("Title")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([])
        .disabled(false)
        .omitted(false);

    checker
        .createField("initialQuestion")
        .name("Initial Question")
        .type("Text")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);

    checker
        .createField("page")
        .name("Page")
        .type("Link")
        .localized(false)
        .required(true)
        .validations([
            {
                linkContentType: ["page"],
            },
        ])
        .linkType("Entry")
        .disabled(false)
        .omitted(false);

    checker
        .createField("answers")
        .name("Answers")
        .type("Array")
        .localized(false)
        .required(false)
        .validations([])
        .items({
            type: "Link",
            linkType: "Entry",
            validations: [
                {
                    linkContentType: ["answer"],
                },
            ],
        })
        .disabled(false)
        .omitted(false);


    checker.changeFieldControl("title", "builtin", "singleLine", { helpText: 'The name of this status checker (Not used for display purposes)' });
    checker.changeFieldControl("initialQuestion", "builtin", "singleLine", { helpText: 'The initial question to display' });
    checker.changeFieldControl("page", "builtin", "entryLinkEditor", { helpText: 'The page that this status checker sits within' });
    checker.changeFieldControl("answers", "builtin", "entryLinkEditor", { helpText: 'The answers to show within the checker' });
};
