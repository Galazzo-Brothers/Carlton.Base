using System.Collections;

namespace Carlton.Base.Infrastructure.Test.UnitTesting.Trx;

public class TrxParserGroupedTestData : IEnumerable<object[]>
{
    //Category 1
    public static readonly IEnumerable<TestResult> Category1_TestResults = new List<TestResult>()
    {
        new TestResult(TrxParserSampleSources.TestName_1, TrxParserSampleSources.TestOutcome_1, TrxParserSampleSources.Duration_1),
        new TestResult(TrxParserSampleSources.TestName_2, TrxParserSampleSources.TestOutcome_2, TrxParserSampleSources.Duration_2)
    };
    public static readonly TestResultsReport Category1_TestReport = new(Category1_TestResults);

    //Category 2
    public static readonly IEnumerable<TestResult> Category2_TestResults = new List<TestResult>()
    {
        new TestResult(TrxParserSampleSources.TestName_1, TrxParserSampleSources.TestOutcome_1, TrxParserSampleSources.Duration_1),
        new TestResult(TrxParserSampleSources.TestName_2, TrxParserSampleSources.TestOutcome_2, TrxParserSampleSources.Duration_2)
    };
    public static readonly TestResultsReport Category2_TestReport = new(Category1_TestResults);

    //Category 3
    public static readonly IEnumerable<TestResult> Category3_TestResults = new List<TestResult>()
    {
        new TestResult(TrxParserSampleSources.TestName_3, TrxParserSampleSources.TestOutcome_3, TrxParserSampleSources.Duration_3),
    };
    public static readonly TestResultsReport Category3_TestReport = new(Category3_TestResults);

    //Default

    public static readonly IEnumerable<TestResult> DefaultCategory_TestResults = new List<TestResult>()
    {
        new TestResult(TrxParserSampleSources.TestName_4, TrxParserSampleSources.TestOutcome_4, TrxParserSampleSources.Duration_4),
    };
    public static readonly TestResultsReport DefaultCategory_TestReport = new(DefaultCategory_TestResults);

    public static readonly IReadOnlyDictionary<string, TestResultsReport> CategorizedResults = new Dictionary<string, TestResultsReport>()
    {
        { TrxParserSampleSources.Category1, Category1_TestReport },
        { TrxParserSampleSources.Category2, Category2_TestReport },
        { TrxParserSampleSources.Category3, Category3_TestReport },
        { TrxParserSampleSources.Default,  DefaultCategory_TestReport }
    };

    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { TrxParserSampleSources.UnitTestResult_1, CategorizedResults };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
