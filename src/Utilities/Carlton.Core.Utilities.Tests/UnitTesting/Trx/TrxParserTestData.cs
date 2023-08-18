using System.Collections;

namespace Carlton.Core.Utilities.Test.UnitTesting.Trx;

public class TrxParserTestData : IEnumerable<object[]>
{
    //Result 1
    public static readonly IEnumerable<TestResultModel> TestResults = new List<TestResultModel>()
    {
        new TestResultModel(TrxParserSampleSources.TestName_1, TrxParserSampleSources.TestOutcome_1, TrxParserSampleSources.Duration_1),
        new TestResultModel(TrxParserSampleSources.TestName_2, TrxParserSampleSources.TestOutcome_2, TrxParserSampleSources.Duration_2),
        new TestResultModel(TrxParserSampleSources.TestName_3, TrxParserSampleSources.TestOutcome_3, TrxParserSampleSources.Duration_3),
        new TestResultModel(TrxParserSampleSources.TestName_4, TrxParserSampleSources.TestOutcome_4, TrxParserSampleSources.Duration_4),
    };
    public static readonly TestResultsReportModel TestReport = new(TestResults);

    //Result 2
    public static readonly IEnumerable<TestResultModel> TestResults_2 = new List<TestResultModel>()
    {
        new TestResultModel(TrxParserSampleSources.TestName_1, TrxParserSampleSources.TestOutcome_1, TrxParserSampleSources.Duration_1),
        new TestResultModel(TrxParserSampleSources.TestName_4, TrxParserSampleSources.TestOutcome_4, TrxParserSampleSources.Duration_4),
    };
    public static readonly TestResultsReportModel TestReport_2 = new(TestResults_2);

    //Result 3
    public static readonly IEnumerable<TestResultModel> TestResults_3 = new List<TestResultModel>()
    {
        new TestResultModel(TrxParserSampleSources.TestName_1, TrxParserSampleSources.TestOutcome_1, TrxParserSampleSources.Duration_1),
        new TestResultModel(TrxParserSampleSources.TestName_2, TrxParserSampleSources.TestOutcome_2, TrxParserSampleSources.Duration_2),
        new TestResultModel(TrxParserSampleSources.TestName_3, TrxParserSampleSources.TestOutcome_3, TrxParserSampleSources.Duration_3),
    };
    public static readonly TestResultsReportModel TestReport_3 = new(TestResults_3);

    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { TrxParserSampleSources.UnitTestResult_1, TestReport };
        yield return new object[] { TrxParserSampleSources.UnitTestResult_2, TestReport_2 };
        yield return new object[] { TrxParserSampleSources.UnitTestResult_3, TestReport_3 };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
