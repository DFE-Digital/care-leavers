using CareLeavers.Web.Models.Content;
using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Core.Search;
using ZiggyCreatures.Caching.Fusion;

namespace CareLeavers.Web.Contentful.Webhooks;

public class PublishContentfulWebhook(
    IContentfulClient contentfulClient,
    IFusionCache fusionCache,
    IContentfulManagementClient contentfulManagementClient,
    ILogger<PublishContentfulWebhook> logger)
{
    private HashSet<string> _idsScanned = [];

    public async Task Consume(Entry<ContentfulContent> entry)
    {
        contentfulClient.ContentTypeResolver = new ContentfulEntityResolver();

        // Reset our scanned list
        _idsScanned.Clear();
        
        // Add this entry, since we've already got it
        _idsScanned.Add(entry.SystemProperties.Id);
        
        if (entry.SystemProperties.ContentType.SystemProperties.Id == RedirectionRules.ContentType)
        {
            logger.LogInformation("Redirection rules entry updated, purging redirections cache");
            try
            {
                await fusionCache.RemoveAsync("content:redirections");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unable to clear redirection rules");
            }
        }
        else if (entry.SystemProperties.ContentType.SystemProperties.Id == Page.ContentType)
        {
            var pageEntry = await contentfulClient.GetEntry<Page>(entry.SystemProperties.Id);
            
            if (pageEntry == null)
            {
                return;
            }
            
            logger.LogInformation("The following slug will be purged: {Slug}", pageEntry.Slug);

            try
            {
                await fusionCache.RemoveAsync($"content:{pageEntry.Slug}");
                if (pageEntry.Slug != null) await fusionCache.RemoveByTagAsync(pageEntry.Slug);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unable to purge page with slug {slug}", pageEntry.Slug);
            }
            
            var pageById = await fusionCache.TryGetAsync<Page>(pageEntry.Sys.Id);

            if (pageById.HasValue && pageEntry.Slug != null && pageById.Value.Slug != pageEntry.Slug)
            {
                logger.LogInformation("Slug changed from {OldSlug} to {NewSlug}", pageById.Value.Slug, pageEntry.Slug);
                await fusionCache.RemoveAsync($"content:{pageById.Value.Slug}");
                if (pageById.Value.Slug != null) await fusionCache.RemoveByTagAsync(pageById.Value.Slug);

                var redirectionContent = await contentfulClient.GetEntries(
                    new QueryBuilder<RedirectionRules>()
                        .ContentTypeIs(RedirectionRules.ContentType)
                        .Include(1));
                
                var redirectionRules = redirectionContent.Items.FirstOrDefault() ?? new RedirectionRules()
                {
                    Sys = new SystemProperties()
                    {
                        Id = "redirectionRulesId"
                    }
                };

                if (pageById.Value.Slug != null)
                {
                    redirectionRules.Rules[pageById.Value.Slug] = pageEntry.Slug;
                    redirectionRules.Rules.Remove(pageEntry.Slug);
                }

                var updatedEntryResp = await contentfulManagementClient.CreateOrUpdateEntry(new Entry<dynamic>
                {
                    SystemProperties = new SystemProperties()
                    {
                        Id = redirectionRules.Sys.Id,
                        Version = redirectionRules.Sys.Version
                    },
                    Fields = new
                    {
                        title = new Dictionary<string, dynamic> 
                        {
                            ["en-US"] = "Redirection Rules - DO NOT DELETE"
                        },
                        rules = new Dictionary<string, dynamic>
                        {
                            ["en-US"] = redirectionRules.Rules
                        }
                    }
                }, contentTypeId: RedirectionRules.ContentType, version: redirectionRules.Sys.PublishedVersion + 1);

                await contentfulManagementClient.PublishEntry(
                    updatedEntryResp.SystemProperties.Id,
                    updatedEntryResp.SystemProperties.Version ?? 1
                );
            }
        }
        else if (entry.SystemProperties.ContentType.SystemProperties.Id == ContentfulConfigurationEntity.ContentType)
        {
            logger.LogInformation("Configuration entry updated, purging configuration cache");
            try
            {
                await fusionCache.RemoveAsync("content:configuration");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unable to purge configuration");
            }
        }
        else
        {
            var pageEntries = new List<Page>();
            
            await FindLinkedPages(entry.SystemProperties.Id, pageEntries);

            logger.LogInformation("The following slugs will be purged: {Slugs}", pageEntries.Select(x => x.Slug));

            foreach (var pageEntry in pageEntries)
            {
                try
                {
                    await fusionCache.RemoveAsync($"content:{pageEntry.Slug}");
                    if (pageEntry.Slug != null) await fusionCache.RemoveByTagAsync(pageEntry.Slug);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Unable to purge page with slug {slug}", pageEntry.Slug);
                }
            }
        }
        
        // Now wipe all direct IDs that have been cached
        foreach (var id in _idsScanned)
        {
            logger.LogInformation("Removing content item directly from cache with Id: {Id}", id);
            try
            {
                await fusionCache.RemoveAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unable to purge entry with id {id}", id);
            }
        }

        try
        {
            await fusionCache.RemoveAsync("content:sitemap");
            await fusionCache.RemoveAsync("content:hierarchy");
        } 
        catch (Exception ex)
        {
            logger.LogError(ex, "Unable to clear sitemap and hierarchy");
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