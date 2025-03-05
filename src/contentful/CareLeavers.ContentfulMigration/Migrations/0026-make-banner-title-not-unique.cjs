module.exports = function (migration) {
    const banner = migration
        .editContentType("banner")
    
    // Remove validation
    banner.editField("title")
        .validations([])

    // Move background colour to after the title
    banner
        .moveField('background').afterField('title');
};
