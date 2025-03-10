using CareLeavers.Web.Caching;
using CareLeavers.Web.Models.Content;
using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Core.Search;
using Microsoft.Extensions.Caching.Distributed;

namespace CareLeavers.Web.Contentful;

public class ContentfulContentService : IContentService
{
    private readonly IDistributedCache _distributedCache;
    private readonly IContentfulClient _contentfulClient;

    public ContentfulContentService(IDistributedCache distributedCache, IContentfulClient contentfulClient)
    {
        _distributedCache = distributedCache;
        _contentfulClient = contentfulClient;
        _contentfulClient.ContentTypeResolver = new ContentfulEntityResolver();
    }
    
    public Task<Page?> GetPage(string slug)
    {
        return _distributedCache.GetOrSetAsync($"content:{slug}", async () =>
        {
            var pages = new QueryBuilder<Page>()
                .ContentTypeIs(Page.ContentType)
                .FieldEquals(c => c.Slug, slug)
                .Limit(1);

            var pageEntries = await _contentfulClient.GetEntries(pages);

            return pageEntries.FirstOrDefault();
        });
    }

    public Task<StatusChecker?> GetStatusChecker(string id)
    {
        return _distributedCache.GetOrSetAsync($"statuschecker:{id}", async () =>
        {
            var query = new QueryBuilder<StatusChecker>()
                .ContentTypeIs(StatusChecker.ContentType);
            
            return await _contentfulClient.GetEntry(id, query);
        });
    }

    public Task<RichContent?> Hydrate(RichContent? richContent)
    {
        var id = richContent?.Sys.Id;

        if (id != null)
            return _distributedCache.GetOrSetAsync(id, async () =>
            {
                var query = new QueryBuilder<RichContent>()
                    .ContentTypeIs(RichContent.ContentType)
                    .FieldEquals("sys.id", id)
                    .Include(2)
                    .Limit(1);

                return (await _contentfulClient.GetEntries(query)).FirstOrDefault();
            });

        return Task.FromResult(richContent);
    }
    
    public Task<Grid?> Hydrate(Grid? grid)
    {
        var id = grid?.Sys.Id;

        if (id != null)
            return _distributedCache.GetOrSetAsync(id, async () =>
            {
                var query = new QueryBuilder<Grid>()
                    .ContentTypeIs(Grid.ContentType)
                    .FieldEquals("sys.id", id)
                    .Include(2)
                    .Limit(1);

                return (await _contentfulClient.GetEntries(query)).FirstOrDefault();
            });

        return Task.FromResult(grid);
    }
    
    public Task<RichContentBlock?> Hydrate(RichContentBlock? richContent)
    {
        var id = richContent?.Sys.Id;

        if (richContent != null)
            return _distributedCache.GetOrSetAsync(richContent.Sys.Id, async () =>
            {
                var query = new QueryBuilder<RichContentBlock>()
                    .ContentTypeIs(RichContentBlock.ContentType)
                    .FieldEquals("sys.id", id)
                    .Include(2)
                    .Limit(1);

                return (await _contentfulClient.GetEntries(query)).FirstOrDefault();
            });

        return Task.FromResult(richContent);
    }

    public Task<ContentfulConfigurationEntity?> GetConfiguration()
    {
        return _distributedCache.GetOrSetAsync("content:configuration", async () =>
        {
            var config = new QueryBuilder<ContentfulConfigurationEntity>()
                .ContentTypeIs(ContentfulConfigurationEntity.ContentType)
                .Include(2)
                .Limit(1);

            var configEntries = await _contentfulClient.GetEntries(config);

            return configEntries.FirstOrDefault();
        });
    }

    public async Task<Dictionary<string, string>> GetSiteSlugs()
    {
        return await _distributedCache.GetOrSetAsync("content:sitemap", async () =>
        {
            var pages = new QueryBuilder<Page>()
                .Include(0)
                .ContentTypeIs(Page.ContentType);

            var pageEntries = await _contentfulClient.GetEntries(pages);

            return pageEntries
                .Where(x => x.Sys.Id != null && x.Slug != null)
                .Select(x => new KeyValuePair<string,string>(x.Sys.Id, x.Slug))
                .ToDictionary();
        }) ?? [];
    }
    
    public Task<Dictionary<string, string>> GetSiteHierarchy()
    {
        return _distributedCache.GetOrSetAsync("content:hierarchy", async () =>
        {
            var pages = new QueryBuilder<Page>()
                .Include(0)
                .ContentTypeIs(Page.ContentType);

            var pageEntries = await _contentfulClient.GetEntries(pages);
            var slugs = await GetSiteSlugs();
            
            return pageEntries
                .Where(x => x.Parent != null && x.Slug != null)
                .Select(p => new KeyValuePair<string, string>(slugs[p.Sys.Id], slugs[p.Parent.Sys.Id]))
                .ToDictionary();
        });
    }
}