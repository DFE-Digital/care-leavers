module.exports = function (migration) {
    const page = migration
        .editContentType("page");

    // an editor layout can be created empty but not saved without adding at least one tab
    const editorLayout = page.createEditorLayout()

    // Primary content
    editorLayout.createFieldGroup('content', {
        name: 'Content'
    })
   
    
    // Now add the field sets
    editorLayout.editFieldGroup('content').createFieldGroup('pageInfo').name('Page Info')
    editorLayout.editFieldGroup('content').createFieldGroup('seo').name('SEO')
    editorLayout.editFieldGroup('content').createFieldGroup('layout').name('Layout')

    // Set descriptions and collapse options
    editorLayout.changeFieldGroupControl('seo', 'builtin', 'fieldset', {
        helpText: 'SEO and Open Graph Options',
        collapsedByDefault: true
    })
    editorLayout.changeFieldGroupControl('layout', 'builtin', 'fieldset', {
        helpText: 'Layout Options'
    })
    
    // Now move fields around
    editorLayout.moveField('title').toTheTopOfFieldGroup('pageInfo')
    editorLayout.moveField('slug').afterField('title')
    editorLayout.moveField('parent').afterField('slug')
    editorLayout.moveField('seoTitle').toTheTopOfFieldGroup('seo')
    editorLayout.moveField('seoDescription').afterField('seoTitle')
    editorLayout.moveField('seoImage').afterField('seoDescription')
    editorLayout.moveField('width').toTheTopOfFieldGroup('layout')
    editorLayout.moveField('type').afterField('width')
    editorLayout.moveField('showContentsBlock').afterField('type')
    editorLayout.moveField('contentsHeadings').afterField('showContentsBlock')
    editorLayout.moveField('showLastUpdated').afterField('contentsHeadings')
    editorLayout.moveField('showFooter').afterField('showLastUpdated')
    editorLayout.moveField('showShareThis').afterField('showFooter')

    editorLayout.moveField('header').toTheBottomOfFieldGroup()
    editorLayout.moveField('mainContent').toTheBottomOfFieldGroup()
    editorLayout.moveField('secondaryContent').toTheBottomOfFieldGroup()

};