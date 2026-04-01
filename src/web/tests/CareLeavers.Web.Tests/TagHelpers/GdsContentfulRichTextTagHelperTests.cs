using CareLeavers.Web.ContentfulRenderers;
using CareLeavers.Web.TagHelpers;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CareLeavers.Web.Tests.TagHelpers;

public class GdsContentfulRichTextTagHelperTests
{
    private readonly GdsContentfulRichTextTagHelper _tagHelper;

    public GdsContentfulRichTextTagHelperTests()
    {
        HtmlRenderer htmlRenderer = new(new HtmlRendererOptions
        {
            ListItemOptions = new ListItemContentRendererOptions
            {
                OmitParagraphTagsInsideListItems = true
            }
        });

        htmlRenderer.AddRenderer(new GdsHeaderRenderer(htmlRenderer.Renderers));

        _tagHelper = new GdsContentfulRichTextTagHelper(htmlRenderer);
    }

    [Test]
    public void Process_DoesNotModifyContent_If_LargerTextIsFalse()
    {
        _tagHelper.LargerText = false;
        
        TagHelperContext tagHelperContext = new([], new Dictionary<object, object>(), "Test");
        TagHelperOutput tagHelperOutput = new("gds-contentful-contents", [],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

        tagHelperOutput.Content.SetHtmlContent("<p class=\"govuk-body\">Test</p>");
        
        _tagHelper.Process(tagHelperContext, tagHelperOutput);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(tagHelperOutput.TagName, Is.Null);
            Assert.That(tagHelperOutput.Content.GetContent(), Is.EqualTo("<p class=\"govuk-body\">Test</p>"));
        }
    }

    [Test]
    public void Process_ModifiesContent_If_LargerTextIsTrue()
    {
        _tagHelper.LargerText = true;
        
        TagHelperContext tagHelperContext = new([], new Dictionary<object, object>(), "Test");
        TagHelperOutput tagHelperOutput = new("gds-contentful-contents", [],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

        tagHelperOutput.Content.SetHtmlContent("<p class=\"govuk-body\">Test</p>");
        
        _tagHelper.Process(tagHelperContext, tagHelperOutput);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(tagHelperOutput.TagName, Is.Null);
            Assert.That(tagHelperOutput.Content.GetContent(), Is.EqualTo("<p class=\"govuk-body-l\">Test</p>"));
        }
    }
    
    [Test]
    public async Task ProcessAsync_DoesNotModifyContent_If_LargerTextIsFalse()
    {
        _tagHelper.LargerText = false;
        
        TagHelperContext tagHelperContext = new([], new Dictionary<object, object>(), "Test");
        TagHelperOutput tagHelperOutput = new("gds-contentful-contents", [],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

        tagHelperOutput.Content.SetHtmlContent("<p class=\"govuk-body\">Test</p>");
        
        await _tagHelper.ProcessAsync(tagHelperContext, tagHelperOutput);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(tagHelperOutput.TagName, Is.Null);
            Assert.That(tagHelperOutput.Content.GetContent(), Is.EqualTo("<p class=\"govuk-body\">Test</p>"));
        }
    }

    [Test]
    public async Task ProcessAsync_ModifiesContent_If_LargerTextIsTrue()
    {
        _tagHelper.LargerText = true;
        
        TagHelperContext tagHelperContext = new([], new Dictionary<object, object>(), "Test");
        TagHelperOutput tagHelperOutput = new("gds-contentful-contents", [],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

        tagHelperOutput.Content.SetHtmlContent("<p class=\"govuk-body\">Test</p>");
        
        await _tagHelper.ProcessAsync(tagHelperContext, tagHelperOutput);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(tagHelperOutput.TagName, Is.Null);
            Assert.That(tagHelperOutput.Content.GetContent(), Is.EqualTo("<p class=\"govuk-body-l\">Test</p>"));
        }
    }
}