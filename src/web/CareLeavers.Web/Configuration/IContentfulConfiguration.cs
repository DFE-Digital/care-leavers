using CareLeavers.Web.Models.Content;

namespace CareLeavers.Web.Configuration;

public interface IContentfulConfiguration
{
    Task<ContentfulConfigurationEntity> GetConfiguration();
}