const contentfulImport = require('contentful-import')
const contentfulExport = require('contentful-export')

const { configurationIsInvalid, CONFIGURATION } = require('./configuration')
const {
    isTargetProduction,
} = require('./helpers/is_target_environment_production')
const {
    contentMigrationsAreMismatched,
} = require('./helpers/content_migrations_are_mismatched')
const {
    isAnyEntityInChangedState,
} = require('./helpers/is_entity_in_changed_state')

async function synchronise() {
    console.log('Starting Synchronisation...')

    console.log('Checking Target Environment...')
    if (isTargetProduction()) {
        throw new Error(
            'Production is currently not allowed to be the target, exiting...'
        )
    }

    console.log('Checking Migrations Up To Date...')
    if (await contentMigrationsAreMismatched()) {
        throw new Error(
            'Content migrations between environments are mismatched, exiting...'
        )
    }

    console.log('Checking if any content is in the Changed state...')
    if (await isAnyEntityInChangedState()) {
        throw new Error(
            'Content has been found in the Changed state. All content must be in Archive, Draft or Published. Exiting...'
        )
    }

    console.log('Exporting Source Environment...')
    await contentfulExport({
        spaceId: CONFIGURATION.spaceId,
        managementToken: CONFIGURATION.managementToken,
        environmentId: CONFIGURATION.sourceEnvironment,
        contentFile: 'environment.json',
    })

    console.log('Copying Source Environment to Target Environment...')
    await contentfulImport({
        spaceId: CONFIGURATION.spaceId,
        managementToken: CONFIGURATION.managementToken,
        environmentId: CONFIGURATION.targetEnvironment,
        contentFile: 'environment.json',
    })
}

if (configurationIsInvalid()) {
    process.exit(1)
}

synchronise()
    .then((_) => {
        console.log('Synchronisation Completed.')
        process.exit(0)
    })
    .catch((err) => {
        console.error(err)
        process.exit(1)
    })
