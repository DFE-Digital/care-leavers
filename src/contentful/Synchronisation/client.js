const contentful = require('contentful-management')

const { CONFIGURATION } = require('./configuration')

const sourceClient = contentful.createClient(
    {
        accessToken: CONFIGURATION.managementToken,
    },
    {
        type: 'plain',
        defaults: {
            spaceId: CONFIGURATION.spaceId,
            environmentId: CONFIGURATION.sourceEnvironment,
        },
    }
)

const targetClient = contentful.createClient(
    {
        accessToken: CONFIGURATION.managementToken,
    },
    {
        type: 'plain',
        defaults: {
            spaceId: CONFIGURATION.spaceId,
            environmentId: CONFIGURATION.targetEnvironment,
        },
    }
)

exports.sourceClient = sourceClient
exports.targetClient = targetClient
