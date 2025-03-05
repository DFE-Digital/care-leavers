using CareLeavers.Web.Caching;
using CareLeavers.Web.Models.Content;
using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Core.Search;
using Microsoft.Extensions.Caching.Distributed;
using Serilog;

namespace CareLeavers.Web.Contentful.Webhooks;

public class PublishContentfulWebhook(
    IContentfulClient contentfulClient,
    IDistributedCache distributedCache,
    ILogger<PublishContentfulWebhook> logger)
{
    public async Task Consume(Entry<ContentfulContent> entry)
    {
        contentfulClient.ContentTypeResolver = new ContentfulEntityResolver();
            
        var idsScanned = new HashSet<string>();
    
        async Task<List<Page>> FindLinkedPages(string id, List<Page> linkedPages)
        {
            var entries = await contentfulClient.GetEntries(new QueryBuilder<ContentfulContent>()
                .LinksToEntry(id));

            foreach (var linkedEntry in entries)
            {
                if (!idsScanned.Add(linkedEntry.Sys.Id))
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
        
            return linkedPages.ToList();
        }
    
        if (entry.SystemProperties.ContentType.SystemProperties.Id == Page.ContentType)
        {
            var pageEntry = await contentfulClient.GetEntry<Page>(entry.SystemProperties.Id);

            var oldSlug = await distributedCache.GetAsync<string>($"content:id:{pageEntry.Sys.Id}");
            
            if (oldSlug != null && oldSlug != pageEntry.Slug)
            {
                logger.LogInformation("Slug changed from {OldSlug} to {NewSlug}", oldSlug, pageEntry.Slug);
                await distributedCache.RemoveAsync($"content:{oldSlug}");
                await distributedCache.SetAsync($"content:id:{pageEntry.Sys.Id}", pageEntry.Slug);
            }
            else
            {
                logger.LogInformation("The following slug will be purged: {Slug}", pageEntry.Slug);

                await distributedCache.RemoveAsync($"content:{pageEntry.Slug}");
                await distributedCache.RemoveAsync($"content:id:{pageEntry.Sys.Id}");
            }
        }
        else if (entry.SystemProperties.ContentType.SystemProperties.Id == ContentfulConfigurationEntity.ContentType)
        {
            Log.Logger.Information("Configuration entry updated, purging configuration cache");
            await distributedCache.RemoveAsync("content:configuration");
        }
        else
        {
            var pageEntries = await FindLinkedPages(entry.SystemProperties.Id, []);

            Log.Logger.Information("The following slugs will be purged: {Slugs}", pageEntries.Select(x => x.Slug));

            foreach (var pageEntry in pageEntries)
            {
                await distributedCache.RemoveAsync($"content:{pageEntry.Slug}");
            }
        }

        await distributedCache.RemoveAsync("content:sitemap");
    }
}