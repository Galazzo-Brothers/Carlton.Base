namespace Carlton.Core.Flux.Models;

public class MutationCommandContext<TCommand>(TCommand command) : BaseRequestContext
{
    public Type CommandType { get => MutationCommand.GetType(); }
    public string CommandTypeName { get => CommandType.GetDisplayName(); }

    public string ResultingStateEvent { get; private set; } 
    public TCommand MutationCommand { get; private init; } = command;

    public void MarkAsStateMutationApplied(string stateEvent)
    {
        ResultingStateEvent = stateEvent;
    }
}

