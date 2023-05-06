namespace Carlton.Base.Infrastructure.Test.UnitTesting.Trx;
public static class TrxParserSampleSources
{
    public const string ExceptionMessage = "Content is not a valid Trx file.";

    public const string Category1 = "Category1";
    public const string Category2 = "Category2";
    public const string Category3 = "Category3";
    public const string Default = "default";

    public const string TestName_1 = "Unit Test 1";
    public const string TestName_2 = "Unit Test 2";
    public const string TestName_3 = "Unit Test 3";
    public const string TestName_4 = "Unit Test 4";

    public const double Duration_1 = 3.47;
    public const double Duration_2 = .3;
    public const double Duration_3 = .07;
    public const double Duration_4 = .06;

    public readonly static TestResultOutcomes TestOutcome_1 = TestResultOutcomes.Passed;
    public readonly static TestResultOutcomes TestOutcome_2 = TestResultOutcomes.Passed;
    public readonly static TestResultOutcomes TestOutcome_3 = TestResultOutcomes.Failed;
    public readonly static TestResultOutcomes TestOutcome_4 = TestResultOutcomes.Passed;

    public readonly static string UnitTestResult_1 = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TestRun id=""28934b66-6769-449d-bcf9-2d05352fddba"" name=""nicho@DESKTOP-HE04GST 2023-05-05 22:03:38"" runUser=""DESKTOP-HE04GST\nicho"" xmlns=""http://microsoft.com/schemas/VisualStudio/TeamTest/2010"">
  <Times creation=""2023-05-05T22:03:38.0433248-04:00"" queuing=""2023-05-05T22:03:38.0433254-04:00"" start=""2023-05-05T22:03:36.6976425-04:00"" finish=""2023-05-05T22:03:38.0586462-04:00"" />
  <TestSettings name=""default"" id=""ff27a4ef-f087-4ab4-bbaf-ddb7b57fdca4"">
    <Deployment runDeploymentRoot=""nicho_DESKTOP-HE04GST_2023-05-05_22_03_38"" />
  </TestSettings>
  <Results>
    <UnitTestResult executionId=""495e6dc3-1f51-48c5-8f80-d00a5d858edc"" testId=""f33d86a3-940c-988c-8be3-1332c3ee8518"" testName=""Unit Test 1"" computerName=""DESKTOP-HE04GST"" duration=""00:00:00.0034707"" startTime=""2023-05-05T22:03:37.7319523-04:00"" endTime=""2023-05-05T22:03:37.7522624-04:00"" testType=""13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b"" outcome=""Passed"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" relativeResultsDirectory=""495e6dc3-1f51-48c5-8f80-d00a5d858edc"" />
    <UnitTestResult executionId=""b89e1feb-2a41-4620-91d5-66e5fb2396a5"" testId=""aaa24891-4b35-0f41-4ce9-a9b9856b4cb1"" testName=""Unit Test 2"" computerName=""DESKTOP-HE04GST"" duration=""00:00:00.0002958"" startTime=""2023-05-05T22:03:37.7602414-04:00"" endTime=""2023-05-05T22:03:37.7608119-04:00"" testType=""13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b"" outcome=""Passed"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" relativeResultsDirectory=""b89e1feb-2a41-4620-91d5-66e5fb2396a5"" />
    <UnitTestResult executionId=""d6aed5ea-7b00-44b5-87f6-635110e8bc65"" testId=""3f23c3cc-c264-d433-3d28-971f0e00d01a"" testName=""Unit Test 3"" computerName=""DESKTOP-HE04GST"" duration=""00:00:00.0000677"" startTime=""2023-05-05T22:03:37.7614927-04:00"" endTime=""2023-05-05T22:03:37.7616622-04:00"" testType=""13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b"" outcome=""Failed"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" relativeResultsDirectory=""d6aed5ea-7b00-44b5-87f6-635110e8bc65"" />   
    <UnitTestResult executionId=""0e51c9fe-120a-4ede-b641-308523ad9adc"" testId=""5589c7b8-8d4f-b4e1-9c9f-6114b11ba992"" testName=""Unit Test 4"" computerName=""DESKTOP-HE04GST"" duration=""00:00:00.0000611"" startTime=""2023-05-05T22:03:37.7617013-04:00"" endTime=""2023-05-05T22:03:37.7620276-04:00"" testType=""13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b"" outcome=""Passed"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" relativeResultsDirectory=""0e51c9fe-120a-4ede-b641-308523ad9adc"" />
  </Results>
  <TestDefinitions>
    <UnitTest name=""Unit Test 1"" storage=""c:\users\nicho\source\repos\nickgalazzo\carlton.base\testproject1\bin\debug\net7.0\testproject1.dll"" id=""f33d86a3-940c-988c-8be3-1332c3ee8518"">
      <TestCategory>
        <TestCategoryItem TestCategory=""Category1"" />
        <TestCategoryItem TestCategory=""Category2"" />
      </TestCategory>
      <Execution id=""495e6dc3-1f51-48c5-8f80-d00a5d858edc"" />
      <TestMethod codeBase=""C:\Users\nicho\source\repos\NickGalazzo\Carlton.Base\TestProject1\bin\Debug\net7.0\TestProject1.dll"" adapterTypeName=""executor://mstestadapter/v2"" className=""TestProject1.UnitTest1"" name=""TestMethod1"" />
    </UnitTest>
    <UnitTest name=""Unit Test 2"" storage=""c:\users\nicho\source\repos\nickgalazzo\carlton.base\testproject1\bin\debug\net7.0\testproject1.dll"" id=""aaa24891-4b35-0f41-4ce9-a9b9856b4cb1"">
      <TestCategory>
        <TestCategoryItem TestCategory=""Category2"" />
        <TestCategoryItem TestCategory=""Category1"" />
      </TestCategory>
      <Execution id=""b89e1feb-2a41-4620-91d5-66e5fb2396a5"" />
      <TestMethod codeBase=""C:\Users\nicho\source\repos\NickGalazzo\Carlton.Base\TestProject1\bin\Debug\net7.0\TestProject1.dll"" adapterTypeName=""executor://mstestadapter/v2"" className=""TestProject1.UnitTest1"" name=""TestMethod2"" />
    </UnitTest>
    <UnitTest name=""Unit Test 3"" storage=""c:\users\nicho\source\repos\nickgalazzo\carlton.base\testproject1\bin\debug\net7.0\testproject1.dll"" id=""3f23c3cc-c264-d433-3d28-971f0e00d01a"">
      <TestCategory>
        <TestCategoryItem TestCategory=""Category3"" />
      </TestCategory>
      <Execution id=""d6aed5ea-7b00-44b5-87f6-635110e8bc65"" />
      <TestMethod codeBase=""C:\Users\nicho\source\repos\NickGalazzo\Carlton.Base\TestProject1\bin\Debug\net7.0\TestProject1.dll"" adapterTypeName=""executor://mstestadapter/v2"" className=""TestProject1.UnitTest1"" name=""TestMethod3"" />
    </UnitTest> 
    <UnitTest name=""Unit Test 4"" storage=""c:\users\nicho\source\repos\nickgalazzo\carlton.base\testproject1\bin\debug\net7.0\testproject1.dll"" id=""5589c7b8-8d4f-b4e1-9c9f-6114b11ba992"">
      <Execution id=""0e51c9fe-120a-4ede-b641-308523ad9adc"" />
      <TestMethod codeBase=""C:\Users\nicho\source\repos\NickGalazzo\Carlton.Base\TestProject1\bin\Debug\net7.0\TestProject1.dll"" adapterTypeName=""executor://mstestadapter/v2"" className=""TestProject1.UnitTest1"" name=""TestMethod4"" />
    </UnitTest>
  </TestDefinitions>
  <TestEntries>
    <TestEntry testId=""f33d86a3-940c-988c-8be3-1332c3ee8518"" executionId=""495e6dc3-1f51-48c5-8f80-d00a5d858edc"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" />
    <TestEntry testId=""5589c7b8-8d4f-b4e1-9c9f-6114b11ba992"" executionId=""0e51c9fe-120a-4ede-b641-308523ad9adc"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" />
    <TestEntry testId=""aaa24891-4b35-0f41-4ce9-a9b9856b4cb1"" executionId=""b89e1feb-2a41-4620-91d5-66e5fb2396a5"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" />
    <TestEntry testId=""3f23c3cc-c264-d433-3d28-971f0e00d01a"" executionId=""d6aed5ea-7b00-44b5-87f6-635110e8bc65"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" />
  </TestEntries>
  <TestLists>
    <TestList name=""Results Not in a List"" id=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" />
    <TestList name=""All Loaded Results"" id=""19431567-8539-422a-85d7-44ee4e166bda"" />
  </TestLists>
  <ResultSummary outcome=""Completed"">
    <Counters total=""4"" executed=""4"" passed=""4"" failed=""0"" error=""0"" timeout=""0"" aborted=""0"" inconclusive=""0"" passedButRunAborted=""0"" notRunnable=""0"" notExecuted=""0"" disconnected=""0"" warning=""0"" completed=""0"" inProgress=""0"" pending=""0"" />
  </ResultSummary>
</TestRun>";

