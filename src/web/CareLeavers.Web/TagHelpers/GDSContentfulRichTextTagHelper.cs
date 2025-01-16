using Contentful.AspNetCore.TagHelpers;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CareLeavers.Web.TagHelpers;

[HtmlTargetElement("gds-contentful-rich-text")]
public class GDSContentfulRichTextTagHelper : ContentfulRichTextTagHelper
{
    public GDSContentfulRichTextTagHelper(HtmlRenderer renderer) : base(renderer)
    {
    }
    
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;
        base.Process(context, output);
    }
    
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;
        return base.ProcessAsync(context, output);
    }
}