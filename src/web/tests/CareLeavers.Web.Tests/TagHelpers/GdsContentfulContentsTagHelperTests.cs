using CareLeavers.Web.ContentfulRenderers;
using CareLeavers.Web.Models.Enums;
using CareLeavers.Web.TagHelpers;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CareLeavers.Web.Tests.TagHelpers;

public class GdsContentfulContentsTagHelperTests
{
    private readonly GdsContentfulContentsTagHelper _tagHelper;

    public GdsContentfulContentsTagHelperTests()
    {
        HtmlRenderer htmlRenderer = new(new HtmlRendererOptions
        {
            ListItemOptions = new ListItemContentRendererOptions
            {
                OmitParagraphTagsInsideListItems = true
            }
        });

        htmlRenderer.AddRenderer(new GdsHeaderRenderer(htmlRenderer.Renderers));

        _tagHelper = new GdsContentfulContentsTagHelper(htmlRenderer);
    }

    [Test]
    public async Task ProcessAsync_Returns_Early_IfLengthIsZero()
    {
        _tagHelper.Levels = [];

        TagHelperContext tagHelperContext = new([], new Dictionary<object, object>(), "Test");
        TagHelperOutput tagHelperOutput = new("gds-contentful-contents", [],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

        await _tagHelper.ProcessAsync(tagHelperContext, tagHelperOutput);

        Assert.That(tagHelperOutput.TagName, Is.EqualTo("gds-contentful-contents"));
        Assert.That(tagHelperOutput.Content.GetContent(), Is.Empty);
    }

    [Test]
    public async Task ProcessAsync_GeneratesNavElement()
    {
        _tagHelper.Levels = [HeadingType.H2];

        _tagHelper.Document = new Document
        {
            Content = [new Heading2 { Content = [new Text { Value = "Section One" }] }]
        };

        TagHelperContext tagHelperContext = new([], new Dictionary<object, object>(), "Test");
        TagHelperOutput tagHelperOutput = new("gds-contentful-contents", [],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

        await _tagHelper.ProcessAsync(tagHelperContext, tagHelperOutput);

        string result = tagHelperOutput.Content.GetContent();

        Assert.That(tagHelperOutput.TagName, Is.Empty);
        Assert.That(result, Does.Contain("<nav id=\"main-content-contents\">"));
        Assert.That(result, Does.Contain("<h2 class=\"govuk-heading-m gem-c-contents-list__title\">On this page</h2>"));
        Assert.That(result, Does.Contain("<ol class=\"gem-c-contents-list__list\">"));
        Assert.That(result,
            Does.Contain("<li class=\"gem-c-contents-list__list-item gem-c-contents-list__list-item--dashed\">"));
        Assert.That(result,
            Does.Contain("<span class=\"gem-c-contents-list__list-item-dash\" aria-hidden=\"true\"></span>"));
        Assert.That(result, Does.Contain("<a href=\"#Section-One\" class=\"govuk-link\">Section One</a>"));
    }
}