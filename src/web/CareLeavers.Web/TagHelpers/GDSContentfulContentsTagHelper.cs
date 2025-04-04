using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Models.Enums;
using Contentful.Core.Models;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CareLeavers.Web.TagHelpers;

[HtmlTargetElement("gds-contentful-contents", TagStructure = TagStructure.WithoutEndTag)]
public class GDSContentfulContentsTagHelper : TagHelper
{
    private readonly HtmlRenderer _renderer;
    
    /// <summary>
    /// The document to render.
    /// </summary>
    public Document? Document { get; set; }
    
    public List<Grid>? Grids { get; set; }

    public HeadingType[] Levels { get; set; } = [HeadingType.H2];
    
    public GDSContentfulContentsTagHelper(HtmlRenderer renderer)
    {
        _renderer = renderer;
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (Levels.Length == 0)
            return;
        
        // Set this up as a navigation element
        output.TagName = "";

        TagBuilder contents = new TagBuilder("nav");
        contents.GenerateId("main-content-contents", "");
        TagBuilder list = new TagBuilder("ul");
        list.AddCssClass("govuk-list");
        
        // Get our rendered HTML content
        var html = new HtmlDocument();
        html.LoadHtml(await _renderer.ToHtml(Document));

        var headingsList = new List<string>();
        
        foreach (var level in Levels)
        {
            headingsList.Add("self::" + level.ToString().ToLower());
        }
        
        // Get our list of heading tags with IDs and their text
        var headings = html.DocumentNode.SelectNodes($"//*[({string.Join(" or ", headingsList)}) and @id!=\"\"]");

        if (headings != null)
        {
            
            // Loop through
            foreach (var heading in headings)
            {
                TagBuilder item = new TagBuilder("li");
                item.AddCssClass("govuk-body-s");
                item.InnerHtml.AppendHtml($"<a href=\"#{heading.Id}\" class=\"govuk-link\">{heading.InnerHtml}</a>");
                list.InnerHtml.AppendHtml(item);
            }

            

           
        }

        if (list.HasInnerHtml)
        {
            contents.InnerHtml.AppendHtml("<h2 class=\"govuk-heading-s\">Contents</h2>");
            contents.InnerHtml.AppendHtml(list);
        }
        output.Content.SetHtmlContent(contents);
    }
}