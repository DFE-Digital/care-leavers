module.exports = function (migration) {
    const configuration = migration
        .editContentType("configuration")

    configuration
        .createField("googleSiteVerification")
        .name("Google Site Verification")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);

    configuration
        .createField("bingSiteVerification")
        .name("Bing Site Verification")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([])
        .disabled(false)
        .omitted(false);

    configuration.changeFieldControl("googleSiteVerification", "builtin", "singleLine", {
        helpText: "The content from the Google Site Verification meta tag in Google Webmaster tools",
    });

    configuration.changeFieldControl("bingSiteVerification", "builtin", "singleLine", {
        helpText: "The content from the Bing Site Verification meta tag in Google Webmaster tools",
    });
 
};
