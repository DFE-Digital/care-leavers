module.exports = function (migration) {
    const configuration = migration
        .editContentType("configuration")

    configuration
        .createField("translationEnabled")
        .name("Enable Translations")
        .type("Boolean")
        .localized(false)
        .required(true)
        .validations([])
        .disabled(false)
        .omitted(false);
};
