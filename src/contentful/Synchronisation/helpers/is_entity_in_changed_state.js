const { sourceClient, targetClient } = require('../client')

const PAGE_SIZE = 100

const isChanged = (entity) =>
    !!entity.sys.publishedVersion &&
    entity.sys.version >= entity.sys.publishedVersion + 2

const getEntityBatch = async (client, page) =>
    (
        await client.entry.getMany({
            query: {
                limit: PAGE_SIZE,
                skip: PAGE_SIZE * page,
            },
        })
    ).items

const batchCheckEntities = async (client) => {
    let batch = []
    let page = 1

    do {
        batch = await getEntityBatch(client, page++)
        for (let entity of batch) {
            if (isChanged(entity)) {
                return true
            }
        }
    } while (batch.length === PAGE_SIZE)

    return false
}

const isAnyEntityInChangedState = async () =>
    (await batchCheckEntities(sourceClient)) ||
    (await batchCheckEntities(targetClient))

exports.isAnyEntityInChangedState = isAnyEntityInChangedState
