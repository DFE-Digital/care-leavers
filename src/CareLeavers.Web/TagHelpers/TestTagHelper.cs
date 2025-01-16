using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CareLeavers.Web.TagHelpers;

[HtmlTargetElement("test-tag-helper")]
public class TestTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Content.SetContent("Simple Tag Helper Rendered");
    }
}