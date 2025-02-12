module.exports = function (migration) {
    const page = migration
        .editContentType("page");

    page
        .createField("showShareThis")
        .name("Show Share This on Page")
        .type("Boolean")
        .localized(false)
        .required(true)
        .defaultValue({ "en-US": true })
        .disabled(false)
        .omitted(false);
    
    page.changeFieldControl("showShareThis", "builtin", "boolean", {});

    // Move to after show last updated
    page.moveField('showShareThis').afterField('showLastUpdated')
};