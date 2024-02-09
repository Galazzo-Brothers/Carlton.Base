namespace Carlton.Core.Utilities.Extensions;

public static class StringExtensions
{
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

    public static string TrimTrailingComma(this string str)
    {
        char[] charsToTrim = [' ', ','];
        return str.TrimEnd(charsToTrim);
    }

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

