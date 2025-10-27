using CareLeavers.Web.Contentful;
using CareLeavers.Web.Models.Content;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CareLeavers.Web.Controllers;

public class GetToAnAnswerController (IContentService contentService) : Controller
{
    [HttpGet("gtaa/{slug}")]
    public async Task<IActionResult> EmbeddedGetToAnAnswer(string slug)
    {
        var gtaa = new GetToAnAnswer()
        {
            questionnaireSlug = slug,
            Sys = new SystemProperties() { Id = slug }
        };
        var getToAnAnswer = await contentService.Hydrate(gtaa);

        return View("EmbeddedGetToAnAnswer", new Page
        {
            MainContent = new Document
            {
                Content = [getToAnAnswer]
            }
        });
    }
}