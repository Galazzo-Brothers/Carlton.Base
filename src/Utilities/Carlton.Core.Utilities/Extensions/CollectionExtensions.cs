namespace Carlton.Core.Utilities.Extensions;

/// <summary>
/// Provides extension methods for collections and enumerable types.
/// </summary>
public static class CollectionExtensions
{
	/// <summary>
	/// Safely retrieves the element at the specified index from the list if it exists, otherwise returns the default value for type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The type of elements in the list.</typeparam>
	/// <param name="list">The list to retrieve the element from.</param>
	/// <param name="index">The index of the element to retrieve.</param>
	/// <returns>The element at the specified index if it exists; otherwise, the default value for type <typeparamref name="T"/>.</returns>
	public static T SafeGetAtIndex<T>(this IList<T> list, int index)
	{
		if (index >= 0 && index < list.Count)
			return list[index];

		return default;
	}

	/// <summary>
	/// Safely retrieves the element at the specified index from the enumerable if it exists, otherwise returns the default value for type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The type of elements in the enumerable.</typeparam>
	/// <param name="enumerable">The enumerable to retrieve the element from.</param>
	/// <param name="index">The index of the element to retrieve.</param>
	/// <returns>The element at the specified index if it exists; otherwise, the default value for type <typeparamref name="T"/>.</returns>
	public static T SafeGetAtIndex<T>(this IEnumerable<T> enumerable, int index)
	{
		if (index >= 0 && index < enumerable.Count())
			return enumerable.ElementAt(index);

		return default;
	}

	/// <summary>
	/// Enumerates the source sequence and yields tuples containing each element paired with its index in the sequence.
	/// </summary>
	/// <typeparam name="T">The type of elements in the source sequence.</typeparam>
	/// <param name="source">The source sequence to enumerate.</param>
	/// <returns>An enumerable collection of tuples containing each element along with its index in the sequence.</returns>
	public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
	{
		return source.Select((item, index) => (item, index));
	}

	/// <summary>
	/// Finds the index of the first occurrence of a specified item in the sequence.
	/// </summary>
	/// <typeparam name="T">The type of elements in the sequence.</typeparam>
	/// <param name="source">The sequence to search.</param>
	/// <param name="item">The item to locate in the sequence.</param>
	/// <returns>The zero-based index of the first occurrence of the specified item in the sequence, if found; otherwise, -1.</returns>
	public static int FindIndex<T>(this IEnumerable<T> source, T item)
	{
		return FindIndex(source, o => o.Equals(item));
	}

	/// <summary>
	/// Finds the index of the first element in the sequence that satisfies a specified condition.
	/// </summary>
	/// <typeparam name="T">The type of elements in the sequence.</typeparam>
	/// <param name="source">The sequence to search.</param>
	/// <param name="predicate">A function to test each element for a condition.</param>
	/// <returns>The zero-based index of the first element in the sequence that satisfies the condition, if found; otherwise, -1.</returns>
	public static int FindIndex<T>(this IEnumerable<T> source, Func<T, bool> predicate)
	{
		if (source is List<T> list)
		{
			// If the source is an IList<T>, use its efficient FindIndex method directly
			return list.FindIndex(new Predicate<T>(predicate));
		}

		// Otherwise, iterate through the sequence manually
		int index = 0;
		foreach (var item in source)
		{
			if (predicate(item))
			{
				return index;
			}
			index++;
		}
		return -1; // Not found
	}
}
