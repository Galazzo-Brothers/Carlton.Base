namespace Carlton.Base.TestBed;

public class TestBedState : ICarltonStateStore<TestBedStateEvents>
{
    public event Func<object, TestBedStateEvents, Task> StateChanged;

    private readonly IList<ComponentRecordedEvent> _componentEvents;

    public IEnumerable<SelectGroup<NavMenuItem>> NavMenuItems { get; init; }
    public NavMenuItem SelectedItem { get; private set; }
    public Type TestComponentType { get { return SelectedItem.Type; } }
    public ComponentParameters TestComponentParameters { get; private set; }
    public IEnumerable<ComponentRecordedEvent> ComponentEvents { get { return _componentEvents; } }

    public TestBedState(NavMenuViewModel vm)
    {
        NavMenuItems = vm.MenuItems;
        SelectedItem = vm.SelectedItem;
        TestComponentParameters = vm.SelectedItem.ComponentParameters;
        _componentEvents = new List<ComponentRecordedEvent>();
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
        System.Console.WriteLine("Made it here");
        await InvokeStateChanged(sender, TestBedStateEvents.ParametersChanged);
    }

    public async Task UpdateSelectedItemId(object sender, int componentID, int stateID)
    {
        SelectedItem = NavMenuItems.First(_ => _.Index == componentID)
                             .Items.First(_ => _.Index == stateID)
                             .Value;
        TestComponentParameters = SelectedItem.ComponentParameters;
        
        await InvokeStateChanged(sender, TestBedStateEvents.SelectedItem);
    }

    private async Task InvokeStateChanged(object sender, TestBedStateEvents evt)
    {
        var task = StateChanged?.Invoke(sender, evt);
        
        if(task != null)
            await task.ConfigureAwait(false);
    }
}




