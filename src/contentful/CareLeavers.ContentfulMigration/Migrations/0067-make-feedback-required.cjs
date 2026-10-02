module.exports = function (migration) {
    const page = migration.editContentType("page")

    page
        .editField("showQuickfeedback")
        .required(true)
};
