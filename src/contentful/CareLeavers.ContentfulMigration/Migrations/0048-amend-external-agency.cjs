module.exports = function (migration) {
    const externalAgency = migration
        .editContentType("externalAgency")

    externalAgency
        .createField("content")
        .name("Content")
        .type("RichText")
        .localized(false)
        .required(false)
        .validations([
            {
                enabledMarks: [
                    "bold",
                    "italic"
                ]
            },
            {
                enabledNodeTypes: [
                    "heading-4",
                    "ordered-list",
                    "unordered-list",
                    "hyperlink"
                ],
                message: "Only heading 4, ordered list, unordered list, and link to Url nodes are allowed"
            }
        ])
        .disabled(false)
        .omitted(false);
    
    externalAgency
        .editField("url")
        .disabled(false)
        .omitted(true)
    
    externalAgency
        .editField("description")
        .disabled(false)
        .omitted(true)

    externalAgency
        .editField("call")
        .disabled(false)
        .omitted(true)

    externalAgency
        .editField("openingTimes")
        .disabled(false)
        .omitted(true)

    externalAgency
        .editField("free")
        .disabled(false)
        .omitted(true)

    externalAgency
        .editField("accessibility")
        .disabled(false)
        .omitted(true)
    
    
    externalAgency
        .moveField('content').afterField('name')

    externalAgency
        .moveField('logo').afterField('content')

    externalAgency
        .moveField('url').afterField('logo')
    
    
};
