using System.Collections.Immutable;

namespace Carlton.Core.Components.Lab;

public record LabState 
{
    protected readonly IList<ComponentRecordedEvent> _componentEvents = new List<ComponentRecordedEvent>();

    public IEnumerable<ComponentState> ComponentStates { get; init; }
    public ComponentState SelectedComponentState { get; init; }
    public Type SelectedComponentType { get { return SelectedComponentState.Type; } }
    public string SelectedComponentMarkup { get; init; }
    public ComponentParameters SelectedComponentParameters { get; init; }
    public IEnumerable<ComponentRecordedEvent> ComponentEvents 
    {
        get { return _componentEvents; }
        init { _componentEvents = value.ToList(); }
    }
    public ImmutableDictionary<string, TestResultsReport> ComponentTestResults { get; init; }
    public TestResultsReport SelectedComponentTestReport
    {
        get => ComponentTestResults.ContainsKey(SelectedComponentType.GetDisplayName()) ?
             ComponentTestResults[SelectedComponentType.GetDisplayName()]
            : new TestResultsReport();
    }

    public LabState(IEnumerable<ComponentState> componentStates, IDictionary<string, TestResultsReport> testResults)
    {
        ComponentStates = componentStates;
        SelectedComponentState = ComponentStates.First();
        SelectedComponentParameters = SelectedComponentState.ComponentParameters;
        ComponentTestResults = testResults.ToImmutableDictionary();
    }
}