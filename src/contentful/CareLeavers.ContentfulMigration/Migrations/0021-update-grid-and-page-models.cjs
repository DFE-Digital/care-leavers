module.exports = function (migration) {
    const grid = migration
        .editContentType("grid");

    grid
        .deleteField("cssClass"); // Remove redundant field
    
    const page = migration
        .editContentType("page");

    page
        .moveField("contentsHeadings").afterField("showContentsBlock") // Move to after show contents block
        .changeFieldControl("contentsHeadings", "builtin", "checkbox"); // Change to checklist
    
};
