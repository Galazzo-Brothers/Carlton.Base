namespace Carlton.Base.TestBed;

public class TestBedStateProcessor : TestBedState, IStateProcessor<TestBedState>
{
    public TestBedStateProcessor(IEnumerable<ComponentState> componentState, IDictionary<string, TestResultsReport> testResults) 
        : base(componentState, testResults)
    {
    }

    public async Task ProcessCommand<TCommand>(object sender, TCommand command)
      where TCommand : ICommand
    {
        await ProcessCommand(sender, (dynamic)command);
    }

    public async Task ProcessCommand(object sender, RecordEventCommand command)
    {
        _componentEvents.Add(new ComponentRecordedEvent(command.RecordedEventName, command.EventArgs));
        await InvokeStateChanged(sender, TestBedStateEvents.EventRecorded);
    }

    public async Task ProcessCommand(object sender, ClearEventsCommand command)
    {
        _componentEvents.Clear();
        await InvokeStateChanged(sender, TestBedStateEvents.EventsCleared);
    }

    public async Task ProcessCommand(object sender, UpdateParametersCommand command)
    {
        SelectedComponentParameters = new ComponentParameters(command.ComponentParameters, ParameterObjectType.ParameterObject);
        await InvokeStateChanged(sender, TestBedStateEvents.ParametersUpdated);
    }

    public async Task ProcessCommand(object sender, SelectMenuItemCommand command)
    {
        SelectedComponentState = command.ComponentState;
        SelectedComponentParameters = SelectedComponentState.ComponentParameters;
        await InvokeStateChanged(sender, TestBedStateEvents.MenuItemSelected);
    }

    public void RestoreState(Memento<TestBedState> memento)
    {
        memento.State.Adapt(this);
    }

    public Memento<TestBedState> SaveState()
    {
        return new Memento<TestBedState>(this);
    }
}

