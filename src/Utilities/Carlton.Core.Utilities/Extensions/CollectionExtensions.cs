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

    public static T SafeGetAtIndex<T>(this IList<T> list, int index)
    {
        if (index >= 0 && index < list.Count)
            return list[index];

        return default;
    }

    public static T SafeGetAtIndex<T>(this IEnumerable<T> enumerable, int index)
    {
        if (index >= 0 && index < enumerable.Count())
            return enumerable.ElementAt(index);

        return default;
    }
}
