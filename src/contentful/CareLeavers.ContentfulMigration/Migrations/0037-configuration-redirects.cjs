module.exports = function (migration) {
    const configuration = migration
        .editContentType("configuration")

    configuration
        .createField("redirects")
        .name("Redirects")
        .type("Object")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);
};