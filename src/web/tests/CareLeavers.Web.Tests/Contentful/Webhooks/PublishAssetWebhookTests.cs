using CareLeavers.Web.Contentful.Webhooks;
using CareLeavers.Web.Models.Content;
using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Core.Search;
using Microsoft.Extensions.Logging;
using NSubstitute;
using ZiggyCreatures.Caching.Fusion;

namespace CareLeavers.Web.Tests.Contentful.Webhooks;

public class PublishAssetWebhookTests
{
    private IContentfulClient _contentfulClient;
    private IFusionCache _fusionCache;

    private PublishAssetWebhook _publishAssetWebhook;

    [SetUp]
    public void Init()
    {
        _contentfulClient = Substitute.For<IContentfulClient>();
        _fusionCache = Substitute.For<IFusionCache>();

        _publishAssetWebhook = new PublishAssetWebhook(_contentfulClient, _fusionCache,
            Substitute.For<ILogger<PublishAssetWebhook>>());
    }

    [Test]
    public async Task Consume_DoesNotClearCache_If_ThereAreNoLinkedPages()
    {
        const string assetId = "Test";
        Asset asset = new Asset { SystemProperties = new SystemProperties { Id = assetId } };

        ContentfulCollection<ContentfulContent> noLinkedAssetsList = new() { Items = [] };

        _contentfulClient.GetEntries(Arg.Any<QueryBuilder<ContentfulContent>>())
            .Returns(Task.FromResult(noLinkedAssetsList));

        await _publishAssetWebhook.Consume(asset);

        await _fusionCache.DidNotReceiveWithAnyArgs().RemoveAsync(Arg.Is<string>(slug => slug.StartsWith("content:")));
    }

    [Test]
    public async Task Consume_ClearsCache_If_ThereAreLinkedPages()
    {
        const string assetId = "Test";
        Asset asset = new Asset { SystemProperties = new SystemProperties { Id = assetId } };

        const string pageId = "Test-Page";
        const string pageSlug = "test-slug";
        Page linkedPage = new Page { Sys = new SystemProperties { Id = pageId }, Slug = pageSlug };

        ContentfulCollection<ContentfulContent> linkedAssetsList = new() { Items = [linkedPage] };

        _contentfulClient.GetEntries(Arg.Any<QueryBuilder<ContentfulContent>>())
            .Returns(Task.FromResult(linkedAssetsList));

        await _publishAssetWebhook.Consume(asset);

        await _fusionCache.Received(1).RemoveAsync($"content:{pageSlug}");
        await _fusionCache.Received(1).RemoveAsync(pageId);
    }

    [Test]
    public async Task Consume_ClearsCache_If_ThereAreDeepLinkedPages()
    {
        const string assetId = "AssetId";
        const string richContentId = "RichContentId";
        const string pageId = "PageId";
        const string pageSlug = "test-slug";

        Asset asset = new Asset { SystemProperties = new SystemProperties { Id = assetId } };
        RichContentBlock richContentBlock = new RichContentBlock { Sys = new SystemProperties { Id = richContentId } };
        Page page = new Page { Sys = new SystemProperties { Id = pageId }, Slug = pageSlug };

        ContentfulCollection<ContentfulContent> assetLinkedContent = new() { Items = [richContentBlock] };
        ContentfulCollection<ContentfulContent> pageLinkedContent = new() { Items = [page] };

        _contentfulClient
            .GetEntries(
                Arg.Is<QueryBuilder<ContentfulContent>>(qB => qB.Build() == $"?links_to_asset={assetId}&include=0"))
            .Returns(Task.FromResult(assetLinkedContent));
        
        _contentfulClient
            .GetEntries(
                Arg.Is<QueryBuilder<ContentfulContent>>(qB => qB.Build() == $"?links_to_entry={richContentId}&include=0"))
            .Returns(Task.FromResult(pageLinkedContent));
        
        _contentfulClient
            .GetEntries(
                Arg.Is<QueryBuilder<ContentfulContent>>(qB => qB.Build() == $"?links_to_entry={pageId}&include=0"))
            .Returns(Task.FromResult(new ContentfulCollection<ContentfulContent>()));

        await _publishAssetWebhook.Consume(asset);
        
        await _fusionCache.Received(1).RemoveAsync(pageId);
        await _fusionCache.Received(1).RemoveAsync($"content:{pageSlug}");
    }

    [Test]
    public async Task Consume_DoesNotInfiniteLoop_If_ThereAreCircularReferences()
    {
        const string assetId = "AssetId";
        const string richContentIdOne = "RichContentIdOne";
        const string richContentIdTwo = "RichContentIdTwo";
        const string pageId = "PageOneId";
        const string pageSlug = "page-one-slug";

        Asset asset = new Asset { SystemProperties = new SystemProperties { Id = assetId } };
        RichContentBlock richContentBlockOne = new RichContentBlock { Sys = new SystemProperties { Id = richContentIdOne } };
        RichContentBlock richContentBlockTwo = new RichContentBlock { Sys = new SystemProperties { Id = richContentIdTwo } };
        Page page = new Page { Sys = new SystemProperties { Id = pageId }, Slug = pageSlug };

        ContentfulCollection<ContentfulContent> assetLinkedContent = new() { Items = [richContentBlockOne] };
        ContentfulCollection<ContentfulContent> richContentBlockOneLinkedContent = new() { Items = [richContentBlockTwo, page] };
        ContentfulCollection<ContentfulContent> richContentBlockTwoLinkedContent = new() { Items = [richContentBlockOne] };

        _contentfulClient
            .GetEntries(
                Arg.Is<QueryBuilder<ContentfulContent>>(qB => qB.Build() == $"?links_to_asset={assetId}&include=0"))
            .Returns(Task.FromResult(assetLinkedContent));

        _contentfulClient
            .GetEntries(
                Arg.Is<QueryBuilder<ContentfulContent>>(qB => qB.Build() == $"?links_to_entry={richContentIdOne}&include=0"))
            .Returns(Task.FromResult(richContentBlockOneLinkedContent));

        _contentfulClient
            .GetEntries(
                Arg.Is<QueryBuilder<ContentfulContent>>(qB => qB.Build() == $"?links_to_entry={richContentIdTwo}&include=0"))
            .Returns(Task.FromResult(richContentBlockTwoLinkedContent));
        
        _contentfulClient
            .GetEntries(
                Arg.Is<QueryBuilder<ContentfulContent>>(qB => qB.Build() == $"?links_to_entry={pageId}&include=0"))
            .Returns(Task.FromResult(new ContentfulCollection<ContentfulContent>()));

        await _publishAssetWebhook.Consume(asset);

        await _fusionCache.Received(1).RemoveAsync(pageId);
        await _fusionCache.Received(1).RemoveAsync($"content:{pageSlug}");
        await _fusionCache.Received(1).RemoveAsync(richContentIdOne);
        await _fusionCache.Received(1).RemoveAsync(richContentIdTwo);
    }

    [TearDown]
    public void Teardown()
    {
        _fusionCache.Dispose();
    }
}