using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Models.ViewModels;
using Contentful.Core.Models;

namespace CareLeavers.Web.Contentful;

public interface IContentService
{
    Task<Page?> GetPage(string slug);

    Task<List<SimplePage>?> GetSiteHierarchy();
    
    Task<ContentfulConfigurationEntity?> GetConfiguration();

    Task<Dictionary<string, string>> GetSiteSlugs();

    Task<List<SimplePage>> GetBreadcrumbs(string slug, bool includeHome = true);

    Task<StatusChecker?> GetStatusChecker(string id);

    Task<RichContentBlock?> Hydrate(RichContentBlock? entity);
    
    Task<Grid?> Hydrate(Grid? entity);
    
    Task<Banner?> Hydrate(Banner? entity);

    Task<string> GetSlug(string id);

}