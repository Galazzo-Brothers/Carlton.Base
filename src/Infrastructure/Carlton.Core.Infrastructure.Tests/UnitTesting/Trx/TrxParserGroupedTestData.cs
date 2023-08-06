using System.Collections;

namespace Carlton.Core.Infrastructure.Test.UnitTesting.Trx;

public class TrxParserGroupedTestData : IEnumerable<object[]>
{
    //Category 1
    public static readonly IEnumerable<TestResultModel> Category1_TestResults = new List<TestResultModel>()
    {
        new TestResultModel(TrxParserSampleSources.TestName_1, TrxParserSampleSources.TestOutcome_1, TrxParserSampleSources.Duration_1),
        new TestResultModel(TrxParserSampleSources.TestName_2, TrxParserSampleSources.TestOutcome_2, TrxParserSampleSources.Duration_2)
    };
    public static readonly TestResultsReportModel Category1_TestReport = new(Category1_TestResults);

    //Category 2
    public static readonly IEnumerable<TestResultModel> Category2_TestResults = new List<TestResultModel>()
    {
        new TestResultModel(TrxParserSampleSources.TestName_1, TrxParserSampleSources.TestOutcome_1, TrxParserSampleSources.Duration_1),
        new TestResultModel(TrxParserSampleSources.TestName_2, TrxParserSampleSources.TestOutcome_2, TrxParserSampleSources.Duration_2)
    };
    public static readonly TestResultsReportModel Category2_TestReport = new(Category1_TestResults);

    //Category 3
    public static readonly IEnumerable<TestResultModel> Category3_TestResults = new List<TestResultModel>()
    {
        new TestResultModel(TrxParserSampleSources.TestName_3, TrxParserSampleSources.TestOutcome_3, TrxParserSampleSources.Duration_3),
    };
    public static readonly TestResultsReportModel Category3_TestReport = new(Category3_TestResults);

    //Default

    public static readonly IEnumerable<TestResultModel> DefaultCategory_TestResults = new List<TestResultModel>()
    {
        new TestResultModel(TrxParserSampleSources.TestName_4, TrxParserSampleSources.TestOutcome_4, TrxParserSampleSources.Duration_4),
    };
    public static readonly TestResultsReportModel DefaultCategory_TestReport = new(DefaultCategory_TestResults);

    public static readonly IReadOnlyDictionary<string, TestResultsReportModel> CategorizedResults = new Dictionary<string, TestResultsReportModel>()
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
