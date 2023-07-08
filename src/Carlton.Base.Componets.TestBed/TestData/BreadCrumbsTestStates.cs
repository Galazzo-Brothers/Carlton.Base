namespace Carlton.Base.Components.TestBed;

internal static class BreadCrumbsTestStates
{
    public static object CarrotSingleCrumb
    {
        get => new
        {
            Title = "Home Page",
            Separator = ">",
            BreadCrumbItems = new List<string> { "Home" }
        };
    }

    public static object CarrotMultiCrumb
    {
        get => new
        {
            Title = "Test Page",
            Separator = ">",
            BreadCrumbItems = new List<string> { "Home", "Test" }
        };
    }

    public static object SlashSingleCrumb
    {
        get => new
        {
            Title = "Home Page",
            Separator = "/",
            BreadCrumbItems = new List<string> { "Home" }
        };
    }

    public static object SlashMultiCrumb
    {
        get => new
        {
            Title = "Home Page",
            Separator = "/",
            BreadCrumbItems = new List<string> { "Home", "Test" }
        };
    }
}
