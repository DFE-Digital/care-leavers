module.exports = function (migration) {
    const page = migration
        .editContentType("page");

    const editorLayout = page.editEditorLayout()
    editorLayout.moveField('header').toTheBottomOfFieldGroup('content')
    editorLayout.moveField('mainContent').toTheBottomOfFieldGroup('content')
    editorLayout.moveField('secondaryContent').toTheBottomOfFieldGroup('content')

};