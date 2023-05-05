using System.Collections;

namespace Carlton.Base.Infrastructure.Test.UnitTesting.XUnit;

public class XUnitParserTestData : IEnumerable<object[]>
{
    //Expected Results 1
    public readonly static IEnumerable<TestResult> ExpectedResults_1_NoTraits = new List<TestResult>
    {
        new TestResult("Assembly_1_Component_1_Test_1", TestResultOutcomes.Passed, 0.0162284),
        new TestResult("Assembly_1_Component_1_Test_2", TestResultOutcomes.Passed, 0.0151508),
        new TestResult("Assembly_1_Component_1_Test_3", TestResultOutcomes.Passed, 0.0056237),
        new TestResult("Assembly_1_Component_1_Test_4", TestResultOutcomes.Passed, 0.0340880),
        new TestResult("Assembly_1_Component_2_Test_1", TestResultOutcomes.Passed, 0.1442009),
        new TestResult("Assembly_1_Component_2_Test_2", TestResultOutcomes.Failed, 0.0127423),
        new TestResult("Assembly_1_Component_3_Test_1", TestResultOutcomes.Passed, 0.0405402),
    };
    public readonly static IEnumerable<TestResult> ExpectedResults_1_WithTraits = new List<TestResult>
    {
        new TestResult("Assembly 1 Component 1 Test 1", TestResultOutcomes.Passed, 0.0162284),
        new TestResult("Assembly 1 Component 1 Test 2", TestResultOutcomes.Passed, 0.0151508),
        new TestResult("Assembly 1 Component 1 Test 3", TestResultOutcomes.Passed, 0.0056237),
        new TestResult("Assembly 1 Component 1 Test 4", TestResultOutcomes.Passed, 0.0340880),
        new TestResult("Assembly 1 Component 2 Test 1", TestResultOutcomes.Passed, 0.1442009),
        new TestResult("Assembly 1 Component 2 Test 2", TestResultOutcomes.Failed, 0.0127423),
        new TestResult("Assembly 1 Component 3 Test 1", TestResultOutcomes.Passed, 0.0405402),
    };

    public readonly static TestResultsSummary ExpectedSummary_1 = new(7, 6, 1, 0.26857430000000004);
    public readonly static TestResultsReport ExpectedReport_1_NoTraits = new(ExpectedResults_1_NoTraits, ExpectedSummary_1);
    public readonly static TestResultsReport ExpectedReport_1_WithTraits = new(ExpectedResults_1_WithTraits, ExpectedSummary_1);

    //Expected Results 2
    public static readonly IEnumerable<TestResult> ExpectedResults_2_NoTraits = new List<TestResult>
    {
        new TestResult("Assembly_1_Component_1_Test_1", TestResultOutcomes.Passed, 0.0162284),
        new TestResult("Assembly_1_Component_1_Test_2", TestResultOutcomes.Failed, 0.0151508),
        new TestResult("Assembly_2_Component_1_Test_1", TestResultOutcomes.Passed, 0.0530211),
        new TestResult("Assembly_2_Component_1_Test_2", TestResultOutcomes.Failed, 0.0433452),
        new TestResult("Assembly_3_Component_1_Test_1", TestResultOutcomes.Passed, 0.3242322),
        new TestResult("Assembly_3_Component_1_Test_2", TestResultOutcomes.Failed, 0.3423426)
    };
    public static readonly IEnumerable<TestResult> ExpectedResults_2_WithTraits = new List<TestResult>
    {
        new TestResult("Assembly 1 Component 1 Test 1", TestResultOutcomes.Passed, 0.0162284),
        new TestResult("Assembly 1 Component 1 Test 2", TestResultOutcomes.Failed, 0.0151508),
        new TestResult("Assembly 2 Component 1 Test 1", TestResultOutcomes.Passed, 0.0530211),
        new TestResult("Assembly 2 Component 1 Test 2", TestResultOutcomes.Failed, 0.0433452),
        new TestResult("Assembly 3 Component 1 Test 1", TestResultOutcomes.Passed, 0.3242322),
        new TestResult("Assembly 3 Component 1 Test 2", TestResultOutcomes.Failed, 0.3423426)
    };

