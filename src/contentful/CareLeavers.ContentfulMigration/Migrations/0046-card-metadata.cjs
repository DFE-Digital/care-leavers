module.exports = function (migration) {
    const card = migration
        .editContentType("card")

    card
        .createField("metadata")
        .name("Metadata")
        .type("Text")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);
    
};
