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

## Testing Contentful Webhooks

Navigate to contentful within the browser. 

In the top right hand corner select the settings cog and navigate to 'Webhooks' - this provides a list of all existing webhooks for the solution.

If the webhook required is not present, select 'Add Webhook' and add a new webhook as appropriate to test the necessary endpoint. Remember here to allocate the filter to the appropriate contentful environment.