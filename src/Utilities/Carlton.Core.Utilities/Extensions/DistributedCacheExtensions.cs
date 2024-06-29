using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Distributed;
namespace Carlton.Core.Utilities.DistributedCacheExtensions;

/// <summary>
/// Provides extension methods for working with IDistributedCache.
/// </summary>
public static class DistributedCacheExtensions
{
    /// <summary>
    /// Asynchronously sets the value of the specified key in the distributed cache.
    /// </summary>
    /// <typeparam name="T">The type of the value to be stored in the cache.</typeparam>
    /// <param name="cache">The IDistributedCache instance.</param>
    /// <param name="key">The cache key.</param>
    /// <param name="value">The value to be stored in the cache.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value)
    {
        await SetAsync(cache, key, value, new DistributedCacheEntryOptions());
    }

    /// <summary>
    /// Asynchronously sets the value of the specified key in the distributed cache with the specified options.
    /// </summary>
    /// <typeparam name="T">The type of the value to be stored in the cache.</typeparam>
    /// <param name="cache">The IDistributedCache instance.</param>
    /// <param name="key">The cache key.</param>
    /// <param name="value">The value to be stored in the cache.</param>
    /// <param name="options">The cache entry options.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions options)
    {
        var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, GetJsonSerializerOptions()));
        await cache.SetAsync(key, bytes, options);
    }

    /// <summary>
    /// Asynchronously retrieves the value associated with the specified key from the distributed cache.
    /// </summary>
    /// <typeparam name="T">The type of the value to retrieve from the cache.</typeparam>
    /// <param name="cache">The IDistributedCache instance.</param>
    /// <param name="key">The cache key.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the retrieved value from the cache.</returns>
    public static async Task<T> GetValueAsync<T>(this IDistributedCache cache, string key)
    {
        var val = await cache.GetAsync(key);
        T value = JsonSerializer.Deserialize<T>(val, GetJsonSerializerOptions());
        return value;
    }

    private static JsonSerializerOptions GetJsonSerializerOptions()
    {
        return new JsonSerializerOptions()
        {
            PropertyNamingPolicy = null,
            WriteIndented = true,
            AllowTrailingCommas = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };
    }
}