using CareLeavers.Web.Contentful;
using CareLeavers.Web.Controllers;
using CareLeavers.Web.Translation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace CareLeavers.Web.Tests.Controllers;

public class ContentfulControllerTests
{
    [Test]
    public async Task HomePageRedirectsToHomeSlug()
    {
        var contentService = Substitute.For<IContentService>();
        var translationService = Substitute.For<ITranslationService>();
        var environment = Substitute.For<IHostEnvironment>();
        var logger = Substitute.For<ILogger<ContentfulController>>();
        
        var controller = new ContentfulController(contentService, translationService, environment, logger);
        
        var result = await controller.Homepage(new MockContentfulConfiguration());
        
        Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
    }
}