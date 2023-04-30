namespace Carlton.Base.Infrastructure.UnitTesting;

public class TrxParser : ITrxParser
{
    //Tag Local Names
    private const string TestRun = "TestRun";
    private const string ResultSummary = "ResultSummary";
    private const string Counters = "Counters";
    private const string Times = "Times";
    private const string Results = "Results";

    //ResultSummary Counter Attributes
    private const string Total = "total";
    private const string Passed = "passed";
    private const string Failed = "failed";

    //Times Attributes
    private const string Start = "start";
    private const string Finish = "finish";

    //Results Attributes
    private const string TestName = "testName";
    private const string Outcome = "outcome";
    private const string Duration = "duration";

    public IEnumerable<TestResult> ParseTrxTestResultsContent(string content)
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
                                       var testName = resultAttributes[TestName].Split('.').Last();
                                       var testResult = (TestResultOutcomes)Enum.Parse(typeof(TestResultOutcomes), resultAttributes[Outcome]);
                                       var duration = Math.Round(TimeSpan.Parse(resultAttributes[Duration]).TotalMilliseconds, 2);

                                       return new TestResult(testName, testResult, duration);

                                   }).ToList();
            return results;
        }
        catch(Exception)
        {
            throw new ArgumentException("Content is not a valid Trx file.");
        }
    }

    public TestResultsSummary ParseTrxSummaryContent(string content)
    {
        try
        {
            var document = XDocument.Parse(content);

            var timeAttributes = document.Elements()
                                .First(_ => _.Name.LocalName == TestRun)
                                .Elements()
                                .First(_ => _.Name.LocalName == Times)
                                .Attributes()
                                .ToDictionary(_ => _.Name.LocalName, _ => DateTime.Parse(_.Value));


            var summaryAttributes = document.Elements()
                            .First(_ => _.Name.LocalName == TestRun)
                            .Elements()
                            .First(_ => _.Name.LocalName == ResultSummary)
                            .Elements()
                            .First(_ => _.Name.LocalName == Counters)
                            .Attributes()
                            .ToDictionary(_ => _.Name.LocalName, _ => int.Parse(_.Value));

            var total = summaryAttributes[Total];
            var passed = summaryAttributes[Passed];
            var failed = summaryAttributes[Failed];

            var startDate = timeAttributes[Start];
            var endDate = timeAttributes[Finish];
            var timespan = endDate - startDate;

            var duration = Math.Round(timespan.TotalSeconds, 2);

            return new TestResultsSummary(total, passed, failed, duration);
        }
        catch(Exception)
        {
            throw new ArgumentException ("Content is not a valid Trx file.");
        }
    }
}
