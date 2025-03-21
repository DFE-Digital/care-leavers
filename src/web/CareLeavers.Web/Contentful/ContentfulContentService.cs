using CareLeavers.Web.Caching;
using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Models.ViewModels;
using Contentful.Core;
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
    
    public Task<RedirectionRules?> GetRedirectionRules(string fromSlug)
    {
        return _distributedCache.GetOrSetAsync("content:redirections", async () =>
        {
            var rules = new QueryBuilder<RedirectionRules>()
                .ContentTypeIs(RedirectionRules.ContentType)
                .Limit(1);

            var ruleEntries = await _contentfulClient.GetEntries(rules);

            return ruleEntries.FirstOrDefault();
        });
    }
    
    public async Task<Page?> GetPage(string slug)
    {
        var page = await _distributedCache.GetOrSetAsync($"content:{slug}", async () =>
        {
            var pages = new QueryBuilder<Page>()
                .ContentTypeIs(Page.ContentType)
                .FieldEquals(c => c.Slug, slug)
                .Limit(1);

            var pageEntries = await _contentfulClient.GetEntries(pages);

            return pageEntries.FirstOrDefault();
        });

        if (page != null)
        {
            await _distributedCache.GetOrSetAsync(page.Sys.Id, () => Task.FromResult(page));
        }

        return page;
    }

    public async Task<List<SimplePage>> GetBreadcrumbs(string slug, bool includeHome = true)
    {
        // Get homepage slug
        var home = (await GetConfiguration())?.HomePage;
        var homePage = new SimplePage()
        {
            Id = home.Sys.Id,
            Title = home.Title,
            Slug = home.Slug,
            Parent = null
        };
        
        // Get site hierarchy
        var hierarchy = await GetSiteHierarchy();
        
        // Keep backing up until the homepage
        List<SimplePage> breadcrumbs = [];

        var currentPage = hierarchy.Find(p => p.Slug == slug);;
        if (currentPage == null)
        {
            return breadcrumbs;
        }
        
        var parentPage = hierarchy.Find(p => p.Slug == currentPage.Parent);

        while (parentPage != null)
        {
            if (!breadcrumbs.Exists(b => b.Id == parentPage.Id))
            {
                if (includeHome || parentPage.Id != homePage.Id)
                    breadcrumbs.Add(parentPage);
            }

            parentPage = hierarchy.Find(p => p.Slug == parentPage.Parent);
        }

        if (((parentPage == null) || breadcrumbs.Count == 0) && includeHome && !breadcrumbs.Exists(b => b.Id == homePage.Id))
        {
            breadcrumbs.Add(homePage);
        }

        // Always set homepage title to home for breadcrumbs
        foreach (var simplePage in breadcrumbs.Where(simplePage => simplePage.Id == homePage.Id))
        {
            simplePage.Title = "Home";
        }
        
        breadcrumbs.Reverse();
        return breadcrumbs;
    }

    public Task<StatusChecker?> Hydrate(StatusChecker? statusChecker)
    {
        var id = statusChecker?.Sys.Id;

        if (id != null)
            return _distributedCache.GetOrSetAsync(id, async () =>
            {
                var query = new QueryBuilder<StatusChecker>()
                    .ContentTypeIs(StatusChecker.ContentType)
                    .FieldEquals("sys.id", id)
                    .Include(2)
                    .Limit(1);

                return (await _contentfulClient.GetEntries(query)).FirstOrDefault();
            });

        return Task.FromResult(statusChecker);
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
    
    public Task<Banner?> Hydrate(Banner? banner)
    {
        var id = banner?.Sys.Id;

        if (id != null)
            return _distributedCache.GetOrSetAsync(id, async () =>
            {
                var query = new QueryBuilder<Banner>()
                    .ContentTypeIs(Banner.ContentType)
                    .FieldEquals("sys.id", id)
                    .Include(2)
                    .Limit(1);

                return (await _contentfulClient.GetEntries(query)).FirstOrDefault();
            });

        return Task.FromResult(banner);
    }

    public async Task<string> GetSlug(string id)
    {
        var slugs = await GetSiteSlugs();
        return slugs[id];
    }

    public Task<RichContentBlock?> Hydrate(RichContentBlock? richContentBlock)
    {
        var id = richContentBlock?.Sys.Id;

        if (richContentBlock != null)
            return _distributedCache.GetOrSetAsync(richContentBlock.Sys.Id, async () =>
            {
                var query = new QueryBuilder<RichContentBlock>()
                    .ContentTypeIs(RichContentBlock.ContentType)
                    .FieldEquals("sys.id", id)
                    .Include(3)
                    .Limit(1);

                return (await _contentfulClient.GetEntries(query)).FirstOrDefault();
            });

        return Task.FromResult(richContentBlock);
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

    public Task<List<SimplePage>?> GetSiteHierarchy()
    {
        return _distributedCache.GetOrSetAsync("content:hierarchy", async () =>
        {
            var pages = new QueryBuilder<Page>()
                .Include(0)
                .ContentTypeIs(Page.ContentType);

            var pageEntries = await _contentfulClient.GetEntries(pages);
            var slugs = await GetSiteSlugs();
            
            return pageEntries
                .Where(x => x.Slug != null)
                .Select(p => new SimplePage()
                {
                    Id = p.Sys.Id,
                    Slug = slugs.FirstOrDefault(s => s.Key == p.Sys.Id).Value,
                    Title = p.Title,
                    Parent = p.Parent != null ? slugs.FirstOrDefault(s => s.Key == p.Parent.Sys.Id).Value : null
                })
                .ToList();
        });
    }
}