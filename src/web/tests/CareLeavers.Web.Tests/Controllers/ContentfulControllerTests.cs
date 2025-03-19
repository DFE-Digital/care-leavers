using CareLeavers.Web.Contentful;
using CareLeavers.Web.Controllers;
using CareLeavers.Web.Translation;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace CareLeavers.Web.Tests.Controllers;

public class ContentfulControllerTests
{
    [Test]
    public async Task HomePageRedirectsToHomeSlug()
    {
        var contentService = Substitute.For<IContentService>();
        var translationService = Substitute.For<ITranslationService>();
        
        var controller = new ContentfulController(contentService, translationService);
        
        var result = await controller.Homepage(new MockContentfulConfiguration());
        
        Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
    }
}