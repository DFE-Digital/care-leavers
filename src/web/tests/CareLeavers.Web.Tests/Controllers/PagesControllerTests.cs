using CareLeavers.Web.Contentful;
using CareLeavers.Web.Controllers;
using CareLeavers.Web.Models;
using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Models.ViewModels;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace CareLeavers.Web.Tests.Controllers;

public class PagesControllerTests
{
    private IContentService _contentService;

    private HttpContext _httpContext;
    private ITempDataDictionary _tempData;
    
    private PagesController _pagesController;

    [SetUp]
    public void Init()
    {
        _contentService = Substitute.For<IContentService>();
        
        _httpContext = Substitute.For<HttpContext>();
        _tempData = Substitute.For<ITempDataDictionary>();
        
        _pagesController = new PagesController(_contentService);
        _pagesController.TempData = _tempData;
        _pagesController.ControllerContext = new ControllerContext
        {
            HttpContext = _httpContext
        };
    }

    [Test]
    public async Task PrivacyPolicies_Returns_View()
    {
        const string slug = "privacy-policies";
        Page page = new Page { Slug = slug };
        _contentService.GetPage(slug).Returns(page);
        
        IActionResult result = await _pagesController.PrivacyPolicies();
        
        Assert.That(result, Is.TypeOf<ViewResult>());
        ViewResult? viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        Assert.That(viewResult.Model, Is.EqualTo(page));
    }

    [Test]
    public async Task CookiePolicy_Returns_View_With_ViewModel()
    {
        const string title = "Cookies";
        const string slug = "cookie-policy";
        Page page = new Page { Title = title, Slug = slug };
        _contentService.GetPage(slug).Returns(page);
        
        ITrackingConsentFeature trackingConsentFeature = Substitute.For<ITrackingConsentFeature>();
        trackingConsentFeature.CanTrack.Returns(true);

        FeatureCollection collection = new FeatureCollection();
        collection.Set(trackingConsentFeature);
        
        _httpContext.Features.Returns(collection);
        
        IActionResult result = await _pagesController.CookiePolicy();
        
        Assert.That(result, Is.TypeOf<ViewResult>());
        ViewResult? viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        CookiePolicyModel? model = viewResult.Model as CookiePolicyModel;
        Assert.That(model, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(model.Page, Is.EqualTo(page));
            Assert.That(model.AcceptCookies, Is.True);
        }
    }

