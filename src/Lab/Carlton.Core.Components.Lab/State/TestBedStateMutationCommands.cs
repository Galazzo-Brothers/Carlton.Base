namespace Carlton.Core.Components.Lab;

public class TestBedStateMutationCommands : TestBedState
{
    public TestBedStateMutationCommands(
        IEnumerable<ComponentState> componentStates,
        IDictionary<string, TestResultsReportModel> testResults)
        : base(componentStates, testResults)
    {
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
}
