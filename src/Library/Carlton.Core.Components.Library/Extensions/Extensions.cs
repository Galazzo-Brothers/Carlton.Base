using System.Text;

namespace Carlton.Core.Components.Library;

public static class StringExtensions
{
    public static string ToKebabCase(this string str)
    {
        str = char.ToLowerInvariant(str[0]) + str[1..];
        var newStr = new StringBuilder();


        foreach(var chr in str)
        {
            if(char.IsUpper(chr))
            {
                newStr.Append($"-{char.ToLower(chr, CultureInfo.InvariantCulture)}");
            }
            else
            {
                newStr.Append(chr);
            }
        }

        return newStr.ToString();
    }

    public static string ToDisplayFormat(this string str)
    {
        return string.Concat(str.Select(_ => char.IsUpper(_) ? $" {_}" : _.ToString())).Trim();
    }

    public static string RemoveTypeParamCharacters(this string str)
    {
        if(str.IndexOf("`") < 0)
            return str;

        return str[..str.IndexOf('`')];
    }
}

public static class EnumerableExtensions
{
    public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
    {
        return source.Select((item, index) => (item, index));
    }
}
