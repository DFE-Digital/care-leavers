using CareLeavers.Web.Configuration;
using CareLeavers.Web.Models.Content;
using Contentful.Core.Models;

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
                Slug = "home",
                Title = "Care Leavers"
            },
            ServiceName = "Care leavers",
            TranslationEnabled = true,
            Footer = new Document
            {
                Content =
                [
                    new Paragraph()
                    {
                        Content =
                        [
                            new Text()
                            {
                                Value = "Care leavers"
                            }
                        ]
                    }
                ]
            },
            DfELogoAltText = "Department for Education"
        });
    }
}