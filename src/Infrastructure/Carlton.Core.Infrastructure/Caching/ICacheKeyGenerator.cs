namespace Carlton.Core.Infrastructure.Caching;

public interface ICacheKeyGenerator
{
    string GenerateCacheKey(object obj);
}
