namespace Carlton.Core.Infrastructure.UnitTesting;

public class XUnitTestResultsParser : ITestResultsParser
{
    private const string Assemblies = "assemblies";
    private const string Assembly = "assembly";
    private const string Collection = "collection";
    private const string Test = "test";
    private const string Name = "name";
    private const string Result = "result";
    private const string Time = "time";
    private const string Pass = "Pass";
    private const string Fail = "Fail";
    private const string Traits = "traits";
    private const string ExMessage = "Content is not a valid XUnit test results file.";
    private const string Value = "value";
    private const string Default = "default";
    private const string TestCategory = "TestCategory";

    public TestResultsReportModel ParseTestResults(string content)
    {
        try
        {
            var document = XDocument.Parse(content);
            var results = ParseDocument(document).Select(_ => ParseTestResult(_).First().Value);
            return new TestResultsReportModel(results);
        }
        catch(Exception)
        {
            throw new ArgumentException(ExMessage);
        }
    }

    public IDictionary<string, TestResultsReportModel> ParseTestResultsByGroup(string content)
    {
        return ParseTestResultsByGroup(content, TestCategory);
    }

    public IDictionary<string, TestResultsReportModel> ParseTestResultsByGroup(string content, string groupKey)
    {
        try
        {
            var results = new Dictionary<string, TestResultsReportModel>();

            var document = XDocument.Parse(content);
            var tests = ParseDocument(document).SelectMany(_ => ParseTestResult(_, groupKey));

            tests.GroupBy(_ => _.Key, _ => _.Value)
                           .ToList()
                           .ForEach(group =>
                           {
                               var report = new TestResultsReportModel(group);
                               results.Add(group.Key, report);
                           });

            return results;
        }
        catch(Exception)
        {
            throw new ArgumentException(ExMessage);
        }
    }

    private static IEnumerable<XElement> ParseDocument(XDocument document)
    {
        return document.Elements()
                       .First(_ => _.Name == Assemblies)
                       .Elements()
                       .Where(_ => _.Name == Assembly)
                       .SelectMany(_ => _.Elements())
                       .Where(_ => _.Name == Collection)
                       .SelectMany(_ => _.Elements())
                       .Where(_ => _.Name == Test);
    }

    private static IEnumerable<KeyValuePair<string, TestResultModel>> ParseTestResult(XElement testElement, string groupingTrait = null)
    {
        //Find all traits
        var traitsElements = testElement.Elements()
                                .FirstOrDefault(_ => _.Name == Traits)
                                ?.Elements();

        //Find the grouping traits
        var groupingTraits = traitsElements.Where(_ => _.Attribute(Name)?.Value == groupingTrait);

        //Parse the test results element attributes into a dictionary
        var resultAttributes = testElement.Attributes().ToDictionary(attrib => attrib.Name, attrib => attrib.Value);

        //Create a TestResult object
        var testResult = ParseTestResult(resultAttributes);

        //Handle Groups
        var result = new List<KeyValuePair<string, TestResultModel>>();
        if(groupingTraits.Any())
        {
            //Create a resulting key/value pair for each group
            foreach(var groupingKey in groupingTraits.Select(_ => _.Attribute(Value).Value))
                result.Add(new KeyValuePair<string, TestResultModel>(groupingKey, testResult));
        }
        else
        {
            //Add the test result to the default grouping
            result.Add(new KeyValuePair<string, TestResultModel>(Default, testResult));
        }

        return result;
    }

    private static TestResultModel ParseTestResult(Dictionary<XName, string> resultAttributes)
    {
        var outcome = resultAttributes[Result];
        var parsedOutcome = outcome switch
        {
            Pass => TestResultOutcomes.Passed,
            Fail => TestResultOutcomes.Failed,
            _ => throw new ArgumentException(ExMessage),
        };
        var duration = double.Parse(resultAttributes[Time]);
        var testName = resultAttributes[Name];

        var testResult = new TestResultModel(testName, parsedOutcome, duration);
        return testResult;
    }
}

