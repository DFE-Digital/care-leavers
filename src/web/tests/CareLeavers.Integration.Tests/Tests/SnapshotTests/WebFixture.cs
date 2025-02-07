using CareLeavers.Integration.Tests.TestSupport;

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

    public const string WrapperBasePath = "../../../Tests";
    
    public static HttpClient GetClient() => _webFactory.CreateClient();
}