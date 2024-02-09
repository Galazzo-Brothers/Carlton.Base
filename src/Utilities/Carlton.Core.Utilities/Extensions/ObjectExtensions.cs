namespace Carlton.Core.Utilities.Extensions;

public static class ObjectExtensions
{
    private static bool IsOfType<T>(object value)
    {
        return value is T;
    }
}

