using System.Net;
using System.Xml;
using CareLeavers.Integration.Tests.TestSupport;

namespace CareLeavers.Integration.Tests.Tests;

public class ContentfulControllerTests
{
    [Test]
    public async Task SitemapIsGenerated()
    {
        // Arrange
        var client = WebFixture.GetClient();
        var wrapper = await File.ReadAllTextAsync(Path.Combine(WebFixture.WrapperBasePath, "RequestWrapper.json"));

        wrapper = wrapper.Replace("**REPLACE**", @"{
    ""slug"" : ""home""
  }, {
    ""slug"" : ""about""
  }");
        
        WebFixture.SetContentfulJson(wrapper);
        
        // Act
        var response = await client.GetAsync("/sitemap.xml");
        
        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        
        var xml = new XmlDocument();
        xml.LoadXml(content);
        
        var urls = xml.GetElementsByTagName("loc");
        
        Assert.That(urls.Count, Is.EqualTo(2));
        Assert.Multiple(() =>
        {
            Assert.That(urls[0]?.InnerText, Is.EqualTo("/home"));
            Assert.That(urls[1]?.InnerText, Is.EqualTo("/about"));
        });
    }

    [Test]
    public async Task NoContentInContentfulReturnsNotFound()
    {
        // Arrange
        var client = WebFixture.GetClient();
        var wrapper = await File.ReadAllTextAsync(Path.Combine(WebFixture.WrapperBasePath, "RequestWrapper.json"));
        wrapper = wrapper.Replace("**REPLACE**", string.Empty);

        WebFixture.SetContentfulJson(wrapper);
        
        // Act
        var response = await client.GetAsync("/home");
        
        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}