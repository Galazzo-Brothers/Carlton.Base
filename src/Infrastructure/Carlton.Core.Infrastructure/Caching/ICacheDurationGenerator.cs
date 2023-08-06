namespace Carlton.Core.Infrastructure.Caching;

public interface ICacheDurationGenerator
{
    TimeSpan GetCacheDuration(object obj);
}
