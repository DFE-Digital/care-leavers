module.exports = function (migration) {
    const richContent = migration.editContentType("richContent");

    richContent.editField("background")
        .validations([
            {
                in: ["Blue"],
            },
        ]);

    const editorLayout = richContent.editEditorLayout();
    editorLayout.moveField("background").toTheBottomOfFieldGroup("layout");
};