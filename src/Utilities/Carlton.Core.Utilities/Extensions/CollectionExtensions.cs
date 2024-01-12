using System.Collections.Concurrent;
using System.Collections.ObjectModel;

namespace Carlton.Core.Utilities.Extensions;

public static class CollectionExtensions
{
    public static IReadOnlyCollection<T> AsReadOnly<T>(this ConcurrentBag<T> concurrentBag)
    {
        if (concurrentBag == null)
            throw new ArgumentNullException(nameof(concurrentBag));

        return new ReadOnlyCollection<T>(concurrentBag.ToList());
    }
}
