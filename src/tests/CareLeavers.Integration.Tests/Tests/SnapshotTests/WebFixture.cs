using CareLeavers.Integration.Tests.TestSupport;
using CareLeavers.Web.Models.Content;
using Contentful.Core.Models;
using Contentful.Core.Search;
using Newtonsoft.Json;
using NSubstitute;

namespace CareLeavers.Integration.Tests.Tests.SnapshotTests;

[SetUpFixture]
public class WebFixture
{
    private static IntegrationTestWebFactory _webFactory = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        _webFactory = new IntegrationTestWebFactory();
    }

    [OneTimeTearDown]
    public void Teardown()
    {
        _webFactory.Dispose();
    }

    public static void SetContentfulJson(string content)
    {
        _webFactory.FakeContentfulHttpClient.Response = content;
    }
    
    public static HttpClient GetClient() => _webFactory.CreateClient();
}