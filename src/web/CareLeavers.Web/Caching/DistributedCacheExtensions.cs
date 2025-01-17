using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CareLeavers.Web.Caching;

public static class DistributedCacheExtensions
{
    private static DistributedCacheEntryOptions DefaultCacheOptions { get; set; } =
        new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(30))
            .SetAbsoluteExpiration(TimeSpan.FromHours(1));
    
    public static Task SetAsync<T>(this IDistributedCache cache, string key, T value)
    {
        return SetAsync(cache, key, value, DefaultCacheOptions);
    }

    public static Task SetAsync<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions options)
    {
        var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value, Constants.SerializerSettings));
        return cache.SetAsync(key, bytes, options);
    }

    public static bool TryGetValue<T>(this IDistributedCache cache, string key, out T? value)
    {
        var val = cache.Get(key);
        value = default;
        
        if (val == null)
        {
            return false;
        }

        using var stream = new MemoryStream(val);
        using var reader = new JsonTextReader(new StreamReader(stream));
        
        value = Constants.Serializer.Deserialize<T>(reader);

        return true;
    }

    public static async Task<T?> GetOrSetAsync<T>(
        this IDistributedCache cache, 
        string key, 
        Func<Task<T>> task, 
        DistributedCacheEntryOptions? options = null)
    {
        if (options == null)
        {
            options = DefaultCacheOptions;
        }
        
        if (cache.TryGetValue(key, out T? value) && value is not null)
        {
            return value;
        }
        
        value = await task();
        
        if (value is not null)
        {
            await cache.SetAsync<T>(key, value, options);
        }
        return value;
    }
}