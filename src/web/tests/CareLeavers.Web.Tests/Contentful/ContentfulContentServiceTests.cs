using CareLeavers.Web.Contentful;
using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Models.ViewModels;
using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Core.Search;
using Microsoft.Extensions.Logging;
using NSubstitute;
using ZiggyCreatures.Caching.Fusion;

namespace CareLeavers.Web.Tests.Contentful;

public class ContentfulContentServiceTests
{
    private IContentfulClient _contentfulClient;
    private IFusionCache _fusionCache;
    private ILogger<ContentfulContentService> _logger;

    private ContentfulContentService _contentfulContentService;

    [SetUp]
    public void Init()
    {
        _contentfulClient = Substitute.For<IContentfulClient>();
        _fusionCache = Substitute.For<IFusionCache>();
        _logger = Substitute.For<ILogger<ContentfulContentService>>();

        _contentfulContentService = new ContentfulContentService(_contentfulClient, _fusionCache, _logger);
    }

    [Test]
    public async Task GetRedirectionRules_Returns_Rules()
    {
        RedirectionRules rules = new RedirectionRules
        {
            Title = "Rules",
            Rules = new Dictionary<string, string> { { "from", "to" } }
        };

        MockGetOrSetAsync(rules, "content:redirections");

        _contentfulClient.GetEntries(Arg.Any<QueryBuilder<RedirectionRules>>(), Arg.Any<CancellationToken>())
            .Returns(new ContentfulCollection<RedirectionRules> { Items = [rules] });

        RedirectionRules? result = await _contentfulContentService.GetRedirectionRules("from");

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Rules["from"], Is.EqualTo("to"));
    }

    [Test]
    public async Task GetPage_Returns_Page()
    {
        Page page = new Page
        {
            Sys = new SystemProperties { Id = "page-id" },
            Slug = "test-page",
            Title = "Test Page"
        };

        MockGetOrSetAsync(page, "content:test-page");

        _contentfulClient.GetEntries(Arg.Any<QueryBuilder<Page>>(), Arg.Any<CancellationToken>())
            .Returns(new ContentfulCollection<Page> { Items = [page] });

        Page? result = await _contentfulContentService.GetPage("test-page");

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Slug, Is.EqualTo("test-page"));
        await _fusionCache.Received(1).SetAsync("page-id", page);
    }

    [Test]
    public async Task GetPage_Returns_Null_On_Slug_Mismatch()
    {
        Page page = new Page
        {
            Sys = new SystemProperties { Id = "page-id" },
            Slug = "wrong-slug",
            Title = "Test Page"
        };

        MockGetOrSetAsync(page, "content:test-page");

        _contentfulClient.GetEntries(Arg.Any<QueryBuilder<Page>>(), Arg.Any<CancellationToken>())
            .Returns(new ContentfulCollection<Page> { Items = [page] });

        Page? result = await _contentfulContentService.GetPage("test-page");

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetPrintableCollection_Returns_Collection()
    {
        Page pageOne = new Page { Sys = new SystemProperties { Id = "page-one-id" }, Slug = "page-one" };
        Page pageTwo = new Page { Sys = new SystemProperties { Id = "page-two-id" }, Slug = "page-two" };

        PrintableCollection printableCollection = new PrintableCollection
        {
            Title = "Printable Collection",
            Identifier = "printable-collection",
            Sys = new SystemProperties { Id = "printable-collection-id" },
            Content = [pageOne, pageTwo]
        };
        
        MockGetOrSetAsync(printableCollection);
        MockGetOrSetAsync(pageOne);
        MockGetOrSetAsync(pageTwo);

        _contentfulClient.GetEntries(Arg.Any<QueryBuilder<PrintableCollection>>(), Arg.Any<CancellationToken>())
            .Returns(new ContentfulCollection<PrintableCollection> { Items = [printableCollection] });
        _contentfulClient.GetEntries(Arg.Is<QueryBuilder<Page>>(qB => qB.Build().Contains("fields.slug=page-one")),
                Arg.Any<CancellationToken>())
            .Returns(new ContentfulCollection<Page> { Items = [pageOne] });
        _contentfulClient.GetEntries(Arg.Is<QueryBuilder<Page>>(qB => qB.Build().Contains("fields.slug=page-two")),
                Arg.Any<CancellationToken>())
            .Returns(new ContentfulCollection<Page> { Items = [pageTwo] });

        PrintableCollection? result =
            await _contentfulContentService.GetPrintableCollection("printable-collection");

        Assert.That(result, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.Identifier, Is.EqualTo("printable-collection"));
            Assert.That(result.Content, Has.Count.EqualTo(2));
        }
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.Content[0], Is.SameAs(pageOne));
            Assert.That(result.Content[1], Is.SameAs(pageTwo));
        }
        await _fusionCache.Received(1).SetAsync("printable-collection-id", printableCollection, tags: Arg.Is<List<string>>(tag => tag.Contains("page-one") && tag.Contains("page-two")));
        await _fusionCache.Received(1).SetAsync("collection:printable-collection", printableCollection, tags: Arg.Is<List<string>>(tag => tag.Contains("page-one") && tag.Contains("page-two")));
    }

    [Test]
    public async Task IsInPrintableCollection_Returns_True_If_Found()
    {
        Page page = new Page { Sys = new SystemProperties { Id = "page-id" }, Slug = "test-page" };
        Dictionary<string, string> slugs = new Dictionary<string, string> { { "page-id", "test-page" } };
        
        MockGetOrSetAsync(slugs, "content:sitemap");
        MockGetOrSetAsync(true, "pageIsPrintable:test-page");

        _contentfulClient.GetEntries(Arg.Any<QueryBuilder<Page>>(), Arg.Any<CancellationToken>())
            .Returns(new ContentfulCollection<Page> { Items = [page] });
        _contentfulClient.GetEntries(Arg.Any<QueryBuilder<PrintableCollection>>(), Arg.Any<CancellationToken>())
            .Returns(new ContentfulCollection<PrintableCollection> { Items = [new PrintableCollection { Title = "Title", Identifier = "Identifier" }] });

        bool result = await _contentfulContentService.IsInPrintableCollection("page-id");

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task IsPageInPrintableCollection_Returns_True_If_Found()
    {
        Page page = new Page { Sys = new SystemProperties { Id = "page-id" }, Slug = "test-page" };
        Dictionary<string, string> slugs = new Dictionary<string, string> { { "page-id", "test-page" } };

        MockGetOrSetAsync(slugs, "content:sitemap");
        MockGetOrSetAsync(true, "pageIsPrintable:test-page");

        _contentfulClient.GetEntries(Arg.Any<QueryBuilder<Page>>(), Arg.Any<CancellationToken>())
            .Returns(new ContentfulCollection<Page> { Items = [page] });
        _contentfulClient.GetEntries(Arg.Any<QueryBuilder<PrintableCollection>>(), Arg.Any<CancellationToken>())
            .Returns(new ContentfulCollection<PrintableCollection> { Items = [new PrintableCollection { Title = "Title", Identifier = "Identifier" }] });

        bool result = await _contentfulContentService.IsPageInPrintableCollection("test-page");

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task GetSlug_Returns_Slug()
    {
        Dictionary<string, string> slugs = new Dictionary<string, string> { { "page-id", "test-page" } };
        MockGetOrSetAsync(slugs, "content:sitemap");

        _contentfulClient.GetEntries(Arg.Any<QueryBuilder<Page>>(), Arg.Any<CancellationToken>())
            .Returns(new ContentfulCollection<Page> { Items = [new Page { Sys = new SystemProperties { Id = "page-id" }, Slug = "test-page" }] });

        string result = await _contentfulContentService.GetSlug("page-id");

        Assert.That(result, Is.EqualTo("test-page"));
    }

    [Test]
    public async Task FlushCache_Calls_ClearAsync()
    {
        await _contentfulContentService.FlushCache();

        await _fusionCache.Received(1).ClearAsync(false);
    }

    [Test]
    public async Task GetConfiguration_Returns_Configuration()
    {
        ContentfulConfigurationEntity config = new ContentfulConfigurationEntity
        {
            Sys = new SystemProperties { Id = "config-id" }
        };
        MockGetOrSetAsync(config, "content:configuration");

        _contentfulClient.GetEntries(Arg.Any<QueryBuilder<ContentfulConfigurationEntity>>(), Arg.Any<CancellationToken>())
            .Returns(new ContentfulCollection<ContentfulConfigurationEntity> { Items = [config] });

        ContentfulConfigurationEntity? result = await _contentfulContentService.GetConfiguration();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.SameAs(config));
    }

    [Test]
    public async Task GetSiteSlugs_Returns_Slugs()
    {
        Page page = new Page { Sys = new SystemProperties { Id = "page-id" }, Slug = "test-page" };
        MockGetOrSetAsync(new Dictionary<string, string> { { "page-id", "test-page" } }, "content:sitemap");

        _contentfulClient.GetEntries(Arg.Any<QueryBuilder<Page>>(), Arg.Any<CancellationToken>())
            .Returns(new ContentfulCollection<Page> { Items = [page] });

        Dictionary<string, string> result = await _contentfulContentService.GetSiteSlugs();

        Assert.That(result["page-id"], Is.EqualTo("test-page"));
    }

    [Test]
    public async Task GetSiteHierarchy_Returns_Hierarchy()
    {
        Page parentPage = new Page { Sys = new SystemProperties { Id = "parent-id" }, Slug = "parent", Title = "Parent" };
        Page childPage = new Page { Sys = new SystemProperties { Id = "child-id" }, Slug = "child", Title = "Child", Parent = parentPage };
        
        Dictionary<string, string> slugs = new Dictionary<string, string> { { "parent-id", "parent" }, { "child-id", "child" } };
        MockGetOrSetAsync(slugs, "content:sitemap");
        
        List<SimplePage> hierarchy = 
        [
            new() { Id = "parent-id", Slug = "parent", Title = "Parent" },
            new() { Id = "child-id", Slug = "child", Title = "Child", Parent = "parent" }
        ];
        MockGetOrSetAsync(hierarchy, "content:hierarchy");

        _contentfulClient.GetEntries(Arg.Any<QueryBuilder<Page>>(), Arg.Any<CancellationToken>())
            .Returns(new ContentfulCollection<Page> { Items = [parentPage, childPage] });

        List<SimplePage>? result = await _contentfulContentService.GetSiteHierarchy();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result[1].Parent, Is.EqualTo("parent"));
    }

    [Test]
    public async Task GetBreadcrumbs_Returns_Breadcrumbs()
    {
        Page homePage = new Page { Sys = new SystemProperties { Id = "home-id" }, Slug = "home", Title = "Home" };
        ContentfulConfigurationEntity config = new ContentfulConfigurationEntity { HomePage = homePage };
        MockGetOrSetAsync(config, "content:configuration", true);

        List<SimplePage> hierarchy = 
        [
            new() { Id = "home-id", Slug = "home", Title = "Home" },
            new() { Id = "parent-id", Slug = "parent", Title = "Parent", Parent = "home" },
            new() { Id = "child-id", Slug = "child", Title = "Child", Parent = "parent" }
        ];
        MockGetOrSetAsync(hierarchy, "content:hierarchy", true);

        List<SimplePage> result = await _contentfulContentService.GetBreadcrumbs("child");

        Assert.That(result, Has.Count.EqualTo(2));
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result[0].Slug, Is.EqualTo("home"));
            Assert.That(result[1].Slug, Is.EqualTo("parent"));
        }
    }

    [Test]
    public async Task Hydrate_Returns_Hydrated_Grid()
    {
        Grid grid = new Grid { Sys = new SystemProperties { Id = "grid-id" } };
        Grid hydratedGrid = new Grid { Sys = new SystemProperties { Id = "grid-id" } };
        
        MockGetOrSetAsync(hydratedGrid, "grid-id");
        
        _contentfulClient.GetEntries(Arg.Any<QueryBuilder<Grid>>(), Arg.Any<CancellationToken>())
            .Returns(new ContentfulCollection<Grid> { Items = [hydratedGrid] });

        Grid result = await _contentfulContentService.Hydrate(grid);

        Assert.That(result, Is.SameAs(hydratedGrid));
    }

    [Test]
    public async Task GetBreadcrumbs_Returns_Empty_List_If_Slug_Null_Or_Empty()
    {
        List<SimplePage> resultOne = await _contentfulContentService.GetBreadcrumbs(null);
        List<SimplePage> resultTwo = await _contentfulContentService.GetBreadcrumbs("");

        using (Assert.EnterMultipleScope())
        {
            Assert.That(resultOne, Is.Empty);
            Assert.That(resultTwo, Is.Empty);
        }
    }

    [Test]
    public async Task GetBreadcrumbs_Returns_Empty_List_If_Hierarchy_Null()
    {
        MockGetOrSetAsync<List<SimplePage>>(null!, "content:hierarchy", true);

        List<SimplePage> result = await _contentfulContentService.GetBreadcrumbs("some-slug");

        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetBreadcrumbs_Returns_Only_Home_If_IncludeHome_True_And_Parent_Null()
    {
        Page homePage = new Page { Sys = new SystemProperties { Id = "home-id" }, Slug = "home", Title = "Home" };
        ContentfulConfigurationEntity config = new ContentfulConfigurationEntity { HomePage = homePage };
        MockGetOrSetAsync(config, "content:configuration", true);

        List<SimplePage> hierarchy = 
        [
            new() { Id = "home-id", Slug = "home", Title = "Home" },
            new() { Id = "page-id", Slug = "page", Title = "Page", Parent = null }
        ];
        MockGetOrSetAsync(hierarchy, "content:hierarchy", true);

        List<SimplePage> result = await _contentfulContentService.GetBreadcrumbs("page", true);

        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].Slug, Is.EqualTo("home"));
    }

    private void MockGetOrSetAsync<T>(T value, string? key = null, bool simulateCacheHit = false) =>
        _fusionCache.GetOrSetAsync(
                key ?? Arg.Any<string>(),
                Arg.Any<Func<FusionCacheFactoryExecutionContext<T>, CancellationToken, Task<T>>>(),
                Arg.Any<MaybeValue<T>>(),
                Arg.Any<FusionCacheEntryOptions>(),
                Arg.Any<IEnumerable<string>>(),
                Arg.Any<CancellationToken>())
            .Returns(info =>
            {
                if (key is not null && info.Arg<string>() != key) return new ValueTask<T>(value);

                var factory = info.Arg<Func<FusionCacheFactoryExecutionContext<T>, CancellationToken, Task<T>>>();
                return factory is not null && !simulateCacheHit
                    ? new ValueTask<T>(factory(null!, info.Arg<CancellationToken>()))
                    : new ValueTask<T>(value);
            });

    [TearDown]
    public void Teardown()
    {
        _fusionCache.Dispose();
    }
}