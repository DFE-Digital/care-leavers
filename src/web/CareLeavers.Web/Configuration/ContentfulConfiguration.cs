using CareLeavers.Web.Contentful;
using CareLeavers.Web.Models.Content;

namespace CareLeavers.Web.Configuration;

public class ContentfulConfiguration(IContentService contentService) : IContentfulConfiguration
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private ContentfulConfigurationEntity? _content;
    
    public async Task<ContentfulConfigurationEntity> GetConfiguration()
    {
        if (_content == null)
        {
            try
            {
                await _semaphore.WaitAsync();
                if (_content == null)
                {
                    var content = await contentService.GetConfiguration();

                    _content = content ?? throw new InvalidOperationException("Contentful configuration not found");
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        return _content;
    }
}