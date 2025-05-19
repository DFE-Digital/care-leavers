module.exports = function (migration) {
    const checker = migration
        .editContentType("statusChecker")

    checker.createField("validationError")
        .name("Validation Error")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([])
        .disabled(false)
        .omitted(false);

    checker.changeFieldControl("validationError", "builtin", "singleLine", { helpText: 'The error message to display if no answers are selected' });
};
