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

    public async Task ProcessCommand(object sender, EventRecordedCommand command)
    {
        _componentEvents.Add(new ComponentRecordedEvent(command.Name, command.Obj));
        await InvokeStateChanged(sender, TestBedStateEvents.ComponentEventRecorded);
    }

    public async Task ProcessCommand(object sender, EventsClearedCommand command)
    {
        _componentEvents.Clear();
        await InvokeStateChanged(sender, TestBedStateEvents.ComponentEventsCleared);
    }

    public async Task ProcessCommand(object sender, ModelChangedCommand command)
    {
        SelectedComponentParameters = command.ComponentParameters;
        await InvokeStateChanged(sender, TestBedStateEvents.ParametersChanged);
    }

    public async Task ProcessCommand(object sender, NavItemSelectedCommand command)
    {
        SelectedComponentState = command.ComponentState;
        SelectedComponentParameters = SelectedComponentState.ComponentParameters;

        await InvokeStateChanged(sender, TestBedStateEvents.ComponentStateSelected);
    }

    public void InitComponentTestResultsReports(IReadOnlyDictionary<string, TestResultsReport> testResultsReports)
    {
        ComponentTestResultsReports = testResultsReports;
    }
}

