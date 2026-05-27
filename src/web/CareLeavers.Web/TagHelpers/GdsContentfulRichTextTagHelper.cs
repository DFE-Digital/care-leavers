using CareLeavers.Web.Configuration;
using Contentful.AspNetCore.TagHelpers;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CareLeavers.Web.TagHelpers;

[HtmlTargetElement("gds-contentful-rich-text")]
public class GdsContentfulRichTextTagHelper(HtmlRenderer renderer, IHttpContextAccessor accessor)
    : ContentfulRichTextTagHelper(renderer)
{
    public bool LargerText { get; set; }
    public bool DynamicTime { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;
        base.Process(context, output);

        output.Content.SetHtmlContent(ProcessFlags(output.Content.GetContent()));
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;
        await base.ProcessAsync(context, output);

        output.Content.SetHtmlContent(ProcessFlags(output.Content.GetContent()));
    }

    private string ProcessFlags(string content)
    {
        if (LargerText)
        {
            content = content.Replace("govuk-body", "govuk-body-l");
        }

        if (DynamicTime)
        {
            string? timeout =
                accessor.HttpContext?.Session.GetString(FairUsageOptions.AzureTranslationTimeoutKey);

            content = timeout is not null && content.Contains("((time))")
                ? content.Replace("((time))", timeout)
                : """
                  <p class="govuk-body">You will be able to translate into a new language in 24 hours.</p>
                  <p class="govuk-body">You can <a class="govuk-link" href="/translate-this-website/home">continue to translate the site</a> into a language you have selected already.</p>
                  """;
        }

        return content;
    }
}