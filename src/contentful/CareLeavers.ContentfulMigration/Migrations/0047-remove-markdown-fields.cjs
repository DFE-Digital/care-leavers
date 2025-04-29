module.exports = function (migration) {
    const card = migration
        .editContentType("card")

    card
        .resetFieldControl("text")
        .resetFieldControl("metadata")
        .changeFieldControl("text", "builtin", "multipleLine", {})
        .changeFieldControl("metadata", "builtin", "multipleLine", {helpText: "This field only shows when the grid is in 'Card' mode and shows below the text"})

    card.moveField('metadata').afterField('text')


    const externalAgency = migration
        .editContentType("externalAgency")

    externalAgency
        .resetFieldControl("description")
        .changeFieldControl("description", "builtin", "multipleLine", {})
    
    
};
