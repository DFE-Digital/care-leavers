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
                    "pattern": "^(translate-this-page|cookies-policy|sitemap|statuschecker)$",
                    "flags": "ium"
                },
                message: 'You cannot use any of the following slugs: translate-this-page, cookies-policy, sitemap, statuschecker'
            }
        ]);
};
