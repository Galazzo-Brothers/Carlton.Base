namespace Carlton.Base.Components;

public static class JavaScriptHelper
{
    public const string Import = "import";

    public static string GetImportPath(Type componentType)
    {
        System.Console.WriteLine($"./_content/{GetProjectName(componentType)}/{componentType.Name}/{componentType.Name}.razor.js");
        return $"./_content/{GetProjectName(componentType)}/{componentType.Name}/{componentType.Name}.razor.js";
    }

    private static string GetProjectName(Type componentType)
    {
        return componentType.Assembly.GetName().Name;
    }

}
