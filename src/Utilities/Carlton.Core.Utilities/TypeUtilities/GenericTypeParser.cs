using System.Text.RegularExpressions;

namespace Carlton.Core.Utilities.TypeUtilities;

public static class GenericTypeParser
{
    public static Type ParseGenericType(string typeName)
    {
        // Use regular expression to extract the generic type name and type arguments.
        var regex = new Regex(@"^(.+)\`(\d+)\[(.+)\]$");
        Match match = regex.Match(typeName);

        if (!match.Success)
        {
            throw new ArgumentException("Invalid type name format");
        }

        // Extract the generic type name, number of type arguments, and type argument list.
        string genericTypeName = match.Groups[1].Value;
        int typeArgumentCount = int.Parse(match.Groups[2].Value);
        string typeArgumentList = match.Groups[3].Value;

        // Split the type argument list into individual type arguments.
        string[] typeArguments = typeArgumentList.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        // Build the full type name, including assembly information if necessary.
        string fullTypeName = $"{genericTypeName}`{typeArgumentCount}[{string.Join(",", typeArguments)}]";

        // Use reflection to obtain the Type object.
        Type type = Type.GetType(fullTypeName);

        return type;
    }
}
