using System.Net;
using System.Xml;

namespace CareLeavers.Integration.Tests.Tests;

public class ContentfulControllerTests
{
    [SetUp]
    public void Setup()
    {
        WebFixture.ClearContent();
    }
    
    [Test]
    public async Task SitemapIsGenerated()
    {
        // Arrange
        var client = WebFixture.GetClient();
        var wrapper = await File.ReadAllTextAsync(Path.Combine(WebFixture.WrapperBasePath, "RequestWrapper.json"));

        wrapper = wrapper.Replace("**REPLACE**", @"{
    ""slug"" : ""home"",
    ""sys"" : { ""id"" : ""12345"" } 
  }, {
    ""slug"" : ""about"",
    ""sys"" : { ""id"" : ""12346"" } 
  }");

        WebFixture.AddContent(new ContentfulContent() { Content = wrapper });
        
        // Act
        var response = await client.GetAsync("/sitemap.xml");
        
        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        
        var xml = new XmlDocument();
        xml.LoadXml(content);
        
        var urls = xml.GetElementsByTagName("loc");
        
        Assert.That(urls.Count, Is.EqualTo(4));
        Assert.Multiple(() =>
        {
            Assert.That(urls[0]?.InnerText, Is.EqualTo("https://localhost/en/home"));
            Assert.That(urls[1]?.InnerText, Is.EqualTo("https://localhost/en/about"));
            Assert.That(urls[2]?.InnerText, Is.EqualTo("https://localhost/en/cookie-policy"));
            Assert.That(urls[3]?.InnerText, Is.EqualTo("https://localhost/en/privacy-policies"));
        });
    }

    [Test]
    public async Task NoContentInContentfulReturnsNotFound()
    {
        // Arrange
        var client = WebFixture.GetClient();
        var wrapper = await File.ReadAllTextAsync(Path.Combine(WebFixture.WrapperBasePath, "RequestWrapper.json"));
        wrapper = wrapper.Replace("**REPLACE**", @"{
    ""slug"" : ""home"",
    ""sys"" : { ""id"" : ""12345"" } 
  }, {
    ""slug"" : ""about"",
    ""sys"" : { ""id"" : ""12346"" } 
  }");
        
        WebFixture.AddContent(new ContentfulContent() { Content = wrapper });
        
        // Act
        var response = await client.GetAsync("/en/not-found-random-page");
        
        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}