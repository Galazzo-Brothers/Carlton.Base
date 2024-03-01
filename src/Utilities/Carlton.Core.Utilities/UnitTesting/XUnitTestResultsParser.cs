using System.Xml.Linq;
using Constants = Carlton.Core.Utilities.UnitTesting.UnitTestingConstants;
namespace Carlton.Core.Utilities.UnitTesting;

/// <summary>
/// Parses XUnit test results files.
/// </summary>
public class XUnitTestResultsParser : ITestResultsParser
{
    /// <inheritdoc/>
    public TestResultsReport ParseTestResults(string content)
    {
        try
        {
            var document = XDocument.Parse(content);
            var results = ParseDocument(document).Select(_ => ParseTestResult(_).First().Value);
            return new TestResultsReport(results);
        }
        catch(Exception)
        {
            throw new ArgumentException(Constants.ExMessage);
        }
    }

    /// <inheritdoc/>
    public IDictionary<string, TestResultsReport> ParseTestResultsByGroup(string content)
    {
        return ParseTestResultsByGroup(content, Constants.TestCategory);
    }

    /// <inheritdoc/>
    public IDictionary<string, TestResultsReport> ParseTestResultsByGroup(string content, string groupKey)
    {
        try
        {
            var results = new Dictionary<string, TestResultsReport>();

            var document = XDocument.Parse(content);
            var tests = ParseDocument(document).SelectMany(_ => ParseTestResult(_, groupKey));

            tests.GroupBy(_ => _.Key, _ => _.Value)
                           .ToList()
                           .ForEach(group =>
                           {
                               var report = new TestResultsReport(group);
                               results.Add(group.Key, report);
                           });

            return results;
        }
        catch(Exception)
        {
            throw new ArgumentException(Constants.ExMessage);
        }
    }

    private static IEnumerable<XElement> ParseDocument(XDocument document)
    {
        return document.Elements()
                       .First(_ => _.Name == Constants.Assemblies)
                       .Elements()
                       .Where(_ => _.Name == Constants.Assembly)
                       .SelectMany(_ => _.Elements())
                       .Where(_ => _.Name == Constants.Collection)
                       .SelectMany(_ => _.Elements())
                       .Where(_ => _.Name == Constants.Test);
    }

    private static IList<KeyValuePair<string, TestResult>> ParseTestResult(XElement testElement, string groupingTrait = null)
    {
        //Find all traits
        var traitsElements = testElement.Elements()
                                .FirstOrDefault(_ => _.Name == Constants.Traits)
                                ?.Elements();

        //Find the grouping traits
        var groupingTraits = traitsElements.Where(_ => _.Attribute(Constants.Name)?.Value == groupingTrait);

        //Parse the test results element attributes into a dictionary
        var resultAttributes = testElement.Attributes().ToDictionary(attrib => attrib.Name, attrib => attrib.Value);

        //Create a TestResult object
        var testResult = ParseTestResult(resultAttributes);

        //Handle Groups
        var result = new List<KeyValuePair<string, TestResult>>();
        if(groupingTraits.Any())
        {
            //Create a resulting key/value pair for each group
            foreach(var groupingKey in groupingTraits.Select(_ => _.Attribute(Constants.Value).Value))
                result.Add(new KeyValuePair<string, TestResult>(groupingKey, testResult));
        }
        else
        {
            //Add the test result to the default grouping
            result.Add(new KeyValuePair<string, TestResult>(Constants.Default, testResult));
        }

        return result;
    }

    private static TestResult ParseTestResult(Dictionary<XName, string> resultAttributes)
    {
        var outcome = resultAttributes[Constants.Result];
        var parsedOutcome = outcome switch
        {
            Constants.Pass => TestResultOutcomes.Passed,
            Constants.Fail => TestResultOutcomes.Failed,
            _ => throw new ArgumentException(Constants.ExMessage),
        };
        var duration = double.Parse(resultAttributes[Constants.Time]);
        var testName = resultAttributes[Constants.Name];

        var testResult = new TestResult(testName, parsedOutcome, duration);
        return testResult;
    }
}

