using System.Collections;

namespace Carlton.Base.Infrastructure.Test.UnitTesting.XUnit;

public class XUnitParserGroupedTestData : IEnumerable<object[]>
{
    //Category 1
    public readonly static IEnumerable<TestResult> ExpectedCategory_1_Results = new List<TestResult>
    {
        new TestResult(XUnitParserSampleSource.TestName_1, XUnitParserSampleSource.TestOutcome_1, XUnitParserSampleSource.Duration_1),
        new TestResult(XUnitParserSampleSource.TestName_2, XUnitParserSampleSource.TestOutcome_2, XUnitParserSampleSource.Duration_2),
        new TestResult(XUnitParserSampleSource.TestName_3, XUnitParserSampleSource.TestOutcome_3, XUnitParserSampleSource.Duration_3),
    };
    public readonly static TestResultsReport ExpectedCategory_1_Report = new(ExpectedCategory_1_Results);

    //Category 2
    public readonly static IEnumerable<TestResult> ExpectedCategory_2_Results = new List<TestResult>
    {
        new TestResult(XUnitParserSampleSource.TestName_1, XUnitParserSampleSource.TestOutcome_1, XUnitParserSampleSource.Duration_1),
        new TestResult(XUnitParserSampleSource.TestName_2, XUnitParserSampleSource.TestOutcome_2, XUnitParserSampleSource.Duration_2)
    };
    public readonly static TestResultsReport ExpectedCategory_2_Report = new(ExpectedCategory_2_Results);

    //Category 3
    public readonly static IEnumerable<TestResult> ExpectedCategory_3_Results = new List<TestResult>
    {
        new TestResult(XUnitParserSampleSource.TestName_5, XUnitParserSampleSource.TestOutcome_5, XUnitParserSampleSource.Duration_5)
    };
    public readonly static TestResultsReport ExpectedCategory_3_Report = new(ExpectedCategory_3_Results);

    //Default
    public readonly static IEnumerable<TestResult> ExpectedCategory_Default_Results = new List<TestResult>
    {
        new TestResult(XUnitParserSampleSource.TestName_4, XUnitParserSampleSource.TestOutcome_4, XUnitParserSampleSource.Duration_4)
    };
    public readonly static TestResultsReport ExpectedCategory_Default_Report = new(ExpectedCategory_Default_Results);

    public readonly static IReadOnlyDictionary<string, TestResultsReport> ExpectedGroups = new Dictionary<string, TestResultsReport>
    {
        { XUnitParserSampleSource.Category1, ExpectedCategory_1_Report },
        { XUnitParserSampleSource.Category2, ExpectedCategory_2_Report },
        { XUnitParserSampleSource.Default, ExpectedCategory_Default_Report },
        { XUnitParserSampleSource.Category3, ExpectedCategory_3_Report }
    };


    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { XUnitParserSampleSource.Content, ExpectedGroups };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
