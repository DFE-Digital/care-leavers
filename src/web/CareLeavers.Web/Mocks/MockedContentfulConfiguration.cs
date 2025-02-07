using CareLeavers.Web.Configuration;
using CareLeavers.Web.Models.Content;

namespace CareLeavers.Web.Mocks;

public class MockedContentfulConfiguration : IContentfulConfiguration
{
    public Task<ContentfulConfigurationEntity> GetConfiguration()
    {
        return Task.FromResult(new ContentfulConfigurationEntity
        {
            ServiceName = "E2E Tests Support for Care Leavers",
            Phase = ContentfulConfigurationEntity.BannerPhase.Beta,
            HomePage = new Page
            {
                Slug = "home",
            },
            Navigation =
            [
                new NavigationElement
                {
                    Title = "Home",
                    Link = new Page
                    {
                        Slug = "home"
                    }
                },
                new NavigationElement
                {
                    Title = "All Support",
                    Link = new Page
                    {
                        Slug = "all-support"
                    }
                }
            ]
        });
    }
}