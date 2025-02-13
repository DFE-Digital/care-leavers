module.exports = function (migration) {
    const riddle = migration
        .createContentType("riddle")
        .name("Riddle")
        .description("A container to render a Riddle quiz/test")
        .displayField("title");

    riddle.createField("Title")
        .name("Title")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([])
        .disabled(false)
        .omitted(false);

    riddle
        .createField("riddleId")
        .name("Riddle ID")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([])
        .disabled(false)
        .omitted(false);

    riddle.changeFieldControl("title", "builtin", "singleLine", { helpText: 'The name of this Riddle block (not used in display)' });
    riddle.changeFieldControl("riddleId", "builtin", "singleLine", { helpText: 'The ID of the Riddle to render' });
};
