using System.Collections.Immutable;

namespace Carlton.Core.Components.Lab;

public record LabState 
{
    protected readonly IList<ComponentRecordedEvent> _componentEvents = new List<ComponentRecordedEvent>();

    public IReadOnlyList<ComponentState> ComponentStates { get; init; }
    public int SelectedComponentIndex { get; init; }
    public ComponentState SelectedComponentState { get => ComponentStates.ElementAt(SelectedComponentIndex); }
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
        ComponentStates = componentStates.ToList();
        SelectedComponentIndex = 0; //Default to the first item
        SelectedComponentParameters = SelectedComponentState.ComponentParameters;
        ComponentTestResults = testResults.ToImmutableDictionary();
    }
}