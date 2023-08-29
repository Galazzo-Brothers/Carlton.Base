using Carlton.Core.Components.Lab.Test.Mocks;
using Carlton.Core.Utilities.UnitTesting;

namespace Carlton.Core.Components.Lab.Test.Common;

internal class LabStateFactory
{
    public static LabState BuildLabState()
    {
        var componentStates = new List<ComponentState>
        {
            new ComponentState("Component State 1", typeof(DummyComponent), BuildParameters("Test", 1, true)),
            new ComponentState("Component State 2", typeof(DummyComponent), BuildParameters("Test 2", 2, true)),
            new ComponentState("Component State 3", typeof(DummyComponent), BuildParameters("Test 3", 3, false))
        };

        var testResults = new TestResultsReport
           (
               new List<TestResult>
               {
                    new TestResult("Test 1", TestResultOutcomes.Passed, 2.3),
                    new TestResult("Test 2", TestResultOutcomes.Passed, 4.3),
                    new TestResult("Test 3", TestResultOutcomes.Failed, 7.3),
               }
           );

        var dictionary = new Dictionary<string, TestResultsReport>
        {
            { nameof(DummyComponent), testResults }
        };


        return new LabState(componentStates, dictionary);
    }

    public static ComponentParameters BuildParameters(string param1, int param2, bool param3)
    {
        return new ComponentParameters
            (
                new { Param1 = param1, Param2 = param2, Param3 = param3 }
            , ParameterObjectType.ParameterObject);
    }
}