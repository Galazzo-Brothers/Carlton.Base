namespace Carlton.Base.TestBed;

public class TestBedState : ICarltonStateStore<TestBedStateEvents>
{
    public event Func<object, TestBedStateEvents, Task> StateChanged;

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
            await InvokeStateChanged(sender, TestBedStateEvents.ComponentEventAdded);
    }

    public async Task ClearComponentEvents(object sender)
    {
        _componentEvents.Clear();
        await InvokeStateChanged(sender, TestBedStateEvents.ComponentEventsCleared);
    }

    public async Task UpdateTestComponentViewModel(object sender, object vm)
    {
        TestComponentViewModel = vm;
        await InvokeStateChanged(sender, TestBedStateEvents.ViewModelChanged);
    }

    public async Task UpdateSelectedItemId(object sender, int componentID, int stateID)
    {
        SelectedItem = NavMenuItems.First(_ => _.Index == componentID)
                             .Items.First(_ => _.Index == stateID)
                             .Value;
        TestComponentViewModel = SelectedItem.ViewModel;
        
        await InvokeStateChanged(sender, TestBedStateEvents.SelectedItem);
    }

    private async Task InvokeStateChanged(object sender, TestBedStateEvents evt)
    {
        var task = StateChanged?.Invoke(sender, evt);
        
        if(task != null)
            await task.ConfigureAwait(false);
    }
}





