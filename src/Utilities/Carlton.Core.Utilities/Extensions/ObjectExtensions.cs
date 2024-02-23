namespace Carlton.Core.Utilities.Extensions;

public static class ObjectExtensions
{
    private static bool IsOfType<T>(object value)
    {
        return value is T;
    }

    public static Dictionary<string, object> ConvertToDictionary(this object obj)
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

