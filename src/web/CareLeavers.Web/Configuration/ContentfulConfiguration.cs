using CareLeavers.Web.Caching;
using CareLeavers.Web.Models.Content;
using Contentful.Core;
using Contentful.Core.Search;
using Microsoft.Extensions.Caching.Distributed;

namespace CareLeavers.Web.Configuration;

public class ContentfulConfiguration(
    IDistributedCache distributedCache, 
    IContentfulClient contentfulClient) : IContentfulConfiguration
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
                    var content = await distributedCache.GetOrSetAsync("contentful:configuration", async () =>
                    {
                        var config = new QueryBuilder<ContentfulConfigurationEntity>()
                            .ContentTypeIs(ContentfulConfigurationEntity.ContentType)
                            .Include(5)
                            .Limit(1);

                        var configEntries = await contentfulClient.GetEntries(config);

                        return configEntries.FirstOrDefault();
                    });

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