    public readonly static TestResultsSummary ExpectedSummary_2 = new(6, 3, 3, .7943203000000001);
    public readonly static TestResultsReport ExpectedReport_2_NoTraits = new(ExpectedResults_2_NoTraits, ExpectedSummary_2);
    public readonly static TestResultsReport ExpectedReport_2_WithTraits = new(ExpectedResults_2_WithTraits, ExpectedSummary_2);

    //Expected Results 3
    public static readonly IEnumerable<TestResult> ExpectedResults_3_NoTraits = new List<TestResult>
    {
        new TestResult("Assembly_1_Component_1_Test_1", TestResultOutcomes.Passed, 0.0162284),
        new TestResult("Assembly_1_Component_1_Test_2", TestResultOutcomes.Passed, 0.0151508),
        new TestResult("Assembly_1_Component_1_Test_3", TestResultOutcomes.Passed, 0.0056237),
        new TestResult("Assembly_1_Component_1_Test_4", TestResultOutcomes.Passed, 0.0340880),
        new TestResult("Assembly_1_Component_2_Test_1", TestResultOutcomes.Passed, 0.1442009),
        new TestResult("Assembly_1_Component_2_Test_2", TestResultOutcomes.Failed, 0.0127423),
        new TestResult("Assembly_2_Component_1_Test_1", TestResultOutcomes.Passed, 0.0530211),
        new TestResult("Assembly_2_Component_1_Test_2", TestResultOutcomes.Failed, 0.0433452),
        new TestResult("Assembly_2_Component_2_Test_1", TestResultOutcomes.Passed, 0.1110342),
        new TestResult("Assembly_3_Component_1_Test_1", TestResultOutcomes.Passed, 0.3242322),
        new TestResult("Assembly_3_Component_1_Test_2", TestResultOutcomes.Failed, 0.3423426)
    };
    public static readonly IEnumerable<TestResult> ExpectedResults_3_WithTraits = new List<TestResult>
    {
        new TestResult("Assembly 1 Component 1 Test 1", TestResultOutcomes.Passed, 0.0162284),
        new TestResult("Assembly 1 Component 1 Test 2", TestResultOutcomes.Passed, 0.0151508),
        new TestResult("Assembly 1 Component 1 Test 3", TestResultOutcomes.Passed, 0.0056237),
        new TestResult("Assembly 1 Component 1 Test 4", TestResultOutcomes.Passed, 0.0340880),
        new TestResult("Assembly 1 Component 2 Test 1", TestResultOutcomes.Passed, 0.1442009),
        new TestResult("Assembly 1 Component 2 Test 2", TestResultOutcomes.Failed, 0.0127423),
        new TestResult("Assembly 2 Component 1 Test 1", TestResultOutcomes.Passed, 0.0530211),
        new TestResult("Assembly 2 Component 1 Test 2", TestResultOutcomes.Failed, 0.0433452),
        new TestResult("Assembly 2 Component 2 Test 1", TestResultOutcomes.Passed, 0.1110342),
        new TestResult("Assembly 3 Component 1 Test 1", TestResultOutcomes.Passed, 0.3242322),
        new TestResult("Assembly 3 Component 1 Test 2", TestResultOutcomes.Failed, 0.3423426)
    };

    public readonly static TestResultsSummary ExpectedSummary_3 = new(11, 8, 3, 1.1020094);
    public readonly static TestResultsReport ExpectedReport_3_NoTraits = new(ExpectedResults_3_NoTraits, ExpectedSummary_3);
    public readonly static TestResultsReport ExpectedReport_3_WithTraits = new(ExpectedResults_3_WithTraits, ExpectedSummary_3);

    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { XUnitParserSampleSource.Content_1_NoTraits, ExpectedReport_1_NoTraits };
        yield return new object[] { XUnitParserSampleSource.Content_1_WithTraits, ExpectedReport_1_WithTraits };
        yield return new object[] { XUnitParserSampleSource.Content_2_NoTraits, ExpectedReport_2_NoTraits };
        yield return new object[] { XUnitParserSampleSource.Content_2_WithTraits, ExpectedReport_2_WithTraits };
        yield return new object[] { XUnitParserSampleSource.Content_3_NoTraits, ExpectedReport_3_NoTraits };
        yield return new object[] { XUnitParserSampleSource.Content_3_WithTraits, ExpectedReport_3_WithTraits };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}