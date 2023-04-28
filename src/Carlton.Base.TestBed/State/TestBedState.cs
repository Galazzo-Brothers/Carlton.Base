namespace Carlton.Base.TestBed;

public class TestBedState : IStateStore<TestBedStateEvents>
{
    public event Func<object, TestBedStateEvents, Task> StateChanged;

    private readonly IList<ComponentRecordedEvent> _componentEvents = new List<ComponentRecordedEvent>();

    public IEnumerable<ComponentState> ComponentStates { get; init; }
    public ComponentState SelectedComponentState { get; private set; }
    public Type SelectedComponentType { get { return SelectedComponentState.Type; } }
    public ComponentParameters SelectedComponentParameters { get; private set; }
    public IEnumerable<ComponentRecordedEvent> ComponentEvents { get { return _componentEvents; } }
    public IReadOnlyDictionary<string, TestResultsReport> ComponentTestResultsReports { get; private set; } = new Dictionary<string, TestResultsReport>();


    public TestBedState(IEnumerable<ComponentState> componentStates)
    {
        ComponentStates = componentStates;
        SelectedComponentState = ComponentStates.First();
        SelectedComponentParameters = SelectedComponentState.ComponentParameters;
    }

    public async Task RecordComponentEvent(object sender, string eventName, object evt)
    {
        _componentEvents.Add(new ComponentRecordedEvent(eventName, evt));
        
        if(StateChanged != null)
            await InvokeStateChanged(sender, TestBedStateEvents.ComponentEventRecorded);
    }

    public async Task ClearComponentEvents(object sender)
    {
        _componentEvents.Clear();
        await InvokeStateChanged(sender, TestBedStateEvents.ComponentEventsCleared);
    }

    public async Task UpdateSelectedComponentParameters(object sender, object parameterObj)
    {
        SelectedComponentParameters = new ComponentParameters(parameterObj, SelectedComponentParameters.ParameterObjType);
        await InvokeStateChanged(sender, TestBedStateEvents.ParametersChanged);
    }

    public async Task SelectComponentState(object sender, Type componentType, string componentState)
    {
        //SelectedComponentState = ComponentStates[componentType].First(_ => _.DisplayName == componentState);
        SelectedComponentParameters = SelectedComponentState.ComponentParameters;
        
        await InvokeStateChanged(sender, TestBedStateEvents.ComponentStateSelected);
    }

    public void InitComponentTestResultsReports(IReadOnlyDictionary<string, TestResultsReport> testResultsReports)
    {
        ComponentTestResultsReports = testResultsReports;
    }

    private async Task InvokeStateChanged(object sender, TestBedStateEvents evt)
    {
        var task = StateChanged?.Invoke(sender, evt);
        
        if(task != null)
            await task.ConfigureAwait(false);
    }
}




