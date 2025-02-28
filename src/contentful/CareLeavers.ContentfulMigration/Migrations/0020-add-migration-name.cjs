module.exports = function (migration) {
    const migrationContent = migration
        .editContentType("migrationTracker")
        .displayField("name");

    migrationContent
        .createField("name")
        .name("Name")
        .type("Symbol")
        .localized(false)
        .required(true)
        .validations([])
        .disabled(false)
        .omitted(false)

    migrationContent
        .moveField('name').toTheTop();
    
};
