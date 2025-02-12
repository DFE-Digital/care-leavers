# Contentful Migration
cd into CareLeavers.ContentfulMigration
login to contentful with either

`contentful login --management-token $MANAGEMENT_TOKEN`
or
`contentful login`

to run a migration

To run the migrations, you need to copy the structure from appsettings.json 
into your .NET secrets and fill in the values.

Run the .NET console app to execute the migrations.