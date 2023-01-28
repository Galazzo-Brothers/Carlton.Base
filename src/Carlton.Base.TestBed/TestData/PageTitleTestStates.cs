namespace Carlton.Base.Components.TestData;

public static class PageTitleTestStates
{
    public static Dictionary<string, object> DefaultState()
    {
        return new Dictionary<string, object>
        {
            { "Title", "Test Page"},
            { "BreadCrumbs", "Home > Test" }
        };
    }
}
