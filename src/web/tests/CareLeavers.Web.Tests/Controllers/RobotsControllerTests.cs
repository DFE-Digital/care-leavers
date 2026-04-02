using CareLeavers.Web.Configuration;
using CareLeavers.Web.Controllers;
using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Translation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using NSubstitute;

namespace CareLeavers.Web.Tests.Controllers;

public class RobotsControllerTests
{
    private IContentfulConfiguration _contentfulConfiguration;
    private ITranslationService _translationService;
    private IUrlHelper _urlHelper;

    private RobotsController _robotsController;

    [SetUp]
    public void Init()
    {
        _contentfulConfiguration = Substitute.For<IContentfulConfiguration>();
        _translationService = Substitute.For<ITranslationService>();
        _urlHelper = Substitute.For<IUrlHelper>();

        _urlHelper.ActionContext.Returns(new ActionContext(new DefaultHttpContext(), new RouteData(),
            new ActionDescriptor()));

        _robotsController = new RobotsController(_contentfulConfiguration, _translationService) { Url = _urlHelper };
    }

    [Test]
    public async Task Robots_WhenTranslation_Enabled_IncludesOtherLanguages()
    {
        ContentfulConfigurationEntity config = new ContentfulConfigurationEntity { TranslationEnabled = true };
        _contentfulConfiguration.GetConfiguration().Returns(config);

        List<TranslationLanguage> languages =
        [
            new() { Code = "en" }, 
            new() { Code = "sv" },
            new() { Code = "pt" }
        ];
        _translationService.GetLanguages().Returns(languages);

        IActionResult result = await _robotsController.Robots();
        
        Assert.That(result, Is.TypeOf<ContentResult>());
        ContentResult contentResult = (ContentResult)result;
        Assert.That(contentResult.Content, Does.Contain("Disallow: /sv/"));
        Assert.That(contentResult.Content, Does.Contain("Disallow: /pt/"));
        Assert.That(contentResult.Content, Does.Not.Contain("Disallow: /en/"));
    }

    [Test]
    public async Task Robots_WhenTranslation_Disabled_ReturnsDefaultContent()
    {
        const string sitemapUrl = "https://localhost:1234/sitemap";
        ContentfulConfigurationEntity config = new ContentfulConfigurationEntity { TranslationEnabled = false };
        _contentfulConfiguration.GetConfiguration().Returns(config);

        _urlHelper.Action(Arg.Any<UrlActionContext>()).Returns(info =>
        {
            UrlActionContext context = (UrlActionContext)info[0];
            return context.Action is not null && context.Action.Equals("Sitemap") ? sitemapUrl : null;
        });
        
        IActionResult result = await _robotsController.Robots();
        
        Assert.That(result, Is.TypeOf<ContentResult>());
        ContentResult contentResult = (ContentResult)result;
        using (Assert.EnterMultipleScope())
        {
            Assert.That(contentResult.ContentType, Is.EqualTo("text/plain"));
            Assert.That(contentResult.Content, Does.Contain("User-agent: *"));
        }
        Assert.That(contentResult.Content, Does.Contain("Sitemap: https://localhost:1234/sitemap"));
    }

    [TearDown]
    public void Teardown()
    {
        _robotsController.Dispose();
    }
}