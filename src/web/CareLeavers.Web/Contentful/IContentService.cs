using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Models.ViewModels;

namespace CareLeavers.Web.Contentful;

public interface IContentService
{
    Task<Page?> GetPage(string slug);

    Task<List<SimplePage>?> GetSiteHierarchy();
    
    Task<ContentfulConfigurationEntity?> GetConfiguration();

    Task<Dictionary<string, string>> GetSiteSlugs();

    Task<List<SimplePage>> GetBreadcrumbs(string slug, bool includeHome = true);
    
    Task<RichContentBlock?> Hydrate(RichContentBlock? entity);
    
    Task<Grid?> Hydrate(Grid? entity);
    
    Task<Banner?> Hydrate(Banner? entity);
    
    Task<StatusChecker?> Hydrate(StatusChecker? entity);

    Task<string> GetSlug(string id);

}