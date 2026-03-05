module.exports = function (migration) {
    const configuration = migration.editContentType("configuration");
    
    configuration
        .createField("dfELogoAltText")
        .name("DfE Logo Alt Text")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([])
        .defaultValue({ "en-US": "Department for Education" })
        .disabled(false)
        .omitted(false);

    configuration.changeFieldControl("dfELogoAltText", "builtin", "singleLine", {});
};