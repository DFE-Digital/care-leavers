using CareLeavers.Web.Configuration;
using CareLeavers.Web.ContentfulRenderers;
using CareLeavers.Web.TagHelpers;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NSubstitute;

namespace CareLeavers.Web.Tests.TagHelpers;

public class GdsContentfulRichTextTagHelperTests
{
    private IHttpContextAccessor _httpContextAccessor;
    private HttpContext _httpContext;
    
    private GdsContentfulRichTextTagHelper _tagHelper;

    [SetUp]
    public void Init()
    {
        HtmlRenderer htmlRenderer = new(new HtmlRendererOptions
        {
            ListItemOptions = new ListItemContentRendererOptions
            {
                OmitParagraphTagsInsideListItems = true
            }
        });

        htmlRenderer.AddRenderer(new GdsHeaderRenderer(htmlRenderer.Renderers));

        _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
        _httpContext = new DefaultHttpContext();
        _httpContext.Session = new MockSession();
        _httpContextAccessor.HttpContext = _httpContext;

        _tagHelper = new GdsContentfulRichTextTagHelper(htmlRenderer, _httpContextAccessor);
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

    [Test]
    public void Process_DoesNotModifyContent_If_DynamicTimeIsFalse()
    {
        _tagHelper.DynamicTime = false;
        
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
    public void Process_ModifiesContent_ReplacesWildcard_If_DynamicTimeIsTrue()
    {
        _httpContext.Session.SetString(FairUsageOptions.AzureTranslationTimeoutKey, "1:28pm");
        
        _tagHelper.DynamicTime = true;
        
        TagHelperContext tagHelperContext = new([], new Dictionary<object, object>(), "Test");
        TagHelperOutput tagHelperOutput = new("gds-contentful-contents", [],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

        tagHelperOutput.Content.SetHtmlContent("<p class=\"govuk-body\">Test ((time))</p>");
        
        _tagHelper.Process(tagHelperContext, tagHelperOutput);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(tagHelperOutput.TagName, Is.Null);
            Assert.That(tagHelperOutput.Content.GetContent(), Is.EqualTo($"<p class=\"govuk-body\">Test 1:28pm</p>"));
        }
    }

    [Test]
    public void Process_ModifiesContent_UsesFallback_If_DynamicTimeIsTrue_And_SessionIsNull()
    {
        _httpContextAccessor.HttpContext = null;
        
        _tagHelper.DynamicTime = true;
        
        TagHelperContext tagHelperContext = new([], new Dictionary<object, object>(), "Test");
        TagHelperOutput tagHelperOutput = new("gds-contentful-contents", [],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

        tagHelperOutput.Content.SetHtmlContent("<p class=\"govuk-body\">Test ((time))</p>");
        
        _tagHelper.Process(tagHelperContext, tagHelperOutput);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(tagHelperOutput.TagName, Is.Null);
            Assert.That(tagHelperOutput.Content.GetContent(), Does.Contain("<p class=\"govuk-body\">You will be able to translate into a new language in 24 hours.</p>"));
            Assert.That(tagHelperOutput.Content.GetContent(), Does.Contain("<p class=\"govuk-body\">You can <a class=\"govuk-link\" href=\"/translate-this-website/home\">continue to translate the site</a> into a language you have selected already.</p>"));
        }
    }
    
    [Test]
    public void Process_ModifiesContent_UsesFallback_If_DynamicTimeIsTrue_And_TimeoutIsNull()
    {
        _tagHelper.DynamicTime = true;
        
        TagHelperContext tagHelperContext = new([], new Dictionary<object, object>(), "Test");
        TagHelperOutput tagHelperOutput = new("gds-contentful-contents", [],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

        tagHelperOutput.Content.SetHtmlContent("<p class=\"govuk-body\">Test ((time))</p>");
        
        _tagHelper.Process(tagHelperContext, tagHelperOutput);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(tagHelperOutput.TagName, Is.Null);
            Assert.That(tagHelperOutput.Content.GetContent(), Does.Contain("<p class=\"govuk-body\">You will be able to translate into a new language in 24 hours.</p>"));
            Assert.That(tagHelperOutput.Content.GetContent(), Does.Contain("<p class=\"govuk-body\">You can <a class=\"govuk-link\" href=\"/translate-this-website/home\">continue to translate the site</a> into a language you have selected already.</p>"));
        }
    }
    
    [Test]
    public void Process_ModifiesContent_UsesFallback_If_DynamicTimeIsTrue_And_ContentDoesNotContainWildcard()
    {
        _httpContext.Session.SetString(FairUsageOptions.AzureTranslationTimeoutKey, "1:28pm");
        
        _tagHelper.DynamicTime = true;
        
        TagHelperContext tagHelperContext = new([], new Dictionary<object, object>(), "Test");
        TagHelperOutput tagHelperOutput = new("gds-contentful-contents", [],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

        tagHelperOutput.Content.SetHtmlContent("<p class=\"govuk-body\">Test</p>");
        
        _tagHelper.Process(tagHelperContext, tagHelperOutput);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(tagHelperOutput.TagName, Is.Null);
            Assert.That(tagHelperOutput.Content.GetContent(), Does.Contain("<p class=\"govuk-body\">You will be able to translate into a new language in 24 hours.</p>"));
            Assert.That(tagHelperOutput.Content.GetContent(), Does.Contain("<p class=\"govuk-body\">You can <a class=\"govuk-link\" href=\"/translate-this-website/home\">continue to translate the site</a> into a language you have selected already.</p>"));
        }
    }
    
        [Test]
    public async Task ProcessAsync_DoesNotModifyContent_If_DynamicTimeIsFalse()
    {
        _tagHelper.DynamicTime = false;
        
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
    public async Task ProcessAsync_ModifiesContent_ReplacesWildcard_If_DynamicTimeIsTrue()
    {
        _httpContext.Session.SetString(FairUsageOptions.AzureTranslationTimeoutKey, "1:28pm");
        
        _tagHelper.DynamicTime = true;
        
        TagHelperContext tagHelperContext = new([], new Dictionary<object, object>(), "Test");
        TagHelperOutput tagHelperOutput = new("gds-contentful-contents", [],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

        tagHelperOutput.Content.SetHtmlContent("<p class=\"govuk-body\">Test ((time))</p>");
        
        await _tagHelper.ProcessAsync(tagHelperContext, tagHelperOutput);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(tagHelperOutput.TagName, Is.Null);
            Assert.That(tagHelperOutput.Content.GetContent(), Is.EqualTo($"<p class=\"govuk-body\">Test 1:28pm</p>"));
        }
    }

    [Test]
    public async Task ProcessAsync_ModifiesContent_UsesFallback_If_DynamicTimeIsTrue_And_SessionIsNull()
    {
        _httpContextAccessor.HttpContext = null;
        
        _tagHelper.DynamicTime = true;
        
        TagHelperContext tagHelperContext = new([], new Dictionary<object, object>(), "Test");
        TagHelperOutput tagHelperOutput = new("gds-contentful-contents", [],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

        tagHelperOutput.Content.SetHtmlContent("<p class=\"govuk-body\">Test ((time))</p>");
        
        await _tagHelper.ProcessAsync(tagHelperContext, tagHelperOutput);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(tagHelperOutput.TagName, Is.Null);
            Assert.That(tagHelperOutput.Content.GetContent(), Does.Contain("<p class=\"govuk-body\">You will be able to translate into a new language in 24 hours.</p>"));
            Assert.That(tagHelperOutput.Content.GetContent(), Does.Contain("<p class=\"govuk-body\">You can <a class=\"govuk-link\" href=\"/translate-this-website/home\">continue to translate the site</a> into a language you have selected already.</p>"));
        }
    }
    
    [Test]
    public async Task ProcessAsync_ModifiesContent_UsesFallback_If_DynamicTimeIsTrue_And_TimeoutIsNull()
    {
        _tagHelper.DynamicTime = true;
        
        TagHelperContext tagHelperContext = new([], new Dictionary<object, object>(), "Test");
        TagHelperOutput tagHelperOutput = new("gds-contentful-contents", [],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

        tagHelperOutput.Content.SetHtmlContent("<p class=\"govuk-body\">Test ((time))</p>");
        
        await _tagHelper.ProcessAsync(tagHelperContext, tagHelperOutput);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(tagHelperOutput.TagName, Is.Null);
            Assert.That(tagHelperOutput.Content.GetContent(), Does.Contain("<p class=\"govuk-body\">You will be able to translate into a new language in 24 hours.</p>"));
            Assert.That(tagHelperOutput.Content.GetContent(), Does.Contain("<p class=\"govuk-body\">You can <a class=\"govuk-link\" href=\"/translate-this-website/home\">continue to translate the site</a> into a language you have selected already.</p>"));
        }
    }
    
    [Test]
    public async Task ProcessAsync_ModifiesContent_UsesFallback_If_DynamicTimeIsTrue_And_ContentDoesNotContainWildcard()
    {
        _httpContext.Session.SetString(FairUsageOptions.AzureTranslationTimeoutKey, "1:28pm");
        
        _tagHelper.DynamicTime = true;
        
        TagHelperContext tagHelperContext = new([], new Dictionary<object, object>(), "Test");
        TagHelperOutput tagHelperOutput = new("gds-contentful-contents", [],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

        tagHelperOutput.Content.SetHtmlContent("<p class=\"govuk-body\">Test</p>");
        
        await _tagHelper.ProcessAsync(tagHelperContext, tagHelperOutput);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(tagHelperOutput.TagName, Is.Null);
            Assert.That(tagHelperOutput.Content.GetContent(), Does.Contain("<p class=\"govuk-body\">You will be able to translate into a new language in 24 hours.</p>"));
            Assert.That(tagHelperOutput.Content.GetContent(), Does.Contain("<p class=\"govuk-body\">You can <a class=\"govuk-link\" href=\"/translate-this-website/home\">continue to translate the site</a> into a language you have selected already.</p>"));
        }
    }
}