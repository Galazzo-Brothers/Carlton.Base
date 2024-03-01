namespace Carlton.Core.Utilities.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="Type"/> objects.
/// </summary>
public static class TypeExtensions
{

    /// <summary>
    /// Gets the display name of the specified type, excluding generic type arguments.
    /// </summary>
    /// <param name="type">The type for which to get the display name.</param>
    /// <returns>The display name of the type, excluding generic type arguments if any.</returns>
    public static string GetDisplayName(this Type type)
    {
        var typeName = type.Name;
        var index = typeName.IndexOf('`');
        if (index > -1)
            return typeName[..index];


        return typeName;
    }


    /// <summary>
    /// Gets the display name of the specified type, including generic type arguments.
    /// </summary>
    /// <param name="type">The type for which to get the display name.</param>
    /// <returns>The display name of the type, including generic type arguments if any.</returns>
    public static string GetDisplayNameWithGenerics(this Type type)
    {
        if (!type.IsGenericType)
            return type.Name;

        var typeName = type.Name;
        var genericTypeName = typeName[..typeName.IndexOf('`')];
        var genericArguments = type.GetGenericArguments().Select(arg => arg.GetDisplayName());
        var genericArgumentsString = string.Join(", ", genericArguments);

       
        return $"{genericTypeName}<{genericArgumentsString}>";
    }
}


