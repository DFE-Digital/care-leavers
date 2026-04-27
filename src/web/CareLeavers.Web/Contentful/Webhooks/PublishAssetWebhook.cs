using CareLeavers.Web.Contentful.Webhooks.Helpers;
using Contentful.Core.Models;
using ZiggyCreatures.Caching.Fusion;

namespace CareLeavers.Web.Contentful.Webhooks;

public sealed class PublishAssetWebhook(
    IFusionCache fusionCache,
    LinkedPageFinder linkedPageFinder,
    ILogger<PublishAssetWebhook> logger)
{
    public async Task Consume(Asset asset)
    {
        logger.LogInformation("PublishAssetWebhook Started..");

        LinkedPageResult result = await linkedPageFinder.FindPagesLinkedToAsset(asset.SystemProperties.Id);

        foreach (string slug in result.Pages.Select(p => p.Slug!))
        {
            await fusionCache.RemoveAsync($"content:{slug}");
            await fusionCache.RemoveByTagAsync(slug);
        }

        foreach (string id in result.ScannedIds) await fusionCache.RemoveAsync(id);
        
        logger.LogInformation("PublishAssetWebhook Finished!");
    }
}