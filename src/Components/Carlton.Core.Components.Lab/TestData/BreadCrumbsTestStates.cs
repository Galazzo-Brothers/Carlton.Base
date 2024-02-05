using Carlton.Core.Components.Navigation;
namespace Carlton.Core.Components.Library.Lab.TestData;

internal static class BreadCrumbsTestStates
{
    public static Dictionary<string, object> CarrotSingleCrumb
    {
        get => new()
        {
            { nameof(BreadCrumbs.Title), "Home Page" },
            { nameof(BreadCrumbs.Separator), '>' },
            { nameof(BreadCrumbs.BreadCrumbItems), new List <string> { "Home" } }
        };
    }

    public static Dictionary<string, object> CarrotMultiCrumb
    {
        get => new()
        {
            { nameof(BreadCrumbs.Title), "Home Page" },
            { nameof(BreadCrumbs.Separator), '>' },
            { nameof(BreadCrumbs.BreadCrumbItems), new List <string> { "Home", "Test" } }
        };
    }

    public static Dictionary<string, object> SlashSingleCrumb
    {
        get => new()
        {
            { nameof(BreadCrumbs.Title), "Home Page" },
            { nameof(BreadCrumbs.Separator), '/' },
            { nameof(BreadCrumbs.BreadCrumbItems), new List <string> { "Home" } }
        };
    }

    public static Dictionary<string, object> SlashMultiCrumb
    {
        get => new()
        {
            { nameof(BreadCrumbs.Title), "Home Page" },
            { nameof(BreadCrumbs.Separator), '/' },
            { nameof(BreadCrumbs.BreadCrumbItems), new List <string> { "Home", "Test" } }
        };
    }
}
