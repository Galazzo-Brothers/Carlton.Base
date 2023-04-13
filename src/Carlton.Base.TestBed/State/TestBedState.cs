namespace Carlton.Base.TestBedFramework;

public class TestBedState : ICarltonStateStore
{
    public const string SELECTED_ITEM = "SelectedItem";
    public const string VIEW_MODEL_CHANGED = "ViewModelChanged";
    public const string STATUS_CHANGED = "StatusChanged";
    public const string COMPONENT_EVENT_ADDED = "ComponentEventAdded";
    public const string COMPONENT_EVENTS_CLEARED = "COMPONENT_EVENTS_CLEARED";

    public event Func<object, string, Task> StateChanged;

    private readonly IList<object> _componentEvents;

    public IEnumerable<SelectGroup<NavMenuItem>> NavMenuItems { get; init; }
    public NavMenuItem SelectedItem { get; private set; }
    public Type TestComponentType { get { return SelectedItem.Type; } }
    public bool IsTestComponentCarltonComponent { get { return SelectedItem.IsCarltonComponent; } }
    public object TestComponentViewModel { get; private set; }
    public IEnumerable<object> ComponentEvents { get { return _componentEvents; } }

    public TestBedState(NavMenuViewModel vm)
    {
        NavMenuItems = vm.MenuItems;
        SelectedItem = vm.SelectedItem;
        TestComponentViewModel = vm.SelectedItem.ViewModel;
        _componentEvents = new List<object>();
    }

    public async Task AddTestComponentEvents(object sender, object componentEvent)
    {
        _componentEvents.Add(componentEvent);
        
        if(StateChanged != null)
            await InvokeStateChanged(sender, COMPONENT_EVENT_ADDED);
    }

    public async Task ClearComponentEvents(object sender)
    {
        _componentEvents.Clear();
        await InvokeStateChanged(sender, COMPONENT_EVENTS_CLEARED);
    }

    public async Task UpdateTestComponentViewModel(object sender, object vm)
    {
        TestComponentViewModel = vm;
        await InvokeStateChanged(sender, VIEW_MODEL_CHANGED);
    }

    public async Task UpdateSelectedItemId(object sender, int componentID, int stateID)
    {
        SelectedItem = NavMenuItems.First(_ => _.Index == componentID)
                             .Items.First(_ => _.Index == stateID)
                             .Value;
        TestComponentViewModel = SelectedItem.ViewModel;
        
        await InvokeStateChanged(sender, SELECTED_ITEM);
    }

    private async Task InvokeStateChanged(object sender, string evt)
    {
        var task = StateChanged?.Invoke(sender, evt);
        
        if(task != null)
            await task.ConfigureAwait(false);
    }
}





