using CareLeavers.Web.Contentful.Webhooks.Helpers;
using CareLeavers.Web.Models.Content;
using Contentful.Core;
using Contentful.Core.Errors;
using Contentful.Core.Models;
using Contentful.Core.Search;
using ZiggyCreatures.Caching.Fusion;

namespace CareLeavers.Web.Contentful.Webhooks;

public sealed class PublishContentfulWebhook(
    IContentfulClient contentfulClient,
    IFusionCache fusionCache,
    IContentfulManagementClient contentfulManagementClient,
    LinkedPageFinder linkedPageFinder,
    ILogger<PublishContentfulWebhook> logger)
{
    private static readonly List<string> EntryTypesForRepublishingLinkedPages =
    [
        RichContent.ContentType,
        RichContentBlock.ContentType,
        PrintableCollection.ContentType
    ];

    public async Task Consume(Entry<ContentfulContent> entry, string? topic)
    {
        logger.LogInformation("PublishContentfulWebhook Started..");
        
        LinkedPageResult linkedPages = await linkedPageFinder.FindPagesLinkedToEntry(entry.SystemProperties.Id);

        switch (entry.SystemProperties.ContentType.SystemProperties.Id)
        {
            case RedirectionRules.ContentType:
                await HandleRedirectionRulesType();
                break;
            case ContentfulConfigurationEntity.ContentType:
                await HandleContentfulConfigurationType();
                break;
            case Page.ContentType:
                await HandlePageType(entry);
                break;
            case PrintableCollection.ContentType:
                await HandlePrintableCollectionType(entry);
                break;
            default:
                await HandleOtherType(entry.SystemProperties.Id, linkedPages);
                break;
        }

        foreach (string id in linkedPages.ScannedIds) await fusionCache.RemoveAsync(id);

        await fusionCache.RemoveAsync("content:sitemap");
        await fusionCache.RemoveAsync("content:hierarchy");

        await RepublishPagesLinkedToEntry(topic, entry.SystemProperties.ContentType.SystemProperties.Id,
            entry.SystemProperties.Id, linkedPages);

        logger.LogInformation("PublishContentfulWebhook Finished!");
    }

    private async Task HandleRedirectionRulesType()
    {
        logger.LogInformation("Redirection Rules Entry Updated - Purging Cache..");
        await fusionCache.RemoveAsync("content:redirections");
    }

    private async Task HandleContentfulConfigurationType()
    {
        logger.LogInformation("Configuration Entry Updated - Purging Cache..");
        await fusionCache.RemoveAsync("content:configuration");
    }

    private async Task HandlePrintableCollectionType(Entry<ContentfulContent> entry)
    {
        logger.LogInformation("Printable Collection Entry Updated - Purging Cache..");

        PrintableCollection collection =
            await contentfulClient.GetEntry<PrintableCollection>(entry.SystemProperties.Id);

        await fusionCache.RemoveAsync($"collection:{collection.Identifier}");
        await fusionCache.RemoveByTagAsync($"pc-{collection.Identifier}");
        await fusionCache.RemoveAsync(collection.Sys.Id);
    }

    private async Task HandlePageType(Entry<ContentfulContent> entry)
    {
        logger.LogInformation("Page Entry {Id} Updated - Purging Cache..", entry.SystemProperties.Id);

        Page page = await contentfulClient.GetEntry<Page>(entry.SystemProperties.Id);
        MaybeValue<Page> cachedOldPage = await fusionCache.TryGetAsync<Page>(page.Sys.Id);

        await fusionCache.RemoveAsync($"content:{page.Slug}");
        await fusionCache.RemoveByTagAsync(page.Slug!);

        if (!cachedOldPage.HasValue) return;
        if (cachedOldPage.Value.Slug!.Equals(page.Slug)) return;

        await HandlePageTypeSlugChange(cachedOldPage.Value.Slug, page.Slug!);
    }

    private async Task HandlePageTypeSlugChange(string staleSlugInCache, string updatedSlug)
    {
        logger.LogInformation("Slug Changed From {OldSlug} to {NewSlug} - Purging Cache & Updating Redirection Rules..",
            staleSlugInCache, updatedSlug);

        await fusionCache.RemoveAsync($"content:{staleSlugInCache}");
        await fusionCache.RemoveByTagAsync(staleSlugInCache);

        RedirectionRules? redirectionRules =
            (await contentfulClient.GetEntries(
                new QueryBuilder<RedirectionRules>().ContentTypeIs(RedirectionRules.ContentType))).FirstOrDefault();

        if (redirectionRules is not null)
        {
            redirectionRules.Rules[staleSlugInCache] = updatedSlug;
            redirectionRules.Rules.Remove(updatedSlug);
        }

        Entry<dynamic> redirectionRulesUpdated = await contentfulManagementClient.CreateOrUpdateEntry(
            UpdatedRedirectionRuleEntry(redirectionRules), contentTypeId: RedirectionRules.ContentType,
            version: redirectionRules?.Sys.PublishedVersion + 1 ?? 1);

        try
        {
            await contentfulManagementClient.PublishEntry(redirectionRulesUpdated.SystemProperties.Id,
                redirectionRulesUpdated.SystemProperties.Version ?? 1);
        }
        catch (ContentfulException e)
        {
            logger.LogError(e, "Error Publishing Redirection Rules");
        }
    }

    private async Task HandleOtherType(string entryId, LinkedPageResult linkedPages)
    {
        logger.LogInformation("Other Entry {Id} Updated - Purging Cache..", entryId);

        foreach (string slug in linkedPages.Pages.Select(p => p.Slug!))
        {
            await fusionCache.RemoveAsync($"content:{slug}");
            await fusionCache.RemoveByTagAsync(slug);
        }
    }

    private async Task RepublishPagesLinkedToEntry(string? topic, string contentType, string entryId,
        LinkedPageResult linkedPages)
    {
        if (topic is not "ContentManagement.Entry.publish") return;
        if (!EntryTypesForRepublishingLinkedPages.Contains(contentType)) return;

        logger.LogInformation("Republishing Pages Linked to Entry {Id}", entryId);

        foreach (Page page in linkedPages.Pages)
        {
            try
            {
                await contentfulManagementClient.PublishEntry(page.Sys.Id, page.Sys.PublishedVersion + 1 ?? 1);
            }
            catch (ContentfulException e)
            {
                logger.LogError(e, "Error Publishing Linked Page: {Slug}", page.Slug);
            }
        }
    }

    private static Entry<dynamic> UpdatedRedirectionRuleEntry(RedirectionRules? redirectionRules) => new()
    {
        SystemProperties = new SystemProperties
        {
            Id = redirectionRules?.Sys.Id ?? "redirectionRulesId",
            Version = redirectionRules?.Sys.Version
        },
        Fields = new
        {
            title = new Dictionary<string, dynamic>
            {
                ["en-US"] = redirectionRules?.Title ?? "Redirection Rules - DO NOT DELETE"
            },
            rules = new Dictionary<string, dynamic>
            {
                ["en-US"] = redirectionRules?.Rules ?? []
            }
        }
    };
}