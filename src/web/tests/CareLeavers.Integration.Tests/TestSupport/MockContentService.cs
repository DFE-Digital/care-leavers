using CareLeavers.Web;
using CareLeavers.Web.Contentful;
using CareLeavers.Web.Models.Content;
using Newtonsoft.Json;

namespace CareLeavers.Integration.Tests.TestSupport;

public class MockContentService : IContentService
{
    public static string? ResponseJson { get; set; }
    
    public Task<Page?> GetPage(string slug)
    {
        if (string.IsNullOrWhiteSpace(ResponseJson))
        {
            return Task.FromResult<Page?>(null);
        }
        
        using var reader = new JsonTextReader(new StringReader(ResponseJson));
        
        return Task.FromResult(Constants.Serializer.Deserialize<Page>(reader));
    }

    public Task<ContentfulConfigurationEntity?> GetConfiguration()
    {
        return Task.FromResult(new ContentfulConfigurationEntity())!;
    }

    public Task<List<string>> GetSiteSlugs()
    {
        return Task.FromResult(new List<string>
        {
            "home",
            "all-support",
            "guides"
        });
    }
}