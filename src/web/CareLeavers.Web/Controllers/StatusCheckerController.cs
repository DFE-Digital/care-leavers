using CareLeavers.Web.Contentful;
using CareLeavers.Web.Models.Content;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CareLeavers.Web.Controllers;

[Route("statuschecker")]
public class StatusCheckerController (IContentService contentService) : Controller
{
    // POST
    [HttpPost]
    [Route("index")]
    [Route("/statuschecker")]
    public async Task<IActionResult> Index(string[] answers, string checkerId, string languageCode)
    {
        
        var checker = new StatusChecker()
        {
            Sys = new SystemProperties() { Id = checkerId }
        };
        var statusChecker = await contentService.Hydrate(checker);

        if (statusChecker is { Answers: not null } && statusChecker.Answers.Count != 0 && answers.Length != 0)
        {
            // Get the answers from the status checker that match the answers given
            // Grab the first match, ordered by priority descending
            var result = statusChecker?.Answers
                .Where(c => answers.Contains(c.Sys?.Id))
                .OrderByDescending(c => c.Priority)
                .FirstOrDefault();

            // If we have a match, redirect to that page
            if (result is { Target: not null })
            {
                return RedirectToAction("GetContent", "Contentful", new { slug = result.Target.Slug, languageCode });
            }
        }

        var redirect = RedirectToAction("GetContent", "Contentful", new { slug = statusChecker?.Page?.Slug, languageCode, errorMessage = statusChecker?.ValidationError });
        return redirect;
    }
}