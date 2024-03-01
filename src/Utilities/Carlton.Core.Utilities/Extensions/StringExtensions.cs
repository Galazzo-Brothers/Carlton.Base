using System.Globalization;
using System.Text;
namespace Carlton.Core.Utilities.Extensions;

/// <summary>
/// Provides extension methods for string manipulation.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Converts the specified string to kebab case.
    /// </summary>
    /// <param name="str">The string to convert.</param>
    /// <returns>The string converted to kebab case.</returns>
    public static string ToKebabCase(this string str)
    {
        str = char.ToLowerInvariant(str[0]) + str.Substring(1);
        var newStr = string.Empty;


        foreach(var chr in str)
        {
            if(char.IsUpper(chr))
            {
                newStr += $"-{char.ToLower(chr, CultureInfo.InvariantCulture)}";
            }
            else
            {
                newStr += chr;
            }
        }

        return newStr;
    }

    /// <summary>
    /// Trims trailing comma and space characters from the specified string.
    /// </summary>
    /// <param name="str">The string to trim.</param>
    /// <returns>The string with trailing comma and space characters trimmed.</returns>
    public static string TrimTrailingComma(this string str)
    {
        char[] charsToTrim = [' ', ','];
        return str.TrimEnd(charsToTrim);
    }

    /// <summary>
    /// Adds spaces to camel case string.
    /// </summary>
    /// <param name="input">The input string in camel case.</param>
    /// <returns>The input string with spaces added between camel case words.</returns>
    public static string AddSpacesToCamelCase(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;
        
        var result = new StringBuilder(input.Length * 2); // Create a StringBuilder to build the result.

        // Add the first character as is.
        result.Append(input[0]);

        for (int i = 1; i < input.Length; i++)
        {
            // If the current character is uppercase, add a space before it.
            if (char.IsUpper(input[i]))
            {
                result.Append(' ');
            }

            // Add the current character to the result.
            result.Append(input[i]);
        }

        return result.ToString();
    }
}

