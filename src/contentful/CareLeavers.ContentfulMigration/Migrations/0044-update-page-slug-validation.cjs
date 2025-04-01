module.exports = function (migration) {
    const page = migration
        .editContentType("page");

    page
        .editField("slug")
        .validations([
            {
                unique: true,
                message: 'The slug for the site must be unique'
            },
            {
                "prohibitRegexp": {
                    "pattern": "^(translate-this-page|cookie-policy|cookies-policy|privacy-policies|accessibility-statement|service-unavailable|error|sitemap|page-not-found|statuschecker)$", 
                    "flags": "ium"
                },
                message: 'You cannot use any of the following slugs: translate-this-page, cookie-policy, cookies-policy, privacy-policies, accessibility-statement, service-unavailable, error, sitemap, page-not-found, statuschecker'
            }
        ]);

    page
        .changeFieldControl("slug", "builtin", "slugEditor", {
            helpText: "Unique URL for the site - this should normally follow the page title - for example \"Find Housing\" should have a slug of \"find-housing\"",
            trackingFieldId: "title"
        });
};