    public readonly static string UnitTestResult_2 = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TestRun id=""28934b66-6769-449d-bcf9-2d05352fddba"" name=""nicho@DESKTOP-HE04GST 2023-05-05 22:03:38"" runUser=""DESKTOP-HE04GST\nicho"" xmlns=""http://microsoft.com/schemas/VisualStudio/TeamTest/2010"">
  <Times creation=""2023-05-05T22:03:38.0433248-04:00"" queuing=""2023-05-05T22:03:38.0433254-04:00"" start=""2023-05-05T22:03:36.6976425-04:00"" finish=""2023-05-05T22:03:38.0586462-04:00"" />
  <TestSettings name=""default"" id=""ff27a4ef-f087-4ab4-bbaf-ddb7b57fdca4"">
    <Deployment runDeploymentRoot=""nicho_DESKTOP-HE04GST_2023-05-05_22_03_38"" />
  </TestSettings>
  <Results>
    <UnitTestResult executionId=""495e6dc3-1f51-48c5-8f80-d00a5d858edc"" testId=""f33d86a3-940c-988c-8be3-1332c3ee8518"" testName=""Unit Test 1"" computerName=""DESKTOP-HE04GST"" duration=""00:00:00.0034707"" startTime=""2023-05-05T22:03:37.7319523-04:00"" endTime=""2023-05-05T22:03:37.7522624-04:00"" testType=""13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b"" outcome=""Passed"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" relativeResultsDirectory=""495e6dc3-1f51-48c5-8f80-d00a5d858edc"" />
    <UnitTestResult executionId=""0e51c9fe-120a-4ede-b641-308523ad9adc"" testId=""5589c7b8-8d4f-b4e1-9c9f-6114b11ba992"" testName=""Unit Test 4"" computerName=""DESKTOP-HE04GST"" duration=""00:00:00.0000611"" startTime=""2023-05-05T22:03:37.7617013-04:00"" endTime=""2023-05-05T22:03:37.7620276-04:00"" testType=""13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b"" outcome=""Passed"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" relativeResultsDirectory=""0e51c9fe-120a-4ede-b641-308523ad9adc"" />
  </Results>
  <TestDefinitions>
    <UnitTest name=""Unit Test 1"" storage=""c:\users\nicho\source\repos\nickgalazzo\carlton.base\testproject1\bin\debug\net7.0\testproject1.dll"" id=""f33d86a3-940c-988c-8be3-1332c3ee8518"">
      <TestCategory>
        <TestCategoryItem TestCategory=""Category2"" />
        <TestCategoryItem TestCategory=""Category1"" />
      </TestCategory>
      <Execution id=""495e6dc3-1f51-48c5-8f80-d00a5d858edc"" />
      <TestMethod codeBase=""C:\Users\nicho\source\repos\NickGalazzo\Carlton.Base\TestProject1\bin\Debug\net7.0\TestProject1.dll"" adapterTypeName=""executor://mstestadapter/v2"" className=""TestProject1.UnitTest1"" name=""TestMethod1"" />
    </UnitTest>
    <UnitTest name=""Unit Test 4"" storage=""c:\users\nicho\source\repos\nickgalazzo\carlton.base\testproject1\bin\debug\net7.0\testproject1.dll"" id=""5589c7b8-8d4f-b4e1-9c9f-6114b11ba992"">
      <Execution id=""0e51c9fe-120a-4ede-b641-308523ad9adc"" />
      <TestMethod codeBase=""C:\Users\nicho\source\repos\NickGalazzo\Carlton.Base\TestProject1\bin\Debug\net7.0\TestProject1.dll"" adapterTypeName=""executor://mstestadapter/v2"" className=""TestProject1.UnitTest1"" name=""TestMethod4"" />
    </UnitTest>
  </TestDefinitions>
  <TestEntries>
    <TestEntry testId=""f33d86a3-940c-988c-8be3-1332c3ee8518"" executionId=""495e6dc3-1f51-48c5-8f80-d00a5d858edc"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" />
    <TestEntry testId=""5589c7b8-8d4f-b4e1-9c9f-6114b11ba992"" executionId=""0e51c9fe-120a-4ede-b641-308523ad9adc"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" />
    <TestEntry testId=""aaa24891-4b35-0f41-4ce9-a9b9856b4cb1"" executionId=""b89e1feb-2a41-4620-91d5-66e5fb2396a5"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" />
    <TestEntry testId=""3f23c3cc-c264-d433-3d28-971f0e00d01a"" executionId=""d6aed5ea-7b00-44b5-87f6-635110e8bc65"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" />
  </TestEntries>
  <TestLists>
    <TestList name=""Results Not in a List"" id=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" />
    <TestList name=""All Loaded Results"" id=""19431567-8539-422a-85d7-44ee4e166bda"" />
  </TestLists>
  <ResultSummary outcome=""Completed"">
    <Counters total=""4"" executed=""4"" passed=""4"" failed=""0"" error=""0"" timeout=""0"" aborted=""0"" inconclusive=""0"" passedButRunAborted=""0"" notRunnable=""0"" notExecuted=""0"" disconnected=""0"" warning=""0"" completed=""0"" inProgress=""0"" pending=""0"" />
  </ResultSummary>
</TestRun>";

