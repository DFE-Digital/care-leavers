using CareLeavers.Web.Contentful;
using CareLeavers.Web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace CareLeavers.Web.Controllers;

[Route("statuschecker")]
public class StatusCheckerController (IContentService contentService) : Controller
{
    // POST
    [HttpPost]
    [Route("index")]
    [Route("/statuschecker")]
    public async Task<IActionResult> Index(string[] answers, string checkerId)
    {
        var statusChecker = await contentService.GetStatusChecker(checkerId);

        if (statusChecker is { Answers: not null } && statusChecker.Answers.Count != 0)
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
                return Redirect($"/{result.Target.Slug}");
            }

            // If not, redirect back to the question page
            return Redirect($"/{statusChecker?.Page?.Slug}");
        }

        return NoContent();
    }
}