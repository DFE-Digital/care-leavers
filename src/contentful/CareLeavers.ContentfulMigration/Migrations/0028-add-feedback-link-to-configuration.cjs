module.exports = function (migration) {
    const configuration = migration
        .editContentType("configuration")

    configuration
        .createField("feedbackText")
        .name("Feedback Text")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);

    // Feedback URL
    configuration
        .createField("feedbackUrl")
        .name("Feedback URL")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([
            {
                regexp: {
                    pattern: "^(http:\\/\\/|https:\\/\\/|mailto:|tel:)(\\w+:{0,1}\\w*@)?(\\S+)(:[0-9]+)?(\\/|\\/([\\w#!:.?+=&%@!\\-\\/]))?$",
                    flags: null
                }
            }
        ])
        .disabled(false)
        .omitted(false)

    configuration.changeFieldControl("feedbackText", "builtin", "singleLine", {
        helpText: "The text to use for the feedback link",
    });
    configuration.changeFieldControl("feedbackUrl", "builtin", "urlEditor", {
        helpText: "URL for the feedback - can be either http, https, email, or telephone number - remember to use %20 for spaces",
    });

};
