using AngleSharp.Html;
using AngleSharp.Html.Parser;
using CareLeavers.Integration.Tests.TestSupport;

namespace CareLeavers.Integration.Tests.Tests.SnapshotTests;

public class SnapshotTests
{
    public static List<SnapshotTestCase> TestCases { get; set; } =
    [
        new("SimpleParagraph")
        {
            TestName = "Assert Simple Paragraphs"
        },
        new ("SimpleAsset")
        {
            TestName = "Assert Simple Asset"
        }
    ];
    
    [TestCaseSource(nameof(TestCases)), Explicit]
    public async Task GenerateSnapshots(string fileName)
    {
        var resp = await DoTest(fileName);
        
        await File.WriteAllTextAsync($"{WebFixture.WrapperBasePath}/SnapshotTests/Output/{fileName}.html", resp);

        Assert.Pass();
    }

    [TestCaseSource(nameof(TestCases))]
    public async Task AssertSnapshots(string fileName)
    {
       var existing = await File.ReadAllTextAsync($"{WebFixture.WrapperBasePath}/SnapshotTests/Output/{fileName}.html");
       
       var resp = await DoTest(fileName);
       
       Assert.That(resp, Is.EqualTo(existing));
    }

    private async Task<string> DoTest(string fileName)
    {
        var content = await File.ReadAllTextAsync(Path.Combine(WebFixture.WrapperBasePath, "SnapshotTests", "Input", $"{fileName}.json"));
        
        var client = WebFixture.GetClient();
        
        MockContentService.ResponseJson = content;
        
        var response = await client.GetStringAsync("");

        var parser = new HtmlParser();
        var doc = parser.ParseDocument(response);

        await using var sw = new StringWriter();
        doc.ToHtml(sw, new PrettyMarkupFormatter());
        
        return sw.ToString();
    }
}