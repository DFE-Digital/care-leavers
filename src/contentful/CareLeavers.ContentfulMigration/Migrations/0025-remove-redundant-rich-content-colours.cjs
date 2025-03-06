module.exports = function (migration) {
    const richContent = migration
        .editContentType("richContent");

    richContent
        .editField("background")
        .validations([
            {
                in: ["Blue"],
            },
        ]);
    

};
