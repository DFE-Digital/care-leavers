module.exports = function (migration) {
    const card = migration
        .editContentType("card")
    
    card
        .createField("externalLink")
        .name("External Destination")
        .type("Symbol")
        .localized(false)
        .required(false)
        .validations([
            {
                regexp:{
                    pattern: "^(ftp|http|https):\\/\\/(\\w+:{0,1}\\w*@)?(\\S+)(:[0-9]+)?(\\/|\\/([\\w#!:.?+=&%@!\\-/]))?$",
                    flags: null
                }
            }
        ])
        .disabled(false)
        .omitted(false);

    card.changeFieldControl("externalLink", "builtin", "urlEditor", {});
};
