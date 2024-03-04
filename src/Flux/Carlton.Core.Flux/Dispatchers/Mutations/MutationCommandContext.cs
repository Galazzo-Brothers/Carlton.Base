namespace Carlton.Core.Flux.Dispatchers.Mutations;

public class MutationCommandContext<TCommand>(TCommand command) : BaseRequestContext
{
    public string CommandTypeName { get => MutationCommand.GetType().GetDisplayName(); }

    public TCommand MutationCommand { get; init; } = command;
    public string ResultingStateEvent { get; private set; }

    internal void MarkAsSucceeded(string stateEvent)
    {
        ResultingStateEvent = stateEvent;
        MarkAsSucceeded();
    }

    //Most uses of this context involve passing a
    //weakly typed object as a command at runtime
    //this override is primarily
    //so the actual value of the command can be logged
    public override string ToString()
     => $"MutationCommandContext[{CommandTypeName}]";
}

