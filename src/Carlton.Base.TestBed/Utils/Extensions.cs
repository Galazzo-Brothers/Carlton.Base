namespace Carlton.Base.TestBed;

public static class TypeExtensions
{
    public static string GetDisplayName(this Type type)
    {
        var name = type.Name;
        var index = name.IndexOf("`");
        if(index == -1)
            return name;

        return name[..index];
    }
}


