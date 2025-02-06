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

run the new migrations with
```
contentful space migration --space-id <your-space-id> --environment-id <your-environment-id> --management-token <your-management-token> 0001-info-box.cjs
```
replace with current migration file name