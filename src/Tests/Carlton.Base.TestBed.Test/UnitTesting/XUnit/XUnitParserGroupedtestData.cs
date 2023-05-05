using System.Collections;

namespace Carlton.Base.Infrastructure.Test.UnitTesting.XUnit;

public class XUnitParserGroupedTestData : IEnumerable<object[]>
{
    //Expected Grouped Results 1
    #region Source_1
    public readonly static TestResultsSummary ExpectedSummary_1 = new(7, 6, 1, 0.26857430000000004);
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

    public readonly static TestResultsReport ExpectedReport_1_NoTraits = new(ExpectedResults_1_NoTraits, ExpectedSummary_1);
    public readonly static TestResultsReport ExpectedReport_1_WithTraits = new(ExpectedResults_1_WithTraits, ExpectedSummary_1);

    public readonly static IReadOnlyDictionary<string, TestResultsReport> ExpectedGroups_1_NoTraits = new Dictionary<string, TestResultsReport>
    {
        {"default", ExpectedReport_1_NoTraits }
    };

    public readonly static IReadOnlyDictionary<string, TestResultsReport> ExpectedGroups_1_WithTraits = new Dictionary<string, TestResultsReport>
    {
        {"Category_1", ExpectedReport_1_WithTraits }
    };
    #endregion

    //Expected Grouped Results 2
    #region Source 2 No Grouping
    public static readonly IEnumerable<TestResult> ExpectedResults_2_NoTraits = new List<TestResult>
    {
        new TestResult("Assembly_1_Component_1_Test_1", TestResultOutcomes.Passed, 0.0162284),
        new TestResult("Assembly_1_Component_1_Test_2", TestResultOutcomes.Failed, 0.0151508),
        new TestResult("Assembly_2_Component_1_Test_1", TestResultOutcomes.Passed, 0.0530211),
        new TestResult("Assembly_2_Component_1_Test_2", TestResultOutcomes.Failed, 0.0433452),
        new TestResult("Assembly_3_Component_1_Test_1", TestResultOutcomes.Passed, 0.3242322),
        new TestResult("Assembly_3_Component_1_Test_2", TestResultOutcomes.Failed, 0.3423426)
    };
   
    public readonly static TestResultsSummary ExpectedSummary_2 = new(6, 3, 3, .7943203000000001);
    public readonly static TestResultsReport ExpectedReport_2_NoTraits = new(ExpectedResults_2_NoTraits, ExpectedSummary_2);
    public readonly static IReadOnlyDictionary<string, TestResultsReport> ExpectedGroups_2_NoTraits = new Dictionary<string, TestResultsReport>
    {
        {"default", ExpectedReport_2_NoTraits }
    };


    #endregion

    #region Source 2 Grouping
    public static readonly TestResultsSummary ExpectedSummary_2_Group_1 = new(2, 1, 1, 0.0313792);
    public static readonly IEnumerable<TestResult> ExpectedResults_2_Group_1_WithTraits = new List<TestResult>
    {
        new TestResult("Assembly 1 Component 1 Test 1", TestResultOutcomes.Passed, 0.0162284),
        new TestResult("Assembly 1 Component 1 Test 2", TestResultOutcomes.Failed, 0.0151508)
    };
    public readonly static TestResultsReport ExpectedReport_2_Group_1_WithTraits = new(ExpectedResults_2_Group_1_WithTraits, ExpectedSummary_2_Group_1);

    public static readonly TestResultsSummary ExpectedSummary_2_Group_2 = new(2, 1, 1, 0.0963663);
    public readonly static IEnumerable<TestResult> ExpectedResults_2_Group_2_WithTraits = new List<TestResult>
    {
        new TestResult("Assembly 2 Component 1 Test 1", TestResultOutcomes.Passed, 0.0530211),
        new TestResult("Assembly 2 Component 1 Test 2", TestResultOutcomes.Failed, 0.0433452)
    };
    public static readonly TestResultsReport ExpectedReport_2_Group_2_WithTraits = new(ExpectedResults_2_Group_2_WithTraits, ExpectedSummary_2_Group_2);

