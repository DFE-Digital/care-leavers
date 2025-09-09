using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Models.ViewModels;
using Contentful.Core;
using Contentful.Core.Search;
using ZiggyCreatures.Caching.Fusion;

namespace CareLeavers.Web.Contentful;

public class ContentfulContentService : IContentService
{
    private readonly IContentfulClient _contentfulClient;
    private readonly IFusionCache _fusionCache;

    public ContentfulContentService(IContentfulClient contentfulClient, IFusionCache fusionCache)
    {
        _contentfulClient = contentfulClient;
        _fusionCache = fusionCache;
        _contentfulClient.ContentTypeResolver = new ContentfulEntityResolver();
    }
    
    public async Task<RedirectionRules?> GetRedirectionRules(string fromSlug)
    {
        return await _fusionCache.GetOrSetAsync("content:redirections", async token =>
        {
            var rules = new QueryBuilder<RedirectionRules>()
                .ContentTypeIs(RedirectionRules.ContentType)
                .Limit(1);

            var ruleEntries = await _contentfulClient.GetEntries(rules, token);

            return ruleEntries.FirstOrDefault();
        });
    }
    
    public async Task<Page?> GetPage(string slug)
    {
        var page = await _fusionCache.GetOrSetAsync($"content:{slug}", async token =>
        {
            var pages = new QueryBuilder<Page>()
                .ContentTypeIs(Page.ContentType)
                .FieldEquals(c => c.Slug, slug)
                .Include(2)
                .Limit(1);

            var pageEntries = await _contentfulClient.GetEntries(pages, token);
            var pageResult = pageEntries.FirstOrDefault();

            if (pageResult != null)
            {
                var latestUpdate = pageResult.Sys.UpdatedAt ?? DateTime.MinValue;

                var maxUpdatedAt = pageEntries.IncludedEntries
                    .Where(e => e.SystemProperties.UpdatedAt != null)
                    .Select(e => e.SystemProperties.UpdatedAt.Value)
                    .Where(entryUpdatedAt => entryUpdatedAt > latestUpdate)
                    .DefaultIfEmpty(latestUpdate)
                    .Max();

                if (maxUpdatedAt > latestUpdate)
                    pageResult.Sys.UpdatedAt = maxUpdatedAt;
            }

            return pageResult;
        });

        // If we get a page, but the slug doesn't match (used in tests), return null so we trigger our 404s
        if (page?.Slug != null && !page.Slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase))
        {
            page = null;
        }

        if (page != null)
        {
            await _fusionCache.SetAsync(page.Sys.Id, page);
        }

