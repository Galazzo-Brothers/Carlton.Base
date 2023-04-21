namespace Carlton.Base.TestBed.Test;

public static class TrxTestHelper
{
    public const string ExceptionMessage = "Content is not a valid Trx file.";

    public const string StartDate_1 = "2012-02-19T09:25:25.2569272-05:00";
    public const string EndDate_1 = "2012-02-19T09:25:26.0819744-05:00";
    public const double SummaryDuration_1 = .83;
    public const string StartDate_2 = "2012-02-19T09:25:25.2569272-05:00";
    public const string EndDate_2 = "2012-02-19T09:25:27.0819744-05:00";
    public const double SummaryDuration_2 = 1.83;
    public const string StartDate_3 = "2012-02-19T09:25:25.15-05:00";
    public const string EndDate_3 = "2012-02-19T09:25:27.092-05:00";
    public const double SummaryDuration_3 = 1.94;

    public const string TestName_1 = "TestComponent_Test1_ShouldDoAThing";
    public const string TestName_2 = "TestComponent_Test2_ShouldDoADifferentThing";
    public const string TestName_3 = "TestComponent_Test3_ShouldNotDoAThing";
    public const string TestName_4 = "TestComponent_Test4_ShouldNotDoADifferentThing";

    public const string TestDuration_1 = "00:00:00.0768910";
    public const string TestDuration_2 = "00:00:00.0111534";
    public const string TestDuration_3 = "00:00:00.0963110";
    public const string TestDuration_4 = "00:00:00.0541230";

    public readonly static double TestDurationResult_1 = Math.Round(TimeSpan.Parse(TestDuration_1).TotalMilliseconds, 2);
    public readonly static double TestDurationResult_2 = Math.Round(TimeSpan.Parse(TestDuration_2).TotalMilliseconds, 2);
    public readonly static double TestDurationResult_3 = Math.Round(TimeSpan.Parse(TestDuration_3).TotalMilliseconds, 2);
    public readonly static double TestDurationResult_4 = Math.Round(TimeSpan.Parse(TestDuration_4).TotalMilliseconds, 2);

    public readonly static TestResultOutcomes TestResult_1 = TestResultOutcomes.Passed;
    public readonly static TestResultOutcomes TestResult_2 = TestResultOutcomes.Passed;
    public readonly static TestResultOutcomes TestResult_3 = TestResultOutcomes.Failed;
    public readonly static TestResultOutcomes TestResult_4 = TestResultOutcomes.Passed;

