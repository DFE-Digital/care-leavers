using CareLeavers.Web.Configuration;
using CareLeavers.Web.Models.Content;

namespace CareLeavers.Integration.Tests.TestSupport;

public class MockContentfulConfiguration : IContentfulConfiguration
{
    public Task<ContentfulConfigurationEntity> GetConfiguration()
    {
        return Task.FromResult(new ContentfulConfigurationEntity
        {
            Phase = ContentfulConfigurationEntity.BannerPhase.Beta,
            HomePage = new Page
            {
                Slug = "home"
            },
            ServiceName = "Care leavers"
        });
    }
}