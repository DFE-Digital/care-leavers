using CareLeavers.Web.Models.Content;
using Contentful.Core.Models;

namespace CareLeavers.Web.ContentfulRenderers;

/// <summary>
/// A renderer for a paragraph.
/// </summary>
public class GDSGetToAnAnswerRenderer(IServiceProvider serviceProvider) : GDSRazorContentRenderer(serviceProvider)
{
    private readonly IConfiguration _configuration = serviceProvider.GetRequiredService<IConfiguration>();

    /// <summary>
    /// Whether or not this renderer supports the provided content.
    /// </summary>
    /// <param name="content">The content to evaluate.</param>
    /// <returns>Returns true if the content is a paragraph, otherwise false.</returns>
    public override bool SupportsContent(IContent content)
    {
        if (content is EntryStructure)
        {
            var structure = content as EntryStructure;
            if (structure?.NodeType == "embedded-entry-block")
            {
                if (structure.Data.Target is GetToAnAnswer)
                    return true;
            }
        }

        return content is GetToAnAnswer;
    }

    public override string Render(IContent content)
    {
        var result = RenderAsync(content);
        result.Wait();
        return result.Result;
    }

    /// <summary>
    /// Renders the content to an html p-tag.
    /// </summary>
    /// <param name="content">The content to render.</param>
    /// <returns>The p-tag as a string.</returns>
    public override Task<string> RenderAsync(IContent content)
    {
        GetToAnAnswer? getToAnAnswer;
        if (content is GetToAnAnswer)
        {
            getToAnAnswer = content as GetToAnAnswer;
        }
        else
        {
            getToAnAnswer = (content as EntryStructure)?.Data.Target as GetToAnAnswer;
        }
        
        if (getToAnAnswer is not null)
        {
            getToAnAnswer.BaseUrl = _configuration["GetToAnAnswer:BaseUrl"];
        }

        return RenderToString("GetToAnAnswer", getToAnAnswer);
    }
}