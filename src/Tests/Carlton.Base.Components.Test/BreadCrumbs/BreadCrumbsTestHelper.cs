namespace Carlton.Base.Components.Test;

public static class BreadCrumbsTestHelper
{
    public const string BreadCrumbsMarkup = @"
    <div class=""page-title"" b-hv0lfh6f33>
        <span class=""title"" b-hv0lfh6f33>Test Title</span>
        <span class=""bread-crumbs"" b-hv0lfh6f33>Home &gt; Test &gt; SubTest</span>
    </div>";

    public const string Title = "Test Title";
    public const string Separator = ">";
    public static readonly IReadOnlyCollection<string> BreadCrumbItems = new List<string> { "Home", "Test", "SubTest" };

    public static IEnumerable<object[]> GetItems()
    {
        yield return new object[]
           {(
                new List<string>()
                {
                     "Home Page",
                },
                "Home Page"
           )};
        yield return new object[]
           {(
                new List<string>()
                {
                     "Home Page",
                     "Test Page"
                },
                "Home Page > Test Page"
           )};
        yield return new object[]
            {(
                new List<string>()
                {
                     "Home Page",
                     "Test Page",
                     "SubTest Page"
                },
                "Home Page > Test Page > SubTest Page"
            )};
    }
}
