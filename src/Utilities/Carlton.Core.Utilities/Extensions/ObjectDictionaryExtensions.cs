namespace Carlton.Core.Utilities.Extensions;

/// <summary>
/// Provides extension methods for dictionaries to retrieve values by key with a specified type.
/// </summary>
public static class ObjectDictionaryExtensions
{
    /// <summary>
    /// Retrieves the value associated with the specified key and converts it to the specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value to retrieve.</typeparam>
    /// <param name="dictionary">The dictionary to retrieve the value from.</param>
    /// <param name="key">The key of the value to retrieve.</param>
    /// <returns>The value associated with the specified key, converted to type <typeparamref name="T"/>.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the key is not found in the dictionary.</exception>
    public static T GetValue<T>(this IDictionary<string, object> dictionary, string key)
    {
        return (T)dictionary[key];
    }

    /// <summary>
    /// Retrieves the value associated with the specified key and converts it to the specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value to retrieve.</typeparam>
    /// <param name="dictionary">The dictionary to retrieve the value from.</param>
    /// <param name="key">The key of the value to retrieve.</param>
    /// <returns>The value associated with the specified key, converted to type <typeparamref name="T"/>.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the key is not found in the dictionary.</exception>
    public static T GetValue<T>(this IReadOnlyDictionary<string, object> dictionary, string key)
    {
        return (T)dictionary[key];
    }
}
