using System.Collections.Concurrent;
using System.Collections.ObjectModel;

namespace Carlton.Core.Utilities.Extensions;

public static class CollectionExtensions
{
    public static IReadOnlyCollection<T> AsReadOnly<T>(this ConcurrentBag<T> concurrentBag)
    {
        return concurrentBag == null
            ? throw new ArgumentNullException(nameof(concurrentBag))
            : (IReadOnlyCollection<T>)new ReadOnlyCollection<T>(concurrentBag.ToList());
    }

    public static T GetValue<T>(this IDictionary<string, object> dictionary, string key)
    {
        return (T) dictionary[key];
    }
}
