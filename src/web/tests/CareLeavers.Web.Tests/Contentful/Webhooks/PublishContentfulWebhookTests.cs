using CareLeavers.Web.Contentful.Webhooks;
using CareLeavers.Web.Contentful.Webhooks.Helpers;
using CareLeavers.Web.Models.Content;
using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Core.Search;
using Microsoft.Extensions.Logging;
using NSubstitute;
using ZiggyCreatures.Caching.Fusion;

namespace CareLeavers.Web.Tests.Contentful.Webhooks;

public class PublishContentfulWebhookTests
{
    private IContentfulClient _contentfulClient;
    private IFusionCache _fusionCache;
    private IContentfulManagementClient _contentfulManagementClient;

    private PublishContentfulWebhook _publishContentfulWebhook;

    [SetUp]
    public void Init()
    {
        _contentfulClient = Substitute.For<IContentfulClient>();
        _fusionCache = Substitute.For<IFusionCache>();
        _contentfulManagementClient = Substitute.For<IContentfulManagementClient>();

        _publishContentfulWebhook = new PublishContentfulWebhook(_contentfulClient, _fusionCache,
            _contentfulManagementClient,
            new LinkedPageFinder(_contentfulClient, Substitute.For<ILogger<LinkedPageFinder>>()),
            Substitute.For<ILogger<PublishContentfulWebhook>>());
    }

    [Test]
    public async Task Consume_WhenContentTypeIs_RedirectionRules_Clears_RedirectionsCache()
    {
        const string entryId = "EntryId";
        Entry<ContentfulContent> entry = CreateEntry(RedirectionRules.ContentType, entryId);

        await _publishContentfulWebhook.Consume(entry, "Topic");

        await _fusionCache.Received(1).RemoveAsync("content:redirections");
        await AssertDefaultCacheClears(entryId);
    }

    [Test]
    public async Task Consume_WhenContentTypeIs_ContentfulConfigurationEntity_Clears_ConfigurationCache()
    {
        const string entryId = "EntryId";
        Entry<ContentfulContent> entry = CreateEntry(ContentfulConfigurationEntity.ContentType, entryId);

        await _publishContentfulWebhook.Consume(entry, "Topic");

        await _fusionCache.Received(1).RemoveAsync("content:configuration");
        await AssertDefaultCacheClears(entryId);
    }

    [Test]
    public async Task Consume_WhenContentTypeIs_PrintableCollection_Clears_CollectionCache()
    {
        const string entryId = "EntryId";
        const string collectionSysId = "CollectionSysId";
        const string collectionId = "CollectionId";
        Entry<ContentfulContent> entry = CreateEntry(PrintableCollection.ContentType, entryId);

        _contentfulClient.GetEntry<PrintableCollection>(entryId).Returns(Task.FromResult(new PrintableCollection
        {
            Sys = new SystemProperties
            {
                Id = collectionSysId
            },
            Identifier = collectionId,
            Title = "Title"
        }));

        await _publishContentfulWebhook.Consume(entry, "Topic");

        await _fusionCache.Received(1).RemoveAsync($"collection:{collectionId}");
        await _fusionCache.Received(1).RemoveByTagAsync($"pc-{collectionId}");
        await _fusionCache.Received(1).RemoveAsync(collectionSysId);
        await AssertDefaultCacheClears(entryId);
    }

    [Test]
    public async Task Consume_WhenContentTypeIs_Page_And_SlugIsNotChanged_Clears_PageCache()
    {
        const string entryId = "EntryId";
        const string pageSlug = "page-slug";
        Entry<ContentfulContent> entry = CreateEntry(Page.ContentType, entryId);
        Page page = new Page { Sys = new SystemProperties { Id = entryId }, Slug = pageSlug };

        _contentfulClient.GetEntry<Page>(entryId).Returns(Task.FromResult(page));
        _fusionCache.TryGetAsync<Page>(entryId)
            .Returns(MaybeValue<Page>.FromValue(page)); // This means the slug hasn't changed

        await _publishContentfulWebhook.Consume(entry, "Topic");

        await _fusionCache.Received(1).RemoveAsync($"content:{pageSlug}");
        await _fusionCache.Received(1).RemoveByTagAsync(pageSlug);
        await AssertDefaultCacheClears(entryId);
    }

