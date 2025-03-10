module.exports = function (migration) {
    const definitionBlock = migration
        .editContentType("definitionBlock");

    const definitionLink = migration
        .editContentType("definitionLink");
    
    const definitionContent = migration
        .editContentType("definitionContent");

    // Rename the field IDs
    definitionBlock
        .changeFieldId('definition', 'definitionContent')

    definitionLink
        .changeFieldId('definition', 'definitionBlock')

    definitionContent
        .changeFieldId('definition', 'content')
    
    // Now rename the fields
    definitionBlock
        .editField('definitionContent')
        .name('Definition Content')

    definitionLink
        .editField('definitionBlock')
        .name('Definition Block')

    definitionContent
        .editField('content')
        .name('Content')


};
