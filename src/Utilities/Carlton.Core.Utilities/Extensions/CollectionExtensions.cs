namespace Carlton.Core.Utilities.Extensions;

public static class CollectionExtensions
{
    public static T GetValue<T>(this IDictionary<string, object> dictionary, string key)
    {
        return (T) dictionary[key];
    }

    public static T GetValue<T>(this IReadOnlyDictionary<string, object> dictionary, string key)
    {
        return (T)dictionary[key];
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

    public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
    {
        return source.Select((item, index) => (item, index));
    }
}
