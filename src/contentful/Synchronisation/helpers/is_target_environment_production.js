const { CONFIGURATION } = require('../configuration')

const isTargetProduction = () =>
    CONFIGURATION.targetEnvironment.toLowerCase() === 'production' ||
    CONFIGURATION.targetEnvironment.toLowerCase() === 'master'

exports.isTargetProduction = isTargetProduction
