using CareLeavers.Web.Caching;
using CareLeavers.Web.Models.Content;
using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Core.Search;
using Microsoft.Extensions.Caching.Distributed;
using ZiggyCreatures.Caching.Fusion;

namespace CareLeavers.Web.Contentful.Webhooks;

public class PublishAssetWebhook(
    IContentfulClient contentfulClient,
    IFusionCache fusionCache,
    ILogger<PublishAssetWebhook> logger)
{
    private HashSet<string> _idsScanned = [];
    
    public async Task Consume(Asset asset)
    {
        contentfulClient.ContentTypeResolver = new ContentfulEntityResolver();

        // Get the IDs of the things linked to the asset
        var linkedToAsset = await contentfulClient.GetEntries(new QueryBuilder<ContentfulContent>()
            .LinksToAsset(asset.SystemProperties.Id)
            .Include(0)
        );
        
        // Clear our scanned IDs
        _idsScanned.Clear();
        var pageEntries = new List<Page>();
        
        // Loop through our entries linking to the asset and go deep
        foreach (var entry in linkedToAsset.Items)
        {
            await FindLinkedPages(entry.Sys.Id, pageEntries);
        }
        
        logger.LogInformation("The following slugs will be purged: {Slugs}", pageEntries.Select(x => x.Slug));

        foreach (var pageEntry in pageEntries)
        {
            await fusionCache.RemoveAsync($"content:{pageEntry.Slug}");
            if (pageEntry.Slug != null) await fusionCache.RemoveByTagAsync(pageEntry.Slug);
        }
        
        // Now wipe all direct IDs that have been cached
        foreach (var id in _idsScanned)
        {
            logger.LogInformation("Removing content item directly from cache with Id: {Id}", id);
            await fusionCache.RemoveAsync(id);
        }
    }
    
    private async Task FindLinkedPages(string id, List<Page> linkedPages)
    {
        var entries = await contentfulClient.GetEntries(new QueryBuilder<ContentfulContent>()
            .LinksToEntry(id)
            .Include(0)
        );

        foreach (var linkedEntry in entries)
        {
            if (!_idsScanned.Add(linkedEntry.Sys.Id))
            {
                continue;
            }
            
            if (linkedEntry is Page pageEntry)
            {
                linkedPages.Add(pageEntry);
            }
            else
            {
                await FindLinkedPages(linkedEntry.Sys.Id, linkedPages);
            }
        }
    }

}