using CareLeavers.Web.Configuration;
using CareLeavers.Web.Models.Content;

namespace CareLeavers.Web.Tests;

public class MockContentfulConfiguration : IContentfulConfiguration
{
    public Task<ContentfulConfigurationEntity> GetConfiguration()
    {
        return Task.FromResult(new ContentfulConfigurationEntity
        {
            Phase = ContentfulConfigurationEntity.BannerPhase.Beta,
            HomePage = new PageLink
            {
                Slug = "home"
            },
            ServiceName = "Care leavers",
            TranslationEnabled = true
        });
    }
}