        return page;
    }
    
    public async Task<PrintableCollection?> GetPrintableCollection(string identifier)
    {
        var printableCollection = await _fusionCache.GetOrSetAsync($"collection:{identifier}", async token =>
        {
            var collection = new QueryBuilder<PrintableCollection>()
                .ContentTypeIs(PrintableCollection.ContentType)
                .FieldEquals(c => c.Identifier, identifier)
                .Include(1)
                .Limit(1);

            var entries = await _contentfulClient.GetEntries(collection, token);
            
            var result = entries.FirstOrDefault();
            if (result != null)
            {
                for (var i = 0; i < result.Content.Count; i++)
                {
                    var page = result.Content[i];
                    if (page.Slug == null) continue;

                    var hydratedPage = await GetPage(page.Slug);
                    if (hydratedPage != null) result.Content[i] = hydratedPage;
                }
            }

            return result;
        });

        // If we get a page, but the slug doesn't match (used in tests), return null so we trigger our 404s
        if (printableCollection?.Identifier != null && !(printableCollection.Identifier).Equals(identifier, StringComparison.InvariantCultureIgnoreCase))
        {
            printableCollection = null;
        }

        if (printableCollection != null)
        {
            // Get slugs from all of the pages
            var tags = printableCollection.Content.Where(p => p.Slug != null).Select(p => p.Slug!).ToList();
            await _fusionCache.SetAsync(printableCollection.Sys.Id, printableCollection, tags: tags);
            await _fusionCache.SetAsync($"collection:{identifier}", printableCollection, tags: tags);
        }

        return printableCollection;
    }
    
    public async Task<bool> IsPageInPrintableCollection(string slug)
    {
        var slugs = await GetSiteSlugs();
        var match = slugs.FirstOrDefault(s => s.Value == slug);
        var id = match.Key;
        return id != null && await IsInPrintableCollection(id);
    }

    public async Task<bool> IsInPrintableCollection(string id)
    {
        try
        {
            var slug = await GetSlug(id);
            var result = await _fusionCache.GetOrSetAsync($"pageIsPrintable:{slug}", async token =>
            {
                var collection = new QueryBuilder<PrintableCollection>()
                    .ContentTypeIs(PrintableCollection.ContentType)
                    .LinksToEntry(id)
                    .Include(0)
                    .Limit(1);

                var entries = await _contentfulClient.GetEntries(collection, token);

                return entries != null && entries.Any();
            }, tags: [slug]);
            
            return result;
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<SimplePage>> GetBreadcrumbs(string? slug, bool includeHome = true)
    {
        if (string.IsNullOrEmpty(slug))
        {
            return [];
        }
        
        // Get homepage slug
        var home = (await GetConfiguration())?.HomePage;
        var homePage = new SimplePage()
        {
            Id = home?.Sys.Id,
            Title = home?.Title,
            Slug = home?.Slug,
            Parent = null,
            ExcludeFromSitemap = home?.ExcludeFromSitemap ?? false
        };
        
        // Get site hierarchy
        var hierarchy = await GetSiteHierarchy();
        
        // Keep backing up until the homepage
        List<SimplePage> breadcrumbs = [];

        if (hierarchy != null)
        {
            var currentPage = hierarchy.Find(p => p.Slug == slug);
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
        }

        // Always set homepage title to home for breadcrumbs
        foreach (var simplePage in breadcrumbs.Where(simplePage => simplePage.Id == homePage.Id))
        {
            simplePage.Title = "Home";
        }
        
        breadcrumbs.Reverse();
        return breadcrumbs;
    }
    
    public async Task<string> GetSlug(string id)
    {
        var slugs = await GetSiteSlugs();
        return slugs[id];
    }

    public async Task<T> Hydrate<T>(T content)
    {
        string? id = null;
        string? contentType = null;
        var levels = 2;
        switch (content)
        {
            case Grid grid:
                id = grid?.Sys.Id;
                contentType = Grid.ContentType;
                break;
            case RichContentBlock block:
                id = block?.Sys.Id;
                contentType = RichContentBlock.ContentType;
                levels = 3;
                break;
            case Banner banner:
                id = banner?.Sys.Id;
                contentType = Banner.ContentType;
                break;
            case StatusChecker checker:
                id = checker?.Sys.Id;
                contentType = StatusChecker.ContentType;
                break;
            default:
                return content;
        }
        
        if (id != null)
            content = await _fusionCache.GetOrSetAsync(id, async token =>
            {
                var query = new QueryBuilder<T>()
                    .ContentTypeIs(contentType)
                    .FieldEquals("sys.id", id)
                    .Include(levels)
                    .Limit(1);

                return (await _contentfulClient.GetEntries(query, token)).FirstOrDefault();
            }) ?? content;

        return content;
    }

    public async Task<ContentfulConfigurationEntity?> GetConfiguration()
    {
        return await _fusionCache.GetOrSetAsync("content:configuration", async token =>
        {
            var config = new QueryBuilder<ContentfulConfigurationEntity>()
                .ContentTypeIs(ContentfulConfigurationEntity.ContentType)
                .Include(2)
                .Limit(1);

            var configEntries = await _contentfulClient.GetEntries(config, token);

            return configEntries.FirstOrDefault();
        });
    }

    public async Task<Dictionary<string, string>> GetSiteSlugs()
    {
        return await _fusionCache.GetOrSetAsync("content:sitemap", async token =>
        {
            var pages = new QueryBuilder<Page>()
                .Include(0)
                .ContentTypeIs(Page.ContentType);

            var pageEntries = await _contentfulClient.GetEntries(pages, token);

            return pageEntries
                .Where(x => x.Sys.Id != null && x.Slug != null)
                .Select(x => new KeyValuePair<string, string>(x.Sys.Id, x?.Slug ?? string.Empty))
                .ToDictionary();
        }) ?? [];
    }

    public async Task<List<SimplePage>?> GetSiteHierarchy()
    {
        return await _fusionCache.GetOrSetAsync("content:hierarchy", async token =>
        {
            var pages = new QueryBuilder<Page>()
                .Include(0)
                .ContentTypeIs(Page.ContentType);

            var pageEntries = await _contentfulClient.GetEntries(pages, token);
            var slugs = await GetSiteSlugs();
            
            return pageEntries
                .Where(x => x.Slug != null)
                .Select(p => new SimplePage()
                {
                    Id = p.Sys.Id,
                    Slug = slugs.FirstOrDefault(s => s.Key == p.Sys.Id).Value,
                    Title = p.Title,
                    Parent = p.Parent != null ? slugs.FirstOrDefault(s => s.Key == p.Parent.Sys.Id).Value : null,
                    ExcludeFromSitemap = p.ExcludeFromSitemap
                })
                .ToList();
        });
    }

    public async Task FlushCache()
    {
        await _fusionCache.ClearAsync(false);
    }
}