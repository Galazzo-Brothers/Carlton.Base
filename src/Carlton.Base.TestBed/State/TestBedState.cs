namespace Carlton.Base.TestBed;

public class TestBedState : ICarltonStateStore<TestBedStateEvents>
{
    public event Func<object, TestBedStateEvents, Task> StateChanged;

    private readonly IList<ComponentRecordedEvent> _componentEvents = new List<ComponentRecordedEvent>();

    public IEnumerable<IGrouping<Type, ComponentState>> ComponentStates { get; init; }
    public ComponentState SelectedComponentState { get; private set; }
    public Type SelectedComponentType { get { return SelectedComponentState.Type; } }
    public ComponentParameters SelectedComponentParameters { get; private set; }
    public IEnumerable<ComponentRecordedEvent> ComponentEvents { get { return _componentEvents; } }
    public Dictionary<Type, TestResultsSummary> ComponentTestResultsReports { get; init; } = new Dictionary<Type, TestResultsSummary>();


    public TestBedState(IEnumerable<IGrouping<Type, ComponentState>> componentStates)
    {
        ComponentStates = componentStates;
        SelectedComponentState = ComponentStates.ElementAt(0).ElementAt(0);
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

    public async Task SelectComponentState(object sender, int componentID, int stateID)
    {
        SelectedComponentState = ComponentStates
            .ElementAt(componentID)
            .ElementAt(stateID);

        SelectedComponentParameters = SelectedComponentState.ComponentParameters;
        
        await InvokeStateChanged(sender, TestBedStateEvents.ComponentStateSelected);
    }

    public async Task UpdateComponentTestResultsReports(IEnumerable<TestResultsReport> reports)
    {
        ComponentTestResultsReports.Clear();        
    }

    private async Task InvokeStateChanged(object sender, TestBedStateEvents evt)
    {
        var task = StateChanged?.Invoke(sender, evt);
        
        if(task != null)
            await task.ConfigureAwait(false);
    }
}




