require('dotenv').config()

const CONFIGURATION = {
    spaceId: process.env.SPACE_ID,
    managementToken: process.env.MANAGEMENT_TOKEN,
    migrationTrackerEntityId: process.env.MIGRATION_TRACKER_ENTITY_ID,
    sourceEnvironment: process.env.SOURCE_ENVIRONMENT,
    targetEnvironment: process.env.TARGET_ENVIRONMENT,
}

const configurationIsInvalid = () => {
    let isInvalid = false

    Object.keys(CONFIGURATION).forEach((key) => {
        if (
            CONFIGURATION[key] === undefined ||
            CONFIGURATION[key] === null ||
            CONFIGURATION[key] === ''
        ) {
            console.error(`${key} is undefined, null or empty`)
            isInvalid = true
        }
    })

    return isInvalid
}

exports.CONFIGURATION = CONFIGURATION
exports.configurationIsInvalid = configurationIsInvalid
