namespace Carlton.Core.Components.Lab;

public class TestBedState : IStateStore<TestBedStateEvents>
{
    public event Func<object, TestBedStateEvents, Task> StateChanged;

    protected readonly IList<ComponentRecordedEvent> _componentEvents = new List<ComponentRecordedEvent>();

    public IEnumerable<ComponentState> ComponentStates { get; init; }
    public ComponentState SelectedComponentState { get; protected set; }
    public Type SelectedComponentType { get { return SelectedComponentState.Type; } }
    public string SelectedComponentMarkup { get; private set; }
    public ComponentParameters SelectedComponentParameters { get; protected set; }
    public IEnumerable<ComponentRecordedEvent> ComponentEvents { get { return _componentEvents; } }
    public IReadOnlyDictionary<string, TestResultsReportModel> ComponentTestResults { get; init; }
    public TestResultsReportModel SelectedComponentTestReport
    {
        get => ComponentTestResults.ContainsKey(SelectedComponentType.GetDisplayName()) ?
             ComponentTestResults[SelectedComponentType.GetDisplayName()]
            : new TestResultsReportModel();
    }

    public TestBedState(IEnumerable<ComponentState> componentStates, IDictionary<string, TestResultsReportModel> testResults)
    {
        ComponentStates = componentStates;
        SelectedComponentState = ComponentStates.First();
        SelectedComponentParameters = SelectedComponentState.ComponentParameters;
        ComponentTestResults = testResults.AsReadOnly();
    }

    protected async Task InvokeStateChanged(object sender, TestBedStateEvents evt)
    {
        var task = StateChanged?.Invoke(sender, evt);

        if(task != null)
            await task.ConfigureAwait(false);
    }
}