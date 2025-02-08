using CareLeavers.Web.Contentful;
using CareLeavers.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace CareLeavers.Web.Tests.Controllers;

public class ContentfulControllerTests
{
    [Test]
    public async Task HomePageRedirectsToHomeSlug()
    {
        var contentService = Substitute.For<IContentService>();
        
        var controller = new ContentfulController(contentService);
        
        var result = await controller.Homepage(new MockContentfulConfiguration());
        
        Assert.That(result, Is.InstanceOf<RedirectResult>());
    }
}