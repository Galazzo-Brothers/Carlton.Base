namespace Carlton.Core.Components.Library;

public static class JavaScriptHelper
{
    public const string Import = "import";

    public static string GetImportPath(Type componentType)
    {
        return $"./_content/{GetProjectName(componentType)}/{componentType.Name}/{componentType.Name}.razor.js";
    }

    private static string GetProjectName(Type componentType)
    {
        return componentType.Assembly.GetName().Name;
    }
}
