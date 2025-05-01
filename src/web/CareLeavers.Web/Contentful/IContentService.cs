using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Models.ViewModels;

namespace CareLeavers.Web.Contentful;

public interface IContentService
{
    Task<RedirectionRules?> GetRedirectionRules(string fromSlug);
    
    Task<Page?> GetPage(string slug);

    Task<List<SimplePage>?> GetSiteHierarchy();
    
    Task<ContentfulConfigurationEntity?> GetConfiguration();

    Task<Dictionary<string, string>> GetSiteSlugs();

    Task<List<SimplePage>> GetBreadcrumbs(string? slug, bool includeHome = true);
    
    Task<T> Hydrate<T>(T entity);
    
    Task<string> GetSlug(string id);

    Task<PrintableCollection?> GetPrintableCollection(string identifier);

    Task<bool> IsPageInPrintableCollection(string slug);
    
    Task<bool> IsInPrintableCollection(string id);

    Task FlushCache();

}