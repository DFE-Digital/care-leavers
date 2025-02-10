# Contentful Migration
## Install the CLI
Install the Contentful CLI using

`yarn global add contentful-cli`

## Login to Contentful
login to contentful with either

`contentful login --management-token $MANAGEMENT_TOKEN`
or
`contentful login`

## Running Migrations
To run the migrations, navigate to the migrations folder
```
cd contentful/migration
```

Add `DELIVERY_API_KEY` , `PREVIEW_API_KEY` , `SPACE_ID` , and `MANAGEMENT_TOKEN`
to a `.env` file in the migration folder

run 
```
node --env-file=.env migrate.js <0004-grid.cjs>
```



run the new migrations with
```
contentful space migration --space-id <your-space-id> --environment-id <your-environment-id> --management-token <your-management-token> 0001-info-box.cjs
```
replace with current migration file name