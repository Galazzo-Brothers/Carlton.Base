namespace Carlton.Core.Flux.Models;

public class MutationCommandContext<TCommand>(TCommand command) : BaseRequestContext
{
    public Type CommandType { get => MutationCommand.GetType(); }
    public string CommandTypeName { get => CommandType.GetDisplayName(); }

    public TCommand MutationCommand { get; init; } = command;
    public string ResultingStateEvent { get; private set; }

    internal void MarkAsSucceeded(string stateEvent)
    {
        ResultingStateEvent = stateEvent;
        base.MarkAsSucceeded();
    }

    //Most uses of this context involve passing a
    //weakly typed object as a command at runtime
    //this override is primarily
    //so the actual value of the command can be logged
    public override string ToString()
     => $"MutationCommandContext[{CommandTypeName}]";
}

