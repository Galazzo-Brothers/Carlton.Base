namespace Carlton.Base.Components.TestBed;

internal static class TitleBreadCrumbsTestStates
{
    public static Dictionary<string, object> DefaultState
    {
        get => new()
        {
            { "Title", "Test Page"},
            { "BreadCrumbs", "Home > Test" }
        };
    }
}
