namespace Carlton.Base.Infrastructure.UnitTesting;

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
    private const string Value = "value";
    private const string ExMessage = "Content is not a valid XUnit test results file.";

    private const string DisplayName = "DisplayName";
    private const string Default = "default";

    public TestResultsReport ParseTestResults(string content)
    {
        try
        {
            var document = XDocument.Parse(content);

            var results = ParseDocument(document).Select(_ => ParseTestResult(_).Value);

            var totalCount = 0;
            var totalPassedCount = 0;
            var totalFailureCount = 0;
            var totalDuration = 0.0;

            results.ToList().ForEach(_ => CalculateSummary(_, ref totalCount, ref totalPassedCount, ref totalFailureCount, ref totalDuration));

            var summary = new TestResultsSummary(totalCount, totalPassedCount, totalFailureCount, totalDuration);

            return new TestResultsReport
            (
                results,
                summary
            );
        }
        catch(Exception)
        {
            throw new ArgumentException(ExMessage);
        }
    }

    public IDictionary<string, TestResultsReport> ParseTestResultsByGroup(string content, string groupKey)
    {
        try
        {
            var results = new Dictionary<string, TestResultsReport>();

            var document = XDocument.Parse(content);
            var tests = ParseDocument(document).Select(_ => ParseTestResult(_, groupKey));

            tests.GroupBy(_ => _.Key, _ => _.Value)
                           .ToList()
                           .ForEach(group =>
                           {
                               var totalCount = 0;
                               var totalPassedCount = 0;
                               var totalFailureCount = 0;
                               var totalDuration = 0.0;

                               group.ToList()
                                    .ForEach(_ => CalculateSummary(_, ref totalCount, ref totalPassedCount, ref totalFailureCount, ref totalDuration));

                               var summary = new TestResultsSummary(totalCount, totalPassedCount, totalFailureCount, totalDuration);
                               var report = new TestResultsReport(group, summary);

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
    
    private static KeyValuePair<string, TestResult> ParseTestResult(XElement testElement)
    {
        return ParseTestResult(testElement, string.Empty);
    }

    private static KeyValuePair<string, TestResult> ParseTestResult(XElement testElement, string groupingTrait)
    {
        var traitsElements = testElement.Elements()
                                .FirstOrDefault(_ => _.Name == Traits)
                                ?.Elements();

        var traits = traitsElements
                    .ToDictionary(_ => _.Attribute(Name).Value, _ => _.Attribute(Value).Value);

        traits.TryGetValue(DisplayName, out var displayName);
        traits.TryGetValue(groupingTrait, out var groupingName);


        var resultAttributes = testElement.Attributes().ToDictionary(attrib => attrib.Name, attrib => attrib.Value);
        var testName = (string.IsNullOrEmpty(displayName)) ? resultAttributes[Name] : displayName;
        var outcome = resultAttributes[Result];
        var parsedOutcome = outcome switch
        {
            Pass => TestResultOutcomes.Passed,
            Fail => TestResultOutcomes.Failed,
            _ => throw new ArgumentException(ExMessage),
        };
        var duration = double.Parse(resultAttributes[Time]);

        var testResult = new TestResult(testName, parsedOutcome, duration);
        var groupingKey = string.IsNullOrEmpty(groupingName) ? Default : groupingName;
        return new KeyValuePair<string, TestResult>(groupingKey, testResult);
    }

    private static void CalculateSummary(TestResult testResult, ref int totalCount, ref int totalPassedCount, ref int totalFailureCount, ref double totalDuration)
    {
        totalCount++;
        totalDuration += testResult.TestDuration;

        if(testResult.TestResultOutcome == TestResultOutcomes.Passed)
            totalPassedCount++;
        if(testResult.TestResultOutcome == TestResultOutcomes.Failed)
            totalFailureCount++;
    }
}

