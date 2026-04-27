using CareLeavers.Web.Models.Content;
using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Core.Search;

namespace CareLeavers.Web.Contentful.Webhooks.Helpers;

public record LinkedPageResult(HashSet<string> ScannedIds, List<Page> Pages);

public sealed class LinkedPageFinder(IContentfulClient contentfulClient, ILogger<LinkedPageFinder> logger)
{
    private readonly LinkedPageResult _result = new([], []);

    public async Task<LinkedPageResult> FindPagesLinkedToEntry(string entryId)
    {
        _result.ScannedIds.Add(entryId);
        await Search(entryId);
        return _result;
    }

    public async Task<LinkedPageResult> FindPagesLinkedToAsset(string assetId)
    {
        foreach (ContentfulContent entry in await GetLinkedEntities(assetId, true)) await Search(entry.Sys.Id);
        return _result;
    }

    private async Task Search(string entityId)
    {
        Stack<string> stack = new Stack<string>();
        stack.Push(entityId);

        while (stack.Count > 0)
        {
            ContentfulCollection<ContentfulContent> entries = await GetLinkedEntities(stack.Pop());

            foreach (ContentfulContent entry in entries)
            {
                if (!_result.ScannedIds.Add(entry.Sys.Id)) continue;

                if (entry is Page pageEntry) _result.Pages.Add(pageEntry);
                else stack.Push(entry.Sys.Id);
            }
        }

        logger.LogInformation("Scanned IDs: {Ids} | Pages: {Pages}", _result.ScannedIds.Count, _result.Pages.Count);
    }

    private async Task<ContentfulCollection<ContentfulContent>> GetLinkedEntities(string entityId, bool asset = false)
    {
        ContentfulCollection<ContentfulContent>? linkedToEntity = await contentfulClient.GetEntries(
            asset
                ? new QueryBuilder<ContentfulContent>().LinksToAsset(entityId).Include(0)
                : new QueryBuilder<ContentfulContent>().LinksToEntry(entityId).Include(0));

        return linkedToEntity ?? new ContentfulCollection<ContentfulContent> { Items = [] };
    }
}