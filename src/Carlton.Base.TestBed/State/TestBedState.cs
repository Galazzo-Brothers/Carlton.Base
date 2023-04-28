namespace Carlton.Base.TestBed;

public class TestBedState : IStateStore<TestBedStateEvents>
{
    public event Func<object, TestBedStateEvents, Task> StateChanged;

    protected readonly IList<ComponentRecordedEvent> _componentEvents = new List<ComponentRecordedEvent>();

    public IEnumerable<ComponentState> ComponentStates { get; init; }
    public ComponentState SelectedComponentState { get; protected set; }
    public Type SelectedComponentType { get { return SelectedComponentState.Type; } }
    public ComponentParameters SelectedComponentParameters { get; protected set; }
    public IEnumerable<ComponentRecordedEvent> ComponentEvents { get { return _componentEvents; } }
    public IReadOnlyDictionary<string, TestResultsReport> ComponentTestResultsReports { get; protected set; } = new Dictionary<string, TestResultsReport>();


    public TestBedState(IEnumerable<ComponentState> componentStates)
    {
        ComponentStates = componentStates;
        SelectedComponentState = ComponentStates.First();
        SelectedComponentParameters = SelectedComponentState.ComponentParameters;
    }

    protected async Task InvokeStateChanged(object sender, TestBedStateEvents evt)
    {
        var task = StateChanged?.Invoke(sender, evt);

        if(task != null)
            await task.ConfigureAwait(false);
    }
}