using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Serilog;
using StackExchange.Redis;

namespace CareLeavers.Web.Caching;

public static class DistributedCacheExtensions
{
    public static DistributedCacheEntryOptions DefaultCacheOptions { get; set; } =
        new DistributedCacheEntryOptions()
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
        try
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
        catch (Exception ex)
        {
            // If our error isn't a redis one, throw it
            if (ex is not (RedisConnectionException or RedisCommandException)) throw;
            
            // Otherwise, let's log that we can't connect and run the task direct
            // TODO: Log to App Insights?
            Log.Logger.Error(ex, "Redis Error");

            // Go and fetch directly from source
            return await task();;

        }

        
    }
}