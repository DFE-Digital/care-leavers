const { sourceClient, targetClient } = require('../client')
const { CONFIGURATION } = require('../configuration')

const getMigrations = async (client) => {
    const migrationTrackerEntity = await client.entry.get({
        entryId: CONFIGURATION.migrationTrackerEntityId,
    })
    return migrationTrackerEntity['fields']['migrations']['en-US']
}

const contentMigrationsAreMismatched = async () => {
    const migrationsSource = await getMigrations(sourceClient)
    const migrationsTarget = await getMigrations(targetClient)

    if (migrationsSource.length !== migrationsTarget.length) {
        return true
    }

    for (let i = 0; i < migrationsSource.length; i++) {
        const nameSource = migrationsSource[i]['name']
        const nameTarget = migrationsTarget[i]['name']

        if (nameSource !== nameTarget) return true
    }

    return false
}

exports.contentMigrationsAreMismatched = contentMigrationsAreMismatched
