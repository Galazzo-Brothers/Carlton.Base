using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Carlton.Base.Infrastructure.Caching;

public static class DistributedCacheExtensions
{
    public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value)
    {
        await SetAsync(cache, key, value, new DistributedCacheEntryOptions());
    }

    public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions options)
    {
        var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, GetJsonSerializerOptions()));
        await cache.SetAsync(key, bytes, options);
    }

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