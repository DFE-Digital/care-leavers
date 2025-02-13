module.exports = function (migration) {
    const answer = migration
        .createContentType("answer")
        .name("Answer")
        .description("An answer with description, target, and priority - for use within the status checker")
        .displayField("answer");

    answer.createField("answer")
        .name("Answer")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([])
        .disabled(false)
        .omitted(false);

    answer
        .createField("description")
        .name("Description")
        .type("Text")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);

    answer
        .createField("target")
        .name("Target")
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

    answer
        .createField("priority")
        .name("Priority")
        .type("Integer")
        .localized(false)
        .required(true)
        .validations([
            {
                "unique": true
            },
            {
                "range": {
                    "min": 0,
                    "max": 99
                }
            }
        ])
        .disabled(false)
        .omitted(false);

    answer.changeFieldControl("answer", "builtin", "singleLine", {});
    answer.changeFieldControl("description", "builtin", "singleLine", { helpText: 'Optional description to describe the answer, if applicable' });
    answer.changeFieldControl("target", "builtin", "entryLinkEditor", { helpText: 'The page that this answer should redirect to' });
    answer.changeFieldControl("priority", "builtin", "numberEditor", { helpText: 'Priority (Higher priority will ensure this answer comes first if multiple are checked)' });
};