    public readonly static string UnitTestResult_3 = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TestRun id=""28934b66-6769-449d-bcf9-2d05352fddba"" name=""nicho@DESKTOP-HE04GST 2023-05-05 22:03:38"" runUser=""DESKTOP-HE04GST\nicho"" xmlns=""http://microsoft.com/schemas/VisualStudio/TeamTest/2010"">
  <Times creation=""2023-05-05T22:03:38.0433248-04:00"" queuing=""2023-05-05T22:03:38.0433254-04:00"" start=""2023-05-05T22:03:36.6976425-04:00"" finish=""2023-05-05T22:03:38.0586462-04:00"" />
  <TestSettings name=""default"" id=""ff27a4ef-f087-4ab4-bbaf-ddb7b57fdca4"">
    <Deployment runDeploymentRoot=""nicho_DESKTOP-HE04GST_2023-05-05_22_03_38"" />
  </TestSettings>
  <Results>
    <UnitTestResult executionId=""495e6dc3-1f51-48c5-8f80-d00a5d858edc"" testId=""f33d86a3-940c-988c-8be3-1332c3ee8518"" testName=""Unit Test 1"" computerName=""DESKTOP-HE04GST"" duration=""00:00:00.0034707"" startTime=""2023-05-05T22:03:37.7319523-04:00"" endTime=""2023-05-05T22:03:37.7522624-04:00"" testType=""13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b"" outcome=""Passed"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" relativeResultsDirectory=""495e6dc3-1f51-48c5-8f80-d00a5d858edc"" />
    <UnitTestResult executionId=""b89e1feb-2a41-4620-91d5-66e5fb2396a5"" testId=""aaa24891-4b35-0f41-4ce9-a9b9856b4cb1"" testName=""Unit Test 2"" computerName=""DESKTOP-HE04GST"" duration=""00:00:00.0002958"" startTime=""2023-05-05T22:03:37.7602414-04:00"" endTime=""2023-05-05T22:03:37.7608119-04:00"" testType=""13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b"" outcome=""Passed"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" relativeResultsDirectory=""b89e1feb-2a41-4620-91d5-66e5fb2396a5"" />
    <UnitTestResult executionId=""d6aed5ea-7b00-44b5-87f6-635110e8bc65"" testId=""3f23c3cc-c264-d433-3d28-971f0e00d01a"" testName=""Unit Test 3"" computerName=""DESKTOP-HE04GST"" duration=""00:00:00.0000677"" startTime=""2023-05-05T22:03:37.7614927-04:00"" endTime=""2023-05-05T22:03:37.7616622-04:00"" testType=""13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b"" outcome=""Failed"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" relativeResultsDirectory=""d6aed5ea-7b00-44b5-87f6-635110e8bc65"" />   
  </Results>
  <TestDefinitions>
    <UnitTest name=""Unit Test 1"" storage=""c:\users\nicho\source\repos\nickgalazzo\carlton.base\testproject1\bin\debug\net7.0\testproject1.dll"" id=""f33d86a3-940c-988c-8be3-1332c3ee8518"">
      <TestCategory>
        <TestCategoryItem TestCategory=""Category2"" />
        <TestCategoryItem TestCategory=""Category1"" />
      </TestCategory>
      <Execution id=""495e6dc3-1f51-48c5-8f80-d00a5d858edc"" />
      <TestMethod codeBase=""C:\Users\nicho\source\repos\NickGalazzo\Carlton.Base\TestProject1\bin\Debug\net7.0\TestProject1.dll"" adapterTypeName=""executor://mstestadapter/v2"" className=""TestProject1.UnitTest1"" name=""TestMethod1"" />
    </UnitTest>
    <UnitTest name=""Unit Test 2"" storage=""c:\users\nicho\source\repos\nickgalazzo\carlton.base\testproject1\bin\debug\net7.0\testproject1.dll"" id=""aaa24891-4b35-0f41-4ce9-a9b9856b4cb1"">
      <TestCategory>
        <TestCategoryItem TestCategory=""Category2"" />
        <TestCategoryItem TestCategory=""Category1"" />
      </TestCategory>
      <Execution id=""b89e1feb-2a41-4620-91d5-66e5fb2396a5"" />
      <TestMethod codeBase=""C:\Users\nicho\source\repos\NickGalazzo\Carlton.Base\TestProject1\bin\Debug\net7.0\TestProject1.dll"" adapterTypeName=""executor://mstestadapter/v2"" className=""TestProject1.UnitTest1"" name=""TestMethod2"" />
    </UnitTest>
    <UnitTest name=""Unit Test 3"" storage=""c:\users\nicho\source\repos\nickgalazzo\carlton.base\testproject1\bin\debug\net7.0\testproject1.dll"" id=""3f23c3cc-c264-d433-3d28-971f0e00d01a"">
      <TestCategory>
        <TestCategoryItem TestCategory=""Category3"" />
      </TestCategory>
      <Execution id=""d6aed5ea-7b00-44b5-87f6-635110e8bc65"" />
      <TestMethod codeBase=""C:\Users\nicho\source\repos\NickGalazzo\Carlton.Base\TestProject1\bin\Debug\net7.0\TestProject1.dll"" adapterTypeName=""executor://mstestadapter/v2"" className=""TestProject1.UnitTest1"" name=""TestMethod3"" />
    </UnitTest> 
  </TestDefinitions>
  <TestEntries>
    <TestEntry testId=""f33d86a3-940c-988c-8be3-1332c3ee8518"" executionId=""495e6dc3-1f51-48c5-8f80-d00a5d858edc"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" />
    <TestEntry testId=""5589c7b8-8d4f-b4e1-9c9f-6114b11ba992"" executionId=""0e51c9fe-120a-4ede-b641-308523ad9adc"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" />
    <TestEntry testId=""aaa24891-4b35-0f41-4ce9-a9b9856b4cb1"" executionId=""b89e1feb-2a41-4620-91d5-66e5fb2396a5"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" />
    <TestEntry testId=""3f23c3cc-c264-d433-3d28-971f0e00d01a"" executionId=""d6aed5ea-7b00-44b5-87f6-635110e8bc65"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" />
  </TestEntries>
  <TestLists>
    <TestList name=""Results Not in a List"" id=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" />
    <TestList name=""All Loaded Results"" id=""19431567-8539-422a-85d7-44ee4e166bda"" />
  </TestLists>
  <ResultSummary outcome=""Completed"">
    <Counters total=""4"" executed=""4"" passed=""4"" failed=""0"" error=""0"" timeout=""0"" aborted=""0"" inconclusive=""0"" passedButRunAborted=""0"" notRunnable=""0"" notExecuted=""0"" disconnected=""0"" warning=""0"" completed=""0"" inProgress=""0"" pending=""0"" />
  </ResultSummary>
</TestRun>";
}
