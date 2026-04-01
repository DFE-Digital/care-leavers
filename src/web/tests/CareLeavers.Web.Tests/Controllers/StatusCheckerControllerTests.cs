using CareLeavers.Web.Contentful;
using CareLeavers.Web.Controllers;
using CareLeavers.Web.Models.Content;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace CareLeavers.Web.Tests.Controllers;

public class StatusCheckerControllerTests
{
    private IContentService _contentService;
    
    private StatusCheckerController _statusCheckerController;

    [SetUp]
    public void Init()
    {
        _contentService = Substitute.For<IContentService>();
        
        _statusCheckerController = new StatusCheckerController(_contentService);
    }

    [Test]
    public async Task Index_Redirects_ToTarget()
    {
        const string languageCode = "en";
        const string slug = "test-slug";
        string[] answers = [ "answer-1" ];

        Page page = new Page { Slug = slug };
        AnswerEntity matchingAnswer = new AnswerEntity
        {
            Sys = new SystemProperties { Id = answers[0] },
            Target = page,
            Priority = 1
        };

        StatusChecker statusChecker = new StatusChecker
        {
            Answers = [ matchingAnswer ]
        };
        
        _contentService.Hydrate(Arg.Any<StatusChecker>()).Returns(statusChecker);

        IActionResult result = await _statusCheckerController.Index(answers, "test-id", languageCode);
        
        Assert.That(result, Is.TypeOf<RedirectToActionResult>());
        RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
        using (Assert.EnterMultipleScope())
        {
            Assert.That(redirectToActionResult.ActionName, Is.EqualTo("GetContent"));
            Assert.That(redirectToActionResult.ControllerName, Is.EqualTo("Contentful"));
            Assert.That(redirectToActionResult.RouteValues, Is.Not.Null);
        }

        using (Assert.EnterMultipleScope())
        {
            Assert.That(redirectToActionResult.RouteValues["slug"], Is.EqualTo(slug));
            Assert.That(redirectToActionResult.RouteValues["languageCode"], Is.EqualTo(languageCode));
        }
    }

    [Test]
    public async Task Index_WhenMultipleMatchingAnswers_Redirects_ToHighestPriorityTarget()
    {
        string[] answers = [ "answer-1", "answer-2" ];
        
        Page pageOne = new Page { Slug = "page-one" };
        Page pageTwo = new Page { Slug = "page-two" };

        AnswerEntity answerOne = new AnswerEntity { Sys =  new SystemProperties { Id = answers[0] }, Target = pageOne, Priority = 1 };
        AnswerEntity answerTwo = new AnswerEntity { Sys =  new SystemProperties { Id = answers[1] }, Target = pageTwo, Priority = 2 };

        StatusChecker statusChecker = new StatusChecker { Answers = [ answerOne, answerTwo ]};
        
        _contentService.Hydrate(Arg.Any<StatusChecker>()).Returns(statusChecker);

        IActionResult result = await _statusCheckerController.Index(answers, "id", "en");
        
        Assert.That(result, Is.TypeOf<RedirectToActionResult>());
        RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
        Assert.That(redirectToActionResult.RouteValues, Is.Not.Null);
        Assert.That(redirectToActionResult.RouteValues["slug"], Is.EqualTo(pageTwo.Slug));
    }

    [Test]
    public async Task Index_WhenNoMatchingAnswers_Redirects_ToErrorPage()
    {
        Page page = new Page { Slug = "error-page" };

        StatusChecker statusChecker = new StatusChecker
        {
            Page = page,
            ValidationError = "Error Message",
            Answers = []
        };
        
        _contentService.Hydrate(Arg.Any<StatusChecker>()).Returns(statusChecker);

        IActionResult result = await _statusCheckerController.Index([], "id", "en");
        
        Assert.That(result, Is.TypeOf<RedirectToActionResult>());
        RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
        Assert.That(redirectToActionResult.RouteValues, Is.Not.Null);
        Assert.That(redirectToActionResult.RouteValues["errorMessage"], Is.EqualTo("Error Message"));
    }

    [TearDown]
    public void Teardown()
    {
        _statusCheckerController.Dispose();
    }
}