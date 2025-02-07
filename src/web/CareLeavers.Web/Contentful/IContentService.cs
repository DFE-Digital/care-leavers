using CareLeavers.Web.Models.Content;

namespace CareLeavers.Web.Contentful;

public interface IContentService
{
    Task<Page?> GetPage(string slug);
    
    Task<ContentfulConfigurationEntity?> GetConfiguration();

    Task<List<string>> GetSiteSlugs();
}