    public readonly static TestResultsSummary ExpectedSummary_2_Group_3 = new(2, 1, 1, 0.6665748);
    public readonly static IEnumerable<TestResult> ExpectedResults_2_Group_3_WithTraits = new List<TestResult>
    {
        new TestResult("Assembly 3 Component 1 Test 1", TestResultOutcomes.Passed, 0.3242322),
        new TestResult("Assembly 3 Component 1 Test 2", TestResultOutcomes.Failed, 0.3423426)
    };
    public readonly static TestResultsReport ExpectedReport_2_Group_3_WithTraits = new(ExpectedResults_2_Group_3_WithTraits, ExpectedSummary_2_Group_3);

    public readonly static IReadOnlyDictionary<string, TestResultsReport> ExpectedGroups_2_WithTraits = new Dictionary<string, TestResultsReport>
    {
        {"Category_1", ExpectedReport_2_Group_1_WithTraits },
        {"Category_2", ExpectedReport_2_Group_2_WithTraits },
        {"Category_3", ExpectedReport_2_Group_3_WithTraits }
    };
    #endregion

    //Expected Grouped Results 3
    #region Source 3 No Grouping
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

    public readonly static TestResultsSummary ExpectedSummary_3 = new(11, 8, 3, 1.1020094);
    public readonly static TestResultsReport ExpectedReport_3_NoTraits = new(ExpectedResults_3_NoTraits, ExpectedSummary_3);
    public readonly static IReadOnlyDictionary<string, TestResultsReport> ExpectedGroups_3_NoTraits = new Dictionary<string, TestResultsReport>
    {
        {"default", ExpectedReport_3_NoTraits }
    };

    #endregion

    #region Source 3 Grouping
    public readonly static TestResultsSummary ExpectedSummary_3_Group_1_WithTraits = new(3, 3, 0, 0.27146349999999997);
    public static readonly IEnumerable<TestResult> ExpectedResults_3_Group_1_WithTraits = new List<TestResult>
    {
        new TestResult("Assembly 1 Component 1 Test 1", TestResultOutcomes.Passed, 0.0162284),
        new TestResult("Assembly 1 Component 2 Test 1", TestResultOutcomes.Passed, 0.1442009),
        new TestResult("Assembly 2 Component 2 Test 1", TestResultOutcomes.Passed, 0.1110342)
    };
    public readonly static TestResultsReport ExpectedReport_3_Group_1_WithTraits = new(ExpectedResults_3_Group_1_WithTraits, ExpectedSummary_3_Group_1_WithTraits);

    public readonly static TestResultsSummary ExpectedSummary_3_Group_2_WithTraits = new(3, 2, 1, 0.35212530000000003);
    public static readonly IEnumerable<TestResult> ExpectedResults_3_Group_2_WithTraits = new List<TestResult>
    {
        new TestResult("Assembly 1 Component 1 Test 2", TestResultOutcomes.Passed, 0.0151508),
        new TestResult("Assembly 1 Component 2 Test 2", TestResultOutcomes.Failed, 0.0127423),
        new TestResult("Assembly 3 Component 1 Test 1", TestResultOutcomes.Passed, 0.3242322)
    };
    public readonly static TestResultsReport ExpectedReport_3_Group_2_WithTraits = new(ExpectedResults_3_Group_2_WithTraits, ExpectedSummary_3_Group_2_WithTraits);

    public readonly static TestResultsSummary ExpectedSummary_3_Group_3_WithTraits = new(3, 2, 1, 0.4009874);
    public static readonly IEnumerable<TestResult> ExpectedResults_3_Group_3_WithTraits = new List<TestResult>
    {
        new TestResult("Assembly 1 Component 1 Test 3", TestResultOutcomes.Passed, 0.0056237),
        new TestResult("Assembly 2 Component 1 Test 1", TestResultOutcomes.Passed, 0.0530211),
        new TestResult("Assembly 3 Component 1 Test 2", TestResultOutcomes.Failed, 0.3423426)
    };
    public readonly static TestResultsReport ExpectedReport_3_Group_3_WithTraits = new(ExpectedResults_3_Group_3_WithTraits, ExpectedSummary_3_Group_3_WithTraits);

