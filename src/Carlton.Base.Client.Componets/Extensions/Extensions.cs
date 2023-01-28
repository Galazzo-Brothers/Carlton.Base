namespace Carlton.Base.Components;

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
