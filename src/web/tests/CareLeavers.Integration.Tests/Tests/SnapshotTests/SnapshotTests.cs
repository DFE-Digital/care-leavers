using AngleSharp.Html;
using AngleSharp.Html.Parser;
using CareLeavers.Web.Models.Content;

namespace CareLeavers.Integration.Tests.Tests.SnapshotTests;

public class SnapshotTests
{
    public static List<SnapshotTestCase> TestCases { get; set; } =
    [
        new ("SimpleAsset")
        {
            TestName = "Image asset rendered correctly"
        },
        new ("HomePageWithSupport")
        {
            TestName = "Home page from prototype with support section"
        },
        new ("PageWithBanner")
        {
            TestName = "Guide page with rich text, links, and a banner"
        }
    ];
    
    [SetUp]
    public void Setup()
    {
        WebFixture.ClearContent();
    }
    
    [TestCaseSource(nameof(TestCases)), Explicit]
    public async Task GenerateSnapshots(string folder)
    {
        var resp = await DoTest(folder);
        
        await File.WriteAllTextAsync($"{WebFixture.WrapperBasePath}/SnapshotTests/Output/{folder}.html", resp);

        Assert.Pass();
    }

    [TestCaseSource(nameof(TestCases))]
    public async Task AssertSnapshots(string folder)
    {
       var existing = await File.ReadAllTextAsync($"{WebFixture.WrapperBasePath}/SnapshotTests/Output/{folder}.html");
       
       var resp = await DoTest(folder);
       
       Assert.That(resp, Is.EqualTo(existing));
    }

    private async Task<string> DoTest(string folder)
    {
        // Get our files
        var files = Directory
            .GetFiles(Path.Combine(WebFixture.WrapperBasePath, "SnapshotTests", "Input", folder))
            .ToList();
        
        // Setup our slug for the page content
        foreach (var file in files)
        {
            var id = Path.GetFileNameWithoutExtension(file);
            var contentType = string.Empty;

            if (Path.GetFileNameWithoutExtension(file).Contains("_"))
            {
                id = Path.GetFileNameWithoutExtension(file).Split("_", StringSplitOptions.None).Last();
                contentType = Path.GetFileNameWithoutExtension(file).Split("_", StringSplitOptions.None).First();
            }

            WebFixture.AddContent(new ContentfulContent()
            {
                ContentType = contentType,
                Id = id,
                Slug = contentType == Page.ContentType ? folder :
                    contentType == ContentfulConfigurationEntity.ContentType ? "config" : null,
                Content = await FullJson((await File.ReadAllTextAsync(Path.Combine(file))))
            });
        }
        
        WebFixture.AddContent(new ContentfulContent()
        {
            ContentType = RedirectionRules.ContentType,
            Id = "12345",
            Content = await FullJson("")
        });

        var client = WebFixture.GetClient();
        
        var response = await client.GetStringAsync($"");

        return response;
        
        var parser = new HtmlParser();
        var doc = parser.ParseDocument(response);

        await using var sw = new StringWriter();
        doc.ToHtml(sw, new PrettyMarkupFormatter());
        
        return sw.ToString();
    }
    
    private static async Task<string> FullJson(string content)
    {
        var wrapper = await File.ReadAllTextAsync(Path.Combine(WebFixture.WrapperBasePath, "RequestWrapper.json"));
     
        return wrapper.Replace("**REPLACE**", content);
    }
}