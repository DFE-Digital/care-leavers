using CareLeavers.Web.Contentful;
using CareLeavers.Web.Filters;
using CareLeavers.Web.GetToAnAnswerRun;
using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Translation;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CareLeavers.Web.Controllers;

public class GetToAnAnswerController (
    IContentService contentService,
    IGetToAnAnswerRunClient getToAnAnswerRunClient,
    ILogger<ContentfulController> logger
) : Controller
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
    
    [HttpGet("/{languageCode}/get-to-an-answer-questionnaires/{slug}/start")]
    [Translation]
    public async Task<IActionResult> GetStartPageOrInitialState(string languageCode, string slug)
    {
        try
        {
            var html = await getToAnAnswerRunClient.GetStartPageOrInitialState(languageCode, slug);

            return Content(html, "text/html");
        } catch (Exception e)
        {
            logger.LogError(e, "Error getting start state");
            return NotFound();
        }
    }
    
    [HttpGet("/{languageCode}/get-to-an-answer-questionnaires/{slug}/next")]
    [Translation]
    public async Task<IActionResult> GetInitialState(string languageCode, string slug)
    {
        try
        {
            var html = await getToAnAnswerRunClient.GetInitialState(languageCode, slug);

            return Content(html, "text/html");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error getting next state");
            return NotFound();
        }
    }
    
    [HttpPost("/{languageCode}/get-to-an-answer-questionnaires/{slug}/next")]
    [Translation]
    public async Task<IActionResult> GetNextState(string languageCode, string slug)
    {
        try
        {
            var formData = Request.Form.ToDictionary(
                x => x.Key,
                x => x.Value
            );

            var html = await getToAnAnswerRunClient.GetNextState(languageCode, slug, formData);

            return Content(html, "text/html");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error getting next state");
            return NotFound();
        }
    }
    
    [HttpGet("/{languageCode}/get-to-an-answer-questionnaires/{slug}/decorative-image")]
    [Translation]
    public async Task<IActionResult> GetDecorativeImage(string languageCode, string slug)
    {
        try
        {
            // this is a file stream
            var (fileStream, contentType) = await getToAnAnswerRunClient.GetDecorativeImage(slug);
            
            return new FileStreamResult(fileStream, contentType);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error getting next state");
            return NotFound();
        }
    }
}