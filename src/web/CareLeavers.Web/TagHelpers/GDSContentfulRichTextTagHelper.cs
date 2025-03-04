using Contentful.AspNetCore.TagHelpers;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CareLeavers.Web.TagHelpers;

[HtmlTargetElement("gds-contentful-rich-text")]
public class GDSContentfulRichTextTagHelper(HtmlRenderer renderer) : ContentfulRichTextTagHelper(renderer)
{
    public bool LargerText { get; set; } = false;
    
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;
        base.Process(context, output);
        
        if (LargerText)
        {
            var content = output.Content.GetContent();
            content = content.Replace("govuk-body", "govuk-body-l");
            output.Content = new DefaultTagHelperContent();
            output.Content.SetHtmlContent(content);
        }
        
    }
    
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;
        var result = base.ProcessAsync(context, output);
        result.Wait();
        if (LargerText)
        {
            var content = output.Content.GetContent();
            content = content.Replace("govuk-body", "govuk-body-l");
            output.Content = new DefaultTagHelperContent();
            output.Content.SetHtmlContent(content);
        }

        return result;
    }
}