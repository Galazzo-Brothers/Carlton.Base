namespace Carlton.Base.TestBed;

public class TestBedStateProcessor : TestBedState, ICommandProcessor
{

    public TestBedStateProcessor(IEnumerable<ComponentState> componentStates) : base(componentStates)
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

    public void InitComponentTestResultsReports(IReadOnlyDictionary<string, TestResultsReport> testResultsReports)
    {
        ComponentTestResultsReports = testResultsReports;
    }
}

