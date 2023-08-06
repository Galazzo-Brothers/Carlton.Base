using System.Collections;

namespace Carlton.Core.Infrastructure.Test.UnitTesting.XUnit;

public class XUnitParserTestData : IEnumerable<object[]>
{
    public readonly static IEnumerable<TestResultModel> ExpectedResults = new List<TestResultModel>
    {
        new TestResultModel(XUnitParserSampleSource.TestName_1, XUnitParserSampleSource.TestOutcome_1, XUnitParserSampleSource.Duration_1),
        new TestResultModel(XUnitParserSampleSource.TestName_2, XUnitParserSampleSource.TestOutcome_2, XUnitParserSampleSource.Duration_2),
        new TestResultModel(XUnitParserSampleSource.TestName_3, XUnitParserSampleSource.TestOutcome_3, XUnitParserSampleSource.Duration_3),
        new TestResultModel(XUnitParserSampleSource.TestName_4, XUnitParserSampleSource.TestOutcome_4, XUnitParserSampleSource.Duration_4),
        new TestResultModel(XUnitParserSampleSource.TestName_5, XUnitParserSampleSource.TestOutcome_5, XUnitParserSampleSource.Duration_5),
    };
    public readonly static TestResultsReportModel ExpectedReport = new(ExpectedResults);

    public IEnumerator<object[]> GetEnumerator()
    { 
        yield return new object[] { XUnitParserSampleSource.Content, ExpectedReport };

    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}