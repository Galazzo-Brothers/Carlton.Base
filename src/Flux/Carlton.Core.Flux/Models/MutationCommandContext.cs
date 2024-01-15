namespace Carlton.Core.Flux.Models;

public class MutationCommandContext<TCommand>(TCommand command) : BaseRequestContext
{
    public Type CommandType { get => MutationCommand.GetType(); }
    public string CommandTypeName { get => CommandType.GetDisplayName(); }

    public TCommand MutationCommand { get; init; } = command;
    public string ResultingStateEvent { get; private set; }

    public void MarkAsStateMutationApplied(string stateEvent)
    {
        ResultingStateEvent = stateEvent;
    }

    public void MarkChildRequestsSucceeded()
        => base.MarkAsSucceeded();
}

