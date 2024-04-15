using System.Reflection;
namespace Carlton.Core.Utilities.Extensions;

/// <summary>
/// Provides extension methods for working with objects.
/// </summary>
public static class ObjectExtensions
{
	/// <summary>
	/// Determines whether the specified value is of the specified type.
	/// </summary>
	/// <typeparam name="T">The type to check against.</typeparam>
	/// <param name="value">The value to check.</param>
	/// <returns><c>true</c> if the value is of the specified type; otherwise, <c>false</c>.</returns>
	private static bool IsOfType<T>(object value)
	{
		return value is T;
	}

	/// <summary>
	/// Converts the specified object to a dictionary representation, where property names are keys and property values are values.
	/// </summary>
	/// <param name="obj">The object to convert to a dictionary.</param>
	/// <returns>A dictionary representation of the object.</returns>
	public static Dictionary<string, object> ToDictionary(this object obj)
	{
		Dictionary<string, object> dictionary = [];

		if (obj != null)
		{
			PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);

			foreach (PropertyInfo property in properties)
			{
				dictionary.Add(property.Name, property.GetValue(obj));
			}
		}

		return dictionary;
	}
}

