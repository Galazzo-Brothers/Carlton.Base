namespace Carlton.Base.Infrastructure.UnitTesting;

public class TrxTestResultsParser : ITestResultsParser
{
    //Tag Local Names
    private const string TestRun = "TestRun";
    private const string Results = "Results";
    private const string TestDefinitions = "TestDefinitions";
    private const string UnitTest = "UnitTest";
    private const string TestCategory = "TestCategory";
    private const string TestCategoryItem = "TestCategoryItem";

    //Results Attributes
    private const string TestName = "testName";
    private const string Outcome = "outcome";
    private const string Duration = "duration";

    //Other
    private const string Default = "default";


    public TestResultsReport ParseTestResults(string content)
    {
        try
        {
            var document = XDocument.Parse(content);

            var results = document.Elements()
                                   .First(_ => _.Name.LocalName == TestRun)
                                   .Elements()
                                   .Where(_ => _.Name.LocalName == Results)
                                   .Elements()
                                   .Select(_ =>
                                   {
                                       var resultAttributes = _.Attributes().ToDictionary(attrib => attrib.Name, attrib => attrib.Value);
                                       var testName = resultAttributes[TestName];
                                       var testResult = (TestResultOutcomes)Enum.Parse(typeof(TestResultOutcomes), resultAttributes[Outcome]);
                                       var duration = Math.Round(TimeSpan.Parse(resultAttributes[Duration]).TotalMilliseconds, 2);

                                       return new TestResult(testName, testResult, duration);

                                   }).ToList();



            return new TestResultsReport(results);
        }
        catch(Exception)
        {
            throw new ArgumentException("Content is not a valid Trx file.");
        }
    }

    public IDictionary<string, TestResultsReport> ParseTestResultsByGroup(string content)
    {
        var groupedTestResults = new Dictionary<string, List<TestResult>>();

        //Parse the TRX file for a complete list of test results
        //Create a look up of tests to categories
        var allTestResults = ParseTestResults(content).TestResults;
        var categoriesLookup = ParseCategories(content);

        //Find the distinct list of all possible categories
        var distinctCategories = categoriesLookup.SelectMany(_ => _.Value).Distinct();

        //Create a grouping of distinct categories to test results
        foreach(var category in distinctCategories)
            groupedTestResults.Add(category, new List<TestResult>());

        //Add each test into its respective group
        //a test can appear in multiple groups
        //or a default category if none is specified
        foreach(var testResult in allTestResults)
        {
            var testIsCategorized = categoriesLookup.TryGetValue(testResult.TestName, out var testCategories);

            if(testIsCategorized)
                testCategories.ToList().ForEach(_ => groupedTestResults[_].Add(testResult));
            else
                AddToDefaultGroup(groupedTestResults, testResult);
        }

        //Parse the final response into a <string, TestResultsReport Dictionary
        return groupedTestResults.ToDictionary(_ => _.Key, _ => new TestResultsReport(_.Value));
    }

    public IDictionary<string, TestResultsReport> ParseTestResultsByGroup(string content, string groupKey)
    {
        throw new NotImplementedException();
    }

    private static void AddToDefaultGroup(Dictionary<string, List<TestResult>> groupedTestResults, TestResult testResult)
    {
        if(!groupedTestResults.ContainsKey(Default))
        {
            groupedTestResults.Add(Default, new List<TestResult>());
        }

        groupedTestResults[Default].Add(testResult);
    }

    private static Dictionary<string, IEnumerable<string>> ParseCategories(string content)
    {
        var document = XDocument.Parse(content);
        var lookup = new Dictionary<string, IEnumerable<string>>();
        document.Elements()
                .First(_ => _.Name.LocalName == TestRun)
                .Elements()
                .Where(_ => _.Name.LocalName == TestDefinitions)
                .Elements()
                .Where(_ => _.Name.LocalName == UnitTest)
                .ToList()
                .ForEach(_ =>
                {
                    var name = _.Attribute("name").Value;
                    var categoryItems = _.Elements()
                        ?.FirstOrDefault(el => el.Name.LocalName == TestCategory)
                        ?.Elements()
                        ?.Where(el => el.Name.LocalName == TestCategoryItem);

                    if(categoryItems == null || !categoryItems.Any())
                        return;

                    var categories = new List<string>();
                    foreach(var item in categoryItems)
                        categories.Add(item.Attribute(TestCategory).Value);

                    lookup.Add(name, categories);
                });

        return lookup;
    }
}
