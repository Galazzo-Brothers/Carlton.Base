namespace Carlton.Base.TestBed;

public class TestBedState : ICarltonStateStore<TestBedStateEvents>
{
    public event Func<object, TestBedStateEvents, Task> StateChanged;

    private readonly IList<ComponentRecordedEvent> _componentEvents = new List<ComponentRecordedEvent>();

    public IEnumerable<IGrouping<Type, RegisteredComponentState>> RegisteredComponentStates { get; init; }
    public RegisteredComponentState SelectedComponentState { get; private set; }
    public Type TestComponentType { get { return SelectedComponentState.Type; } }
    public ComponentParameters TestComponentParameters { get; private set; }
    public IEnumerable<ComponentRecordedEvent> ComponentEvents { get { return _componentEvents; } }

    public TestBedState(IEnumerable<IGrouping<Type, RegisteredComponentState>> registeredComponentStates)
    {
        RegisteredComponentStates = registeredComponentStates;
        SelectedComponentState = registeredComponentStates.ElementAt(0).ElementAt(0);
        TestComponentParameters = SelectedComponentState.ComponentParameters;
    }

    public async Task AddTestComponentEvents(object sender, string eventName, object evt)
    {
        _componentEvents.Add(new ComponentRecordedEvent(eventName, evt));
        
        if(StateChanged != null)
            await InvokeStateChanged(sender, TestBedStateEvents.ComponentEventAdded);
    }

    public async Task ClearComponentEvents(object sender)
    {
        _componentEvents.Clear();
        await InvokeStateChanged(sender, TestBedStateEvents.ComponentEventsCleared);
    }

    public async Task UpdateTestComponentParameters(object sender, object parameterObj)
    {
        TestComponentParameters = new ComponentParameters(parameterObj, TestComponentParameters.ParameterObjType);
        await InvokeStateChanged(sender, TestBedStateEvents.ParametersChanged);
    }

    public async Task UpdateSelectedItemId(object sender, int componentID, int stateID)
    {
        SelectedComponentState = RegisteredComponentStates
            .ElementAt(componentID)
            .ElementAt(stateID);

        TestComponentParameters = SelectedComponentState.ComponentParameters;
        
        await InvokeStateChanged(sender, TestBedStateEvents.SelectedItem);
    }

    private async Task InvokeStateChanged(object sender, TestBedStateEvents evt)
    {
        var task = StateChanged?.Invoke(sender, evt);
        
        if(task != null)
            await task.ConfigureAwait(false);
    }
}




