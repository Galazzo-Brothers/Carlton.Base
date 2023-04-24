namespace Carlton.Base.TestBed;

public static class JavaScriptHelper
{
    public const string Import = "import";

    public static string GetProjectName()
    {
        return Assembly.GetExecutingAssembly().GetName().Name;
    }

    public static string GetImportPath(string componentName)
    {
        return $"./_content/{GetProjectName()}/Components/{componentName}/{componentName}.razor.js";
    }
}