    public readonly static string UnitTestResult_1 = @$"
    <UnitTestResult executionId=""d7b75d23-a952-4578-8542-326abd65c695"" testId=""272c9292-71fc-4aaa-5ed5-d7869de15ae4"" testName=""{TestName_1}"" computerName=""BUMBLEBEE"" duration=""00:00:00.0768910"" startTime=""2012-02-19T09:25:25.2729281-05:00"" endTime=""2012-02-19T09:25:25.9379661-05:00"" testType=""13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b"" outcome=""{TestResult_1}"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" relativeResultsDirectory=""d7b75d23-a952-4578-8542-326abd65c695"">
      <Output>
        <StdOut>
          Given I have entered 40 into the calculator
          -&gt; done: Steps.GivenIHaveEnteredSomethingIntoTheCalculator(40) (0.0s)
          And I have entered 50 into the calculator
          -&gt; done: Steps.GivenIHaveEnteredSomethingIntoTheCalculator(50) (0.0s)
          When I press add
          -&gt; done: Steps.WhenIPressAdd() (0.0s)
          Then the result should be 90 on the screen
          -&gt; done: Steps.ThenTheResultShouldBePass(90) (0.0s)
        </StdOut>
      </Output>
    </UnitTestResult>";
    public readonly static string UnitTestResult_2 = @$"
    <UnitTestResult executionId=""d7b75d23-a952-4578-8542-326abd65c695"" testId=""272c9292-71fc-4aaa-5ed5-d7869de15ae4"" testName=""{TestName_1}"" computerName=""BUMBLEBEE"" duration=""{TestDuration_1}"" startTime=""2012-02-19T09:25:25.2729281-05:00"" endTime=""2012-02-19T09:25:25.9379661-05:00"" testType=""13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b"" outcome=""{TestResult_1}"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" relativeResultsDirectory=""d7b75d23-a952-4578-8542-326abd65c695"">
      <Output>
        <StdOut>
          Given I have entered 40 into the calculator
          -&gt; done: Steps.GivenIHaveEnteredSomethingIntoTheCalculator(40) (0.0s)
          And I have entered 50 into the calculator
          -&gt; done: Steps.GivenIHaveEnteredSomethingIntoTheCalculator(50) (0.0s)
          When I press add
          -&gt; done: Steps.WhenIPressAdd() (0.0s)
          Then the result should be 90 on the screen
          -&gt; done: Steps.ThenTheResultShouldBePass(90) (0.0s)
        </StdOut>
      </Output>
    </UnitTestResult>
    <UnitTestResult executionId = ""0f00d527-6edd-4045-b9b5-65ebce5b3874"" testId=""0156926a-5b5c-a0f3-47b4-9cf23f44894b"" testName=""{TestName_2}"" computerName=""BUMBLEBEE"" duration=""{TestDuration_2}"" startTime=""2012-02-19T09:25:25.9399663-05:00"" endTime=""2012-02-19T09:25:25.9719681-05:00"" testType=""13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b"" outcome=""{TestResult_2}"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" relativeResultsDirectory=""0f00d527-6edd-4045-b9b5-65ebce5b3874"">
      <Output>
        <StdOut>
          Given I have entered 60 into the calculator
          -&gt; done: Steps.GivenIHaveEnteredSomethingIntoTheCalculator(60) (0.0s)
          And I have entered 70 into the calculator
          -&gt; done: Steps.GivenIHaveEnteredSomethingIntoTheCalculator(70) (0.0s)
          When I press add
          -&gt; done: Steps.WhenIPressAdd() (0.0s)
          Then the result should be 130 on the screen
          -&gt; done: Steps.ThenTheResultShouldBePass(130) (0.0s)
        </StdOut>
      </Output>
    </UnitTestResult>";
    public readonly static string UnitTestResult_3 = @$"
    <UnitTestResult executionId=""d7b75d23-a952-4578-8542-326abd65c695"" testId=""272c9292-71fc-4aaa-5ed5-d7869de15ae4"" testName=""{TestName_1}"" computerName=""BUMBLEBEE"" duration=""{TestDuration_1}"" startTime=""2012-02-19T09:25:25.2729281-05:00"" endTime=""2012-02-19T09:25:25.9379661-05:00"" testType=""13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b"" outcome=""{TestResult_1}"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" relativeResultsDirectory=""d7b75d23-a952-4578-8542-326abd65c695"">
      <Output>
        <StdOut>
          Given I have entered 40 into the calculator
          -&gt; done: Steps.GivenIHaveEnteredSomethingIntoTheCalculator(40) (0.0s)
          And I have entered 50 into the calculator
          -&gt; done: Steps.GivenIHaveEnteredSomethingIntoTheCalculator(50) (0.0s)
          When I press add
          -&gt; done: Steps.WhenIPressAdd() (0.0s)
          Then the result should be 90 on the screen
          -&gt; done: Steps.ThenTheResultShouldBePass(90) (0.0s)
        </StdOut>
      </Output>
    </UnitTestResult>
    <UnitTestResult executionId = ""0f00d527-6edd-4045-b9b5-65ebce5b3874"" testId=""0156926a-5b5c-a0f3-47b4-9cf23f44894b"" testName=""{TestName_2}"" computerName=""BUMBLEBEE"" duration=""{TestDuration_2}"" startTime=""2012-02-19T09:25:25.9399663-05:00"" endTime=""2012-02-19T09:25:25.9719681-05:00"" testType=""13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b"" outcome=""{TestResult_2}"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" relativeResultsDirectory=""0f00d527-6edd-4045-b9b5-65ebce5b3874"">
      <Output>
        <StdOut>
          Given I have entered 60 into the calculator
          -&gt; done: Steps.GivenIHaveEnteredSomethingIntoTheCalculator(60) (0.0s)
          And I have entered 70 into the calculator
          -&gt; done: Steps.GivenIHaveEnteredSomethingIntoTheCalculator(70) (0.0s)
          When I press add
          -&gt; done: Steps.WhenIPressAdd() (0.0s)
          Then the result should be 130 on the screen
          -&gt; done: Steps.ThenTheResultShouldBePass(130) (0.0s)
        </StdOut>
      </Output>
    </UnitTestResult>
    <UnitTestResult executionId = ""17356f2f-b3c9-4bee-b6b3-74fe17b2e82b"" testId=""45571f70-456e-69eb-dbf2-7ec265c4b751"" testName=""{TestName_3}"" computerName=""BUMBLEBEE"" duration=""{TestDuration_3}"" startTime=""2012-02-19T09:25:25.9799685-05:00"" endTime=""2012-02-19T09:25:25.9919692-05:00"" testType=""13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b"" outcome=""{TestResult_3}"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" relativeResultsDirectory=""17356f2f-b3c9-4bee-b6b3-74fe17b2e82b"">
      <Output>
        <StdOut>
          Given I have entered 50 into the calculator
          -&gt; done: Steps.GivenIHaveEnteredSomethingIntoTheCalculator(50) (0.0s)
          And I have entered 70 into the calculator
          -&gt; done: Steps.GivenIHaveEnteredSomethingIntoTheCalculator(70) (0.0s)
          When I press add
          -&gt; done: Steps.WhenIPressAdd() (0.0s)
          Then the result should be 120 on the screen
          -&gt; done: Steps.ThenTheResultShouldBePass(120) (0.0s)
        </StdOut>
      </Output>
    </UnitTestResult>
    <UnitTestResult executionId = ""6a02f552-17f1-4f86-9eaa-bcddec97f075"" testId=""10684beb-1457-c825-5087-2d0029460463"" testName=""{TestName_4}"" computerName=""BUMBLEBEE"" duration=""{TestDuration_4}"" startTime=""2012-02-19T09:25:25.9949694-05:00"" endTime=""2012-02-19T09:25:26.0469724-05:00"" testType=""13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b"" outcome=""{TestResult_4}"" testListId=""8c84fa94-04c1-424b-9868-57a2d4851a1d"" relativeResultsDirectory=""6a02f552-17f1-4f86-9eaa-bcddec97f075"">
      <Output>
        <StdOut>
          Given I have entered 50 into the calculator
          -&gt; done: Steps.GivenIHaveEnteredSomethingIntoTheCalculator(50) (0.0s)
          And I have entered -1 into the calculator
          -&gt; done: Steps.GivenIHaveEnteredSomethingIntoTheCalculator(-1) (0.0s)
          When I press add
          -&gt; done: Steps.WhenIPressAdd() (0.0s)
          Then the result should be -50 on the screen
          -&gt; error: Assert.NotEqual() Failure
        </StdOut>
        <ErrorInfo>
          <Message>
            Test method Pickles.TestHarness.MSTest.AdditionFeature.FailToAddTwoNumbers threw exception:
            Should.Core.Exceptions.NotEqualException: Assert.NotEqual() Failure
          </Message>
          <StackTrace>
            at Pickles.TestHarness.MSTest.Steps.ThenTheResultShouldBePass(Int32 result) in C:\dev\pickles-results-harness\Pickles.TestHarness\Pickles.TestHarness.MSTest\Steps.cs:line 28
            at lambda_method(Closure, IContextManager, Int32)
            at TechTalk.SpecFlow.Bindings.MethodBinding.InvokeAction(IContextManager contextManager, Object[] arguments, ITestTracer testTracer, TimeSpan&amp; duration)
            at TechTalk.SpecFlow.Bindings.StepDefinitionBinding.Invoke(IContextManager contextManager, ITestTracer testTracer, Object[] arguments, TimeSpan&amp; duration)
            at TechTalk.SpecFlow.Infrastructure.TestExecutionEngine.ExecuteStepMatch(BindingMatch match, Object[] arguments)
            at TechTalk.SpecFlow.Infrastructure.TestExecutionEngine.ExecuteStep(StepArgs stepArgs)
            at TechTalk.SpecFlow.Infrastructure.TestExecutionEngine.OnAfterLastStep()
            at TechTalk.SpecFlow.TestRunner.CollectScenarioErrors()
            at Pickles.TestHarness.MSTest.AdditionFeature.ScenarioCleanup() in C:\dev\pickles-results-harness\Pickles.TestHarness\Pickles.TestHarness.MSTest\Addition.feature.cs:line 0
            at Pickles.TestHarness.MSTest.AdditionFeature.FailToAddTwoNumbers() in c:\dev\pickles-results-harness\Pickles.TestHarness\Pickles.TestHarness.MSTest\Addition.feature:line 18
          </StackTrace>
        </ErrorInfo>
      </Output>
    </UnitTestResult>";

