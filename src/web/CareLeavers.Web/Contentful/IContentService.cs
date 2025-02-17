using CareLeavers.Web.Models.Content;
using Contentful.Core.Models;

namespace CareLeavers.Web.Contentful;

public interface IContentService
{
    Task<Page?> GetPage(string slug);
    
    Task<ContentfulConfigurationEntity?> GetConfiguration();

    Task<List<string>> GetSiteSlugs();

    Task<StatusChecker?> GetStatusChecker(string id);
}