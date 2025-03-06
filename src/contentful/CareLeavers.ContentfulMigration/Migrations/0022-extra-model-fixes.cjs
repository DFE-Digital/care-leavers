module.exports = function (migration) {
    const card = migration
        .editContentType("card");

    card
        .deleteField("position"); // Remove redundant field

    card
        .editField('types')
        .items({
            type: "Symbol",
            validations: [{ in: ["Guide", "Support"] }],
        })
    
    card
        .changeFieldControl("types", "builtin", "checkbox");

    const grid = migration
        .editContentType("grid");

    grid
        .deleteField("showTitle"); // Remove redundant field
    
    const page = migration
        .editContentType("page");
        
    page
        .changeFieldControl("slug", "builtin", "slugEditor", {
            helpText: "Unique URL for the site - may have sections in front of it, for example \"find-housing\" will allow access via /find-housing, but also possibly /guides/find-housing",
            trackingFieldId: "title"
    }); // Change to checklist
    
};