    public readonly static TestResultsSummary ExpectedSummary_3_Group_4_WithTraits = new(2, 1, 1, 0.077433200000000008);
    public static readonly IEnumerable<TestResult> ExpectedResults_3_Group_4_WithTraits = new List<TestResult>
    {
        new TestResult("Assembly 1 Component 1 Test 4", TestResultOutcomes.Passed, 0.0340880),
        new TestResult("Assembly 2 Component 1 Test 2", TestResultOutcomes.Failed, 0.0433452),
    };
    public readonly static TestResultsReport ExpectedReport_3_Group_4_WithTraits = new(ExpectedResults_3_Group_4_WithTraits, ExpectedSummary_3_Group_4_WithTraits);

    public readonly static IReadOnlyDictionary<string, TestResultsReport> ExpectedGroups_3_WithTraits = new Dictionary<string, TestResultsReport>
    {
        {"Category_1", ExpectedReport_3_Group_1_WithTraits },
        {"Category_2", ExpectedReport_3_Group_2_WithTraits },
        {"Category_3", ExpectedReport_3_Group_3_WithTraits },
        {"Category_4", ExpectedReport_3_Group_4_WithTraits }
    };

    #endregion

    //Expected Grouped Results 4
    public static readonly TestResultsSummary ExpectedSummary_4_Group_1 = new(2, 2, 0, 0.0313792);
    public static readonly IEnumerable<TestResult> ExpectedResults_4_Group_1 = new List<TestResult>
    {
        new TestResult("Assembly 1 Component 1 Test 1", TestResultOutcomes.Passed, 0.0162284),
        new TestResult("Assembly 1 Component 1 Test 2", TestResultOutcomes.Passed, 0.0151508)
    };
    public readonly static TestResultsReport ExpectedReport_4_Group_1 = new(ExpectedResults_4_Group_1, ExpectedSummary_4_Group_1);

    public static readonly TestResultsSummary ExpectedSummary_4_Group_2 = new(2, 2, 0, 0.0397117);
    public readonly static IEnumerable<TestResult> ExpectedResults_4_Group_2 = new List<TestResult>
    {
        new TestResult("Assembly 1 Component 1 Test 3", TestResultOutcomes.Passed, 0.0056237),
        new TestResult("Assembly 1 Component 1 Test 4", TestResultOutcomes.Passed, 0.0340880)
    };
    public static readonly TestResultsReport ExpectedReport_4_Group_2 = new(ExpectedResults_4_Group_2, ExpectedSummary_4_Group_2);

    public readonly static IReadOnlyDictionary<string, TestResultsReport> ExpectedGroups_4 = new Dictionary<string, TestResultsReport>
    {
        {"Category_1", ExpectedReport_4_Group_1 },
        {"default", ExpectedReport_4_Group_2 }
    };

    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { XUnitParserSampleSource.Content_1_NoTraits, ExpectedGroups_1_NoTraits };
        yield return new object[] { XUnitParserSampleSource.Content_1_WithTraits, ExpectedGroups_1_WithTraits };
        yield return new object[] { XUnitParserSampleSource.Content_2_NoTraits, ExpectedGroups_2_NoTraits };
        yield return new object[] { XUnitParserSampleSource.Content_2_WithTraits, ExpectedGroups_2_WithTraits };
        yield return new object[] { XUnitParserSampleSource.Content_3_NoTraits, ExpectedGroups_3_NoTraits };
        yield return new object[] { XUnitParserSampleSource.Content_3_WithTraits, ExpectedGroups_3_WithTraits };
        yield return new object[] { XUnitParserSampleSource.Content_4_With_Some_Traits_And_NoTraits, ExpectedGroups_4 };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
