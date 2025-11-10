module.exports = function (migration) {
    const getToAnAnswer = migration.createContentType('getToAnAnswer')
        .name('GetToAnAnswer')
        .displayField('title')
        .description('Embeddable iframe that renders a questionnaire runner');

    getToAnAnswer.createField('title').name('Title').type('Symbol').required(true);

    getToAnAnswer.createField('questionnaireSlug').name('Questionnaire Slug').type('Symbol').validations([
        { regexp: { pattern: '^([a-zA-Z0-9|-]).+', flags: null }, message: 'Must be a questionnaire slug' }
    ]);

    getToAnAnswer.changeEditorInterface('questionnaireSlug', 'singleLine');

    const page = migration
        .editContentType("page")

    // Allow primary and secondary content to also contain direct links to printable content
    page
        .editField("mainContent")
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
                    "embedded-entry-block",
                    "embedded-asset-block",
                    "entry-hyperlink",
                    "hyperlink",
                    "embedded-entry-inline",
                    "hr",
                    "table"
                ],
                message: "Only heading 2, heading 3, heading 4, ordered list, unordered list, block entry, asset, link to entry, link to Url, inline entry, table, and horizontal rule nodes are allowed"
            },
            {
                nodes: {
                    "embedded-entry-block": [
                        {
                            "linkContentType": [
                                "definitionContent",
                                "callToAction",
                                "grid",
                                "richContentBlock",
                                "banner",
                                "statusChecker",
                                "riddle",
                                "getToAnAnswer",
                                "spacer",
                                "button"
                            ],
                            "message": null
                        }
                    ],
                    "embedded-entry-inline": [
                        {
                            "linkContentType": [
                                "definitionLink"
                            ],
                            "message": null
                        }
                    ],
                    "entry-hyperlink": [
                        {
                            "linkContentType": [
                                "page",
                                "printableCollection"
                            ],
                            "message": null
                        }
                    ]
                }
            }
        ])

};