namespace Carlton.Core.Infrastructure.Test.UnitTesting.XUnit;

public static class XUnitParserSampleSource
{
    public const string Category1 = "Category 1";
    public const string Category2 = "Category 2";
    public const string Category3 = "Category 3";
    public const string Default = "default";

    public const string TestName_1 = "Unit Test 1";
    public const string TestName_2 = "Unit Test 2";
    public const string TestName_3 = "Unit Test 3";
    public const string TestName_4 = "Unit Test 4";
    public const string TestName_5 = "Unit Test 5";

    public const double Duration_1 = 0.0162284;
    public const double Duration_2 = 0.0151508;
    public const double Duration_3 = 0.1442009;
    public const double Duration_4 = 0.0127423;
    public const double Duration_5 = 0.0405402;

    public readonly static TestResultOutcomes TestOutcome_1 = TestResultOutcomes.Passed;
    public readonly static TestResultOutcomes TestOutcome_2 = TestResultOutcomes.Passed;
    public readonly static TestResultOutcomes TestOutcome_3 = TestResultOutcomes.Passed;
    public readonly static TestResultOutcomes TestOutcome_4 = TestResultOutcomes.Failed;
    public readonly static TestResultOutcomes TestOutcome_5 = TestResultOutcomes.Passed;


    public const string Content =
        @"<assemblies timestamp=""05/02/2023 22:57:51"">
  <assembly name = ""C:\Test\Assembly1.Test.dll"" run-date=""2023-05-02"" run-time=""22:57:51"" total=""335"" passed=""333"" failed=""2"" skipped=""0"" time=""7.446"" errors=""0"">
    <errors />
    <collection total = ""4"" passed=""4"" failed=""0"" skipped=""0"" name=""Test Component 1"" time=""0.071"">
      <test name = ""Unit Test 1"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test1"" time=""0.0162284"" result=""Pass"">
        <traits>
            <trait name=""Category"" value=""Category 1"" />
            <trait name=""Category"" value=""Category 2"" />
        </traits>
      </test>
      <test name = ""Unit Test 2"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test2"" time=""0.0151508"" result=""Pass"">
        <traits>
            <trait name=""Category"" value=""Category 2"" />
            <trait name=""Category"" value=""Category 1"" />
        </traits>
      </test>
    </collection>
    <collection total = ""2"" passed=""1"" failed=""1"" skipped=""0"" name=""Test Component 2"" time=""0.407"">
      <test name = ""Unit Test 3"" type=""Assembly1.Test.Component2"" method=""Assembly1TestComponent2Test1"" time=""0.1442009"" result=""Pass"">
        <traits>
            <trait name=""Category"" value=""Category 1"" />
        </traits>
      </test>
      <test name = ""Unit Test 4"" type=""Assembly1.Test.Component2"" method=""Assembly1TestComponent2Test2"" time=""0.0127423"" result=""Fail"">
        <failure>
          <message>Some Error Message</message>
          <stack-trace>Some Stack Trace</stack-trace>
        </failure>
        <traits>   
        </traits>
      </test>
    </collection>
    </assembly>
    <assembly name = ""C:\Test\Assembly3.Test.dll"" run-date=""2023-05-02"" run-time=""22:57:51"">
        <collection total = ""1"" passed=""1"" failed=""0"" skipped=""0"" name=""Test Component 3"" time=""0.309"">
            <test name = ""Unit Test 5"" type=""Assembly1.Test.Component3"" method=""Assembly1TestComponent3Test1"" time=""0.0405402"" result=""Pass"">
                <traits>
                    <trait name=""Category"" value=""Category 3"" />
                </traits>
            </test>
        </collection>
    </assembly>
</assemblies>";
}