    public static readonly ReadOnlyCollection<TestResult> TestSummaryModel_1 = new List<TestResult>()
    {
        new TestResult(TestName_1, TestResult_1, TestDurationResult_1)
    }.AsReadOnly();
    public readonly static ReadOnlyCollection<TestResult> TestSummaryModel_2 = new List<TestResult>()
    {
        new TestResult(TestName_1, TestResult_1, TestDurationResult_1),
        new TestResult(TestName_2, TestResult_2, TestDurationResult_2)
    }.AsReadOnly();
    public readonly static ReadOnlyCollection<TestResult> TestSummaryModel_3 = new List<TestResult>()
    {
        new TestResult(TestName_1, TestResult_1, TestDurationResult_1),
        new TestResult(TestName_2, TestResult_2, TestDurationResult_2),
        new TestResult(TestName_3, TestResult_3, TestDurationResult_3),
        new TestResult(TestName_4, TestResult_4, TestDurationResult_4)
    }.AsReadOnly();

    public static IEnumerable<object[]> GetItems()
    {
        yield return new object[] { (UnitTestResult_1, TestSummaryModel_1) };
        yield return new object[] { (UnitTestResult_2, TestSummaryModel_2) };
        yield return new object[] { (UnitTestResult_3, TestSummaryModel_3) };
    }
}
