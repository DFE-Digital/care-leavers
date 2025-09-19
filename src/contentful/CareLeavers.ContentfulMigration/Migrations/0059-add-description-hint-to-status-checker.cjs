module.exports = function (migration) {
    const checker = migration
        .editContentType("statusChecker")

    checker.createField("description")
        .name("Initial Question Description")
        .type("Text")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);

    checker.changeFieldControl("description", "builtin", "singleLine", { helpText: 'Optional description to describe the initial question, if applicable' });
};
