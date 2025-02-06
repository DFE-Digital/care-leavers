using CareLeavers.Web.Caching;
using CareLeavers.Web.Controllers;
using Contentful.Core;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace CareLeavers.Web.Tests.Controllers;

public class ContentfulControllerTests
{
    [Test]
    public void HomePageRedirectsToHomeSlug()
    {
        var contentfulClient = Substitute.For<IContentfulClient>();
        
        var controller = new ContentfulController(new CacheDisabledDistributedCache(), contentfulClient);
        
        var result = controller.Homepage();
        
        Assert.That(result, Is.InstanceOf<RedirectResult>());
    }
}