    [Test]
    public async Task Consume_WhenContentTypeIs_Page_And_SlugIsChanged_UpdatesRedirectionRules()
    {
        const string entryId = "EntryId";
        const string pageSlug = "page-slug";
        const string oldPageSlug = "old-page-slug";
        const string redirectionRulesId = "RedirectionRulesId";
        Entry<ContentfulContent> entry = CreateEntry(Page.ContentType, entryId);

        RedirectionRules redirectionRules = new RedirectionRules
        {
            Sys = new SystemProperties { Id = redirectionRulesId, Version = 1, PublishedVersion = 1 },
            Rules = []
        };

        Entry<dynamic> updatedEntryResponse = new()
        {
            SystemProperties = new SystemProperties { Id = redirectionRulesId, Version = 2 }
        };

        Page pageFromContentful = new Page { Sys = new SystemProperties { Id = entryId }, Slug = pageSlug };
        Page outdatedPageInCache = new Page { Sys = new SystemProperties { Id = entryId }, Slug = oldPageSlug };

        _contentfulClient.GetEntry<Page>(entryId).Returns(Task.FromResult(pageFromContentful));
        _fusionCache.TryGetAsync<Page>(entryId).Returns(MaybeValue<Page>.FromValue(outdatedPageInCache));
        _contentfulClient.GetEntries(Arg.Any<QueryBuilder<RedirectionRules>>()).Returns(
            new ContentfulCollection<RedirectionRules>
            {
                Items = [redirectionRules]
            });
        _contentfulManagementClient.CreateOrUpdateEntry(Arg.Any<Entry<dynamic>>(), contentTypeId: Arg.Any<string>(),
                version: Arg.Any<int?>())
            .ReturnsForAnyArgs(updatedEntryResponse);

        await _publishContentfulWebhook.Consume(entry, "Topic");

        await _fusionCache.Received(1).RemoveAsync($"content:{pageSlug}");
        await _fusionCache.Received(1).RemoveAsync($"content:{oldPageSlug}");
        await _fusionCache.Received(1).RemoveByTagAsync(oldPageSlug);
        await _contentfulManagementClient.ReceivedWithAnyArgs(1).CreateOrUpdateEntry(Arg.Any<Entry<dynamic>>(),
            contentTypeId: RedirectionRules.ContentType, version: 2);
        await _contentfulManagementClient.Received(1).PublishEntry(redirectionRulesId, 2);
        await AssertDefaultCacheClears(entryId);
    }

    [Test]
    public async Task Consume_WhenContentTypeIs_NotPage_Finds_LinkedPagesAndClearsCache()
    {
        const string entryId = "EntryId";
        const string pageId = "PageId";
        const string pageSlug = "page-slug";
        Entry<ContentfulContent> entry = CreateEntry(RichContent.ContentType, entryId);
        Page linkedPage = new Page { Sys = new SystemProperties { Id = pageId }, Slug = pageSlug };

        _contentfulClient.GetEntries(Arg.Any<QueryBuilder<ContentfulContent>>())
            .Returns(new ContentfulCollection<ContentfulContent> { Items = [linkedPage] });

        await _publishContentfulWebhook.Consume(entry, "Topic");

        await _fusionCache.Received(1).RemoveAsync(pageId);
        await _fusionCache.Received(1).RemoveAsync($"content:{pageSlug}");
        await _fusionCache.Received(1).RemoveByTagAsync(pageSlug);
        await AssertDefaultCacheClears(entryId, pageId);
    }

    [Test]
    public async Task Consume_WhenTopicIsPublish_And_EntryTypeIsValid_RepublishesPages()
    {
        const string entryId = "EntryId";
        const string pageId = "PageId";
        const string pageSlug = "page-slug";
        Entry<ContentfulContent> entry = CreateEntry(RichContent.ContentType, entryId);
        Page linkedPage = new Page
            { Sys = new SystemProperties { Id = pageId, PublishedVersion = 1 }, Slug = pageSlug };

        _contentfulClient.GetEntries(Arg.Any<QueryBuilder<ContentfulContent>>())
            .Returns(new ContentfulCollection<ContentfulContent> { Items = [linkedPage] });

        await _publishContentfulWebhook.Consume(entry, "ContentManagement.Entry.publish");

        await _contentfulManagementClient.Received(1).PublishEntry(pageId, 2);
        await AssertDefaultCacheClears(entryId, pageId);
    }

    private static Entry<ContentfulContent> CreateEntry(string contentType, string entryId) =>
        new()
        {
            SystemProperties = new SystemProperties
            {
                Id = entryId,
                ContentType = new ContentType
                {
                    SystemProperties = new SystemProperties { Id = contentType }
                }
            }
        };

    private async Task AssertDefaultCacheClears(params string[] ids)
    {
        foreach (string id in ids) await _fusionCache.Received(1).RemoveAsync(id);
        await _fusionCache.Received(1).RemoveAsync("content:sitemap");
        await _fusionCache.Received(1).RemoveAsync("content:hierarchy");
    }

    [TearDown]
    public void Teardown()
    {
        _fusionCache.Dispose();
    }
}