module.exports = function (migration) {
    const definitionContent = migration
        .editContentType("definitionContent");

    definitionContent
        .editField("definition")
        .validations([
            {
                enabledMarks: [
                    "bold",
                    "italic"
                ]
            },
            {
                enabledNodeTypes: [
                    "heading-2",
                    "heading-3",
                    "heading-4",
                    "ordered-list",
                    "unordered-list",
                    "entry-hyperlink",
                    "hyperlink",
                    "embedded-entry-inline"
                ]
            }
        ]);
};
