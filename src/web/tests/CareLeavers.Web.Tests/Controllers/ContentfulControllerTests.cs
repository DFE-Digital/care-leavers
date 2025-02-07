using CareLeavers.Web.Caching;
using CareLeavers.Web.Controllers;
using Contentful.Core;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace CareLeavers.Web.Tests.Controllers;

public class ContentfulControllerTests
{
    [Test]
    public async Task HomePageRedirectsToHomeSlug()
    {
        var contentfulClient = Substitute.For<IContentfulClient>();
        
        var controller = new ContentfulController(new CacheDisabledDistributedCache(), contentfulClient);
        
        var result = await controller.Homepage(new MockContentfulConfiguration());
        
        Assert.That(result, Is.InstanceOf<RedirectResult>());
    }
}