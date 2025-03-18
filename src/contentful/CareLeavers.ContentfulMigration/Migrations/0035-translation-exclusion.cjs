module.exports = function (migration) {
    const configuration = migration
        .editContentType("configuration")

    configuration
        .createField("excludeFromTranslation")
        .name("Language codes excluded from Translation")
        .type("Array")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .items({
            type: "Symbol",
            validations: [],
        })
        .omitted(false);
};
