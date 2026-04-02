using CareLeavers.Web.Configuration;
using CareLeavers.Web.Controllers;
using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Models.ViewModels;
using CareLeavers.Web.Translation;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace CareLeavers.Web.Tests.Controllers;

public class TranslationControllerTests
{
    private ITranslationService _translationService;
    private IContentfulConfiguration _contentfulConfiguration;
    
    private TranslationController _translationController;

    [SetUp]
    public void Init()
    {
        _translationService = Substitute.For<ITranslationService>();
        _contentfulConfiguration = Substitute.For<IContentfulConfiguration>();
        
        _translationController = new TranslationController(_translationService, _contentfulConfiguration);
    }

    [Test]
    public async Task Index_WhenTranslationIs_Enabled_Returns_View()
    {
        const string slug = "test-slug";
        _contentfulConfiguration.GetConfiguration().Returns(new ContentfulConfigurationEntity { TranslationEnabled =  true });
        List<TranslationLanguage> languages = [ new() { Code = "sv"} ];
        _translationService.GetLanguages().Returns(languages);
        
        IActionResult result = await _translationController.Index(slug);
        
        Assert.That(result, Is.TypeOf<ViewResult>());
        ViewResult? viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        Assert.That(viewResult.Model, Is.TypeOf<TranslationViewModel>());
        TranslationViewModel? viewModel = viewResult.Model as TranslationViewModel;
        Assert.That(viewModel, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(viewModel.Slug, Is.EqualTo(slug));
            Assert.That(viewModel.Languages, Is.EqualTo(languages));
        }
    }

    [Test]
    public async Task Index_WhenTranslationIs_Disabled_Returns_NotFound()
    {
        _contentfulConfiguration.GetConfiguration().Returns(new ContentfulConfigurationEntity { TranslationEnabled =  false });

        IActionResult result = await _translationController.Index("test-slug");
        
        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }
    
    [Test]
    public async Task Page_WhenTranslationIs_Enabled_Returns_View()
    {
        const string page = "test-page";
        _contentfulConfiguration.GetConfiguration().Returns(new ContentfulConfigurationEntity { TranslationEnabled =  true });
        List<TranslationLanguage> languages = [ new() { Code = "sv"} ];
        _translationService.GetLanguages().Returns(languages);
        
        IActionResult result = await _translationController.Page(page);
        
        Assert.That(result, Is.TypeOf<ViewResult>());
        ViewResult? viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        Assert.That(viewResult.Model, Is.TypeOf<TranslationViewModel>());
        TranslationViewModel? viewModel = viewResult.Model as TranslationViewModel;
        Assert.That(viewModel, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(viewModel.Page, Is.EqualTo(page));
            Assert.That(viewModel.Languages, Is.EqualTo(languages));
        }
    }

    [Test]
    public async Task Page_WhenTranslationIs_Disabled_Returns_NotFound()
    {
        _contentfulConfiguration.GetConfiguration().Returns(new ContentfulConfigurationEntity { TranslationEnabled =  false });

        IActionResult result = await _translationController.Page("test-page");
        
        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }

    [TearDown]
    public void Teardown()
    {
        _translationController.Dispose();
    }
}