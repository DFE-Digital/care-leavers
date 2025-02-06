# Contentful Migration
cd into CareLeavers.Web
login to contentful with either

`contentful login --management-token $MANAGEMENT_TOKEN`
or
`contentful login`

to run a migration

To run the migrations, extract the migration files
```
cd contentful/migrations
tar -zxf migrations.tar.gz
```

add
DELIVERY_API_KEY , PREVIEW_API_KEY , SPACE_ID , MANAGEMENT_TOKEN
to a .env file in the migrations folder
run 
```
node migrate.js <0004-grid.cjs>
```



run the new migrations with
```
contentful space migration --space-id <your-space-id> --environment-id <your-environment-id> --management-token <your-management-token> 0001-info-box.cjs
```
replace with current migration file name