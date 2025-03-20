using CareLeavers.Web.Caching;
using CareLeavers.Web.Models.Content;
using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Core.Search;
using Microsoft.Extensions.Caching.Distributed;

namespace CareLeavers.Web.Contentful.Webhooks;

public class PublishContentfulWebhook(
    IContentfulClient contentfulClient,
    IDistributedCache distributedCache,
    IContentfulManagementClient contentfulManagementClient,
    ILogger<PublishContentfulWebhook> logger)
{
    public async Task Consume(Entry<ContentfulContent> entry)
    {
        contentfulClient.ContentTypeResolver = new ContentfulEntityResolver();

        var idsScanned = new HashSet<string>
        {
            entry.SystemProperties.Id
        };
    
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
        
        if (entry.SystemProperties.ContentType.SystemProperties.Id == RedirectionRules.ContentType)
        {
            logger.LogInformation("Redirection rules entry updated, purging redirections cache");
            await distributedCache.RemoveAsync("content:redirections");
        }
        else if (entry.SystemProperties.ContentType.SystemProperties.Id == Page.ContentType)
        {
            var pageEntry = await contentfulClient.GetEntry<Page>(entry.SystemProperties.Id);
            
            if (pageEntry == null)
            {
                return;
            }
            
            logger.LogInformation("The following slug will be purged: {Slug}", pageEntry.Slug);

            await distributedCache.RemoveAsync($"content:{pageEntry.Slug}");
            
            if (distributedCache.TryGetValue($"content:{pageEntry.Slug}:languages", out List<string>? translations))
            {
                foreach (var translation in translations ?? [])
                {
                    await distributedCache.RemoveAsync($"content:{pageEntry.Slug}:language:{translation}");
                }
                
                await distributedCache.RemoveAsync($"content:{pageEntry.Slug}:languages");
            }
            
            var pageById = await distributedCache.GetAsync<Page>(pageEntry.Sys.Id);

            if (pageById != null && pageEntry.Slug != null && pageById.Slug != pageEntry.Slug)
            {
                logger.LogInformation("Slug changed from {OldSlug} to {NewSlug}", pageById.Slug, pageEntry.Slug);
                await distributedCache.RemoveAsync($"content:{pageById.Slug}");

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

                redirectionRules.Rules[pageById.Slug!] = pageEntry.Slug;
                redirectionRules.Rules.Remove(pageEntry.Slug);

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
            await distributedCache.RemoveAsync("content:configuration");
        }
        else
        {
            var pageEntries = await FindLinkedPages(entry.SystemProperties.Id, []);

            logger.LogInformation("The following slugs will be purged: {Slugs}", pageEntries.Select(x => x.Slug));

            foreach (var pageEntry in pageEntries)
            {
                await distributedCache.RemoveAsync($"content:{pageEntry.Slug}");
                
                if (distributedCache.TryGetValue($"content:{pageEntry.Slug}:languages", out List<string>? translations))
                {
                    foreach (var translation in translations ?? [])
                    {
                        await distributedCache.RemoveAsync($"content:{pageEntry.Slug}:language:{translation}");
                    }
                    
                    await distributedCache.RemoveAsync($"content:{pageEntry.Slug}:languages");
                }
            }
        }
        
        // Now wipe all direct IDs that have been cached
        foreach (var id in idsScanned)
        {
            logger.LogInformation("Removing content item directly from cache with Id: {Id}", id);
            await distributedCache.RemoveAsync(id);
        }
        
        await distributedCache.RemoveAsync("content:sitemap");
        await distributedCache.RemoveAsync("content:hierarchy");
    }
}