using CareLeavers.Web.Models.Content;
using Contentful.Core.Models;

namespace CareLeavers.Web.Contentful;

public interface IContentService
{
    Task<Page?> GetPage(string slug);

    Task<Dictionary<string, string>> GetSiteHierarchy();
    
    Task<ContentfulConfigurationEntity?> GetConfiguration();

    Task<Dictionary<string, string>> GetSiteSlugs();

    Task<StatusChecker?> GetStatusChecker(string id);

    Task<RichContentBlock?> Hydrate(RichContentBlock? entity);
    
    Task<RichContent?> Hydrate(RichContent? entity);
    
    Task<Grid?> Hydrate(Grid? entity);

}