    [Test]
    public void CookiePolicy_WhenTrackingConsentFeatureIsUnavailable_Throws_InvalidOperationException()
    {
        _httpContext.Features.Returns(new FeatureCollection());

        Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)CookieTask);
        return;

        async Task CookieTask() => await _pagesController.CookiePolicy();
    }

    [Test]
    public async Task Error_WhenStatusCodeIs404_Returns_NotFoundPage()
    {
        Page page = new Page { Title = "Not Found Page" };
        _contentService.GetPage("page-not-found").Returns(page);

        IActionResult result = await _pagesController.Error(StatusCodes.Status404NotFound);
        
        Assert.That(result, Is.TypeOf<ViewResult>());
        ViewResult? viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(viewResult.Model, Is.EqualTo(page));
            Assert.That(viewResult.ViewName, Is.EqualTo("PageNotFound"));
            Assert.That(viewResult.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
        }
    }

    [Test]
    public async Task Error_WhenStatusCodeIsSpecific_And_Exists_Returns_ErrorPage()
    {
        Page page = new Page { Title = "Bad Request Page" };
        _contentService.GetPage("error-400").Returns(page);
        
        IActionResult result = await _pagesController.Error(StatusCodes.Status400BadRequest);
        
        Assert.That(result, Is.TypeOf<ViewResult>());
        ViewResult? viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        ErrorViewModel? error = viewResult.Model as ErrorViewModel;
        Assert.That(error, Is.Not.Null);
        Assert.That(error.Page, Is.EqualTo(page));
    }

    [Test]
    public async Task Error_WhenStatusCodeIsSpecific_And_NotExists_Returns_GenericErrorPage()
    {
        Page page = new Page { Title = "Default Error Page" };
        _contentService.GetPage("error-400").Returns((Page)null!);
        _contentService.GetPage("error").Returns(page);
        
        IActionResult result = await _pagesController.Error(StatusCodes.Status400BadRequest);
        
        Assert.That(result, Is.TypeOf<ViewResult>());
        ViewResult? viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        ErrorViewModel? errorViewModel = viewResult.Model as ErrorViewModel;
        Assert.That(errorViewModel, Is.Not.Null);
        Assert.That(errorViewModel.Page, Is.EqualTo(page));
    }

    [Test]
    public async Task ServiceUnavailable_Returns_Page()
    {
        Page page = new Page { Title = "Service Unavailable Page" };
        _contentService.GetPage("service-unavailable").Returns(page);
        
        IActionResult result = await _pagesController.ServiceUnavailable();
        
        Assert.That(result, Is.TypeOf<ViewResult>());
        ViewResult? viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        Assert.That(viewResult.Model, Is.EqualTo(page));
    }

    [Test]
    public async Task PageNotFound_SetsStatusCode_And_Returns_Page()
    {
        Page page = new Page { Title = "Not Found Page" };
        _contentService.GetPage("page-not-found").Returns(page);
        
        IActionResult result = await _pagesController.PageNotFound();
        
        Assert.That(result, Is.TypeOf<ViewResult>());
        ViewResult? viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(viewResult.Model, Is.EqualTo(page));
            Assert.That(viewResult.ViewName, Is.EqualTo("PageNotFound"));
            Assert.That(viewResult.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
        }
    }

    [Test]
    public void PostCookiePolicy_AppendsCookies_And_Returns_View()
    {
        CookiePolicyModel cookiePolicyModel = new CookiePolicyModel { AcceptCookies = true };
        CookiePolicyOptions cookiePolicyOptions = new CookiePolicyOptions
        {
            ConsentCookie = new CookieBuilder { Name = "consent-cookie"},
            ConsentCookieValue = "yes"
        };

        IOptions<CookiePolicyOptions> cookiePolicyOptionsService = Substitute.For<IOptions<CookiePolicyOptions>>();
        cookiePolicyOptionsService.Value.Returns(cookiePolicyOptions);

        _httpContext.Response.Cookies.Returns(Substitute.For<IResponseCookies>());
        
        IActionResult result = _pagesController.PostCookiePolicy(cookiePolicyModel, cookiePolicyOptionsService);
        
        Assert.That(result, Is.TypeOf<ViewResult>());
        ViewResult? viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(viewResult.ViewName, Is.EqualTo("CookiePolicy"));
            Assert.That(cookiePolicyModel.ShowSuccessBanner, Is.True);
        }
        _httpContext.Response.Cookies.Received(1).Append("consent-cookie", "yes", Arg.Any<CookieOptions>());
    }

    [Test]
    public void PostCookiePolicy_WhenCookiesAreDeclined_AppendsCookies_And_Returns_View()
    {
        CookiePolicyModel cookiePolicyModel = new CookiePolicyModel { AcceptCookies = false };
        CookiePolicyOptions cookiePolicyOptions = new CookiePolicyOptions
        {
            ConsentCookie = new CookieBuilder { Name = "consent-cookie"},
            ConsentCookieValue = "no"
        };
        
        IOptions<CookiePolicyOptions> cookiePolicyOptionsService = Substitute.For<IOptions<CookiePolicyOptions>>();
        cookiePolicyOptionsService.Value.Returns(cookiePolicyOptions);

        _httpContext.Response.Cookies.Returns(Substitute.For<IResponseCookies>());
        
        IActionResult result = _pagesController.PostCookiePolicy(cookiePolicyModel, cookiePolicyOptionsService);
        
        Assert.That(result, Is.TypeOf<ViewResult>());
        ViewResult? viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(viewResult.ViewName, Is.EqualTo("CookiePolicy"));
            Assert.That(cookiePolicyModel.ShowSuccessBanner, Is.True);
        }
        _httpContext.Response.Cookies.Received(1).Append("consent-cookie", "no", Arg.Any<CookieOptions>());
    }

    [Test]
    public async Task TranslationUnavailable_With_LanguageCode_Returns_View()
    {
        Page page = new Page { Title = "Test", MainContent = new Document
        {
            Content = [ new Paragraph() ]
        }};
        _contentService.GetPage("translation-unavailable").Returns(page);

        IActionResult result = await _pagesController.TranslationUnavailable("en");
        
        Assert.That(result, Is.TypeOf<ViewResult>());
        ViewResult? viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        Assert.That(viewResult.Model, Is.EqualTo(page));
    }
    
    [Test]
    public async Task TranslationUnavailable_Without_LanguageCode_Returns_View()
    {
        Page page = new Page { Title = "Test", MainContent = new Document
        {
            Content = [ new Paragraph() ]
        }};
        _contentService.GetPage("translation-unavailable").Returns(page);

        IActionResult result = await _pagesController.TranslationUnavailable(null);
        
        Assert.That(result, Is.TypeOf<ViewResult>());
        ViewResult? viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        Assert.That(viewResult.Model, Is.EqualTo(page));
    }

    [Test]
    public async Task TranslationUnavailable_WhenPageIsNull_Returns_Fallback_View()
    {
        Page? page = null;
        _contentService.GetPage("translation-unavailable").Returns(page);

        IActionResult result = await _pagesController.TranslationUnavailable(null);
        
        Assert.That(result, Is.TypeOf<ViewResult>());
        ViewResult? viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
    }
    
    [Test]
    public async Task TranslationUnavailable_WhenMainContentIsNull_Returns_Fallback_View()
    {
        Page page = new Page { Title = "Test", MainContent = null };
        _contentService.GetPage("translation-unavailable").Returns(page);

        IActionResult result = await _pagesController.TranslationUnavailable(null);
        
        Assert.That(result, Is.TypeOf<ViewResult>());
        ViewResult? viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        Page? pageResult = viewResult.Model as Page;
        Assert.That(pageResult, Is.Not.Null);
        Assert.That(pageResult.Title, Is.Not.EqualTo("Test"));
    }
    
    [Test]
    public async Task TranslationUnavailable_WhenMainContentIsEmpty_Returns_Fallback_View()
    {
        Page page = new Page { Title = "Test", MainContent = new Document { Content = [] } };
        _contentService.GetPage("translation-unavailable").Returns(page);

        IActionResult result = await _pagesController.TranslationUnavailable(null);
        
        Assert.That(result, Is.TypeOf<ViewResult>());
        ViewResult? viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        Page? pageResult = viewResult.Model as Page;
        Assert.That(pageResult, Is.Not.Null);
        Assert.That(pageResult.Title, Is.Not.EqualTo("Test"));
    }

    [TearDown]
    public void Teardown()
    {
        _pagesController.Dispose();
    }
}