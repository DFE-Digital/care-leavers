module.exports = function (migration) {
    const card = migration
        .editContentType("card");

    card
        .editField("title")
        .validations([
            {
                unique: false,
            },
        ]);
};
