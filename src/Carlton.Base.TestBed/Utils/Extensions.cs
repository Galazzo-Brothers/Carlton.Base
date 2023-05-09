namespace Carlton.Base.TestBed;

public static class TestBedUtils
{
    public static IDictionary<string, TestResultsReport> ParseXUnitTestResults(string testResults)
    {
        var parser = new XUnitTestResultsParser();
        return parser.ParseTestResultsByGroup(testResults, "Component");
    }

    public static IDictionary<string, TestResultsReport> ParseTrxTestResults(string testResults)
    {
        var parser = new TrxTestResultsParser();
        return parser.ParseTestResultsByGroup(testResults);
    }
}

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


