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

    public ContentfulContentService(
        IDistributedCache distributedCache, 
        IContentfulClient contentfulClient)
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
                .Include(4)
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
                .ContentTypeIs(StatusChecker.ContentType)
                .Include(4)
                .Limit(1);
            
            return await _contentfulClient.GetEntry(id, query);
        });
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

    public async Task<List<string>> GetSiteSlugs()
    {
        return await _distributedCache.GetOrSetAsync("content:sitemap", async () =>
        {
            var pages = new QueryBuilder<Page>()
                .ContentTypeIs(Page.ContentType)
                .SelectFields(x => new { x.Slug });

            var pageEntries = await _contentfulClient.GetEntries(pages);

            return pageEntries.Select(x => x.Slug ?? string.Empty).ToList();
        }) ?? [];
    }
}