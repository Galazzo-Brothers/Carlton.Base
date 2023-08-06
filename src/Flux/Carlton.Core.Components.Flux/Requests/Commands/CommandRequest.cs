namespace Carlton.Core.Components.Flux;
public class CommandRequest<TCommand> : RequestBase<TCommand>
{
    public TCommand Command { get; init; }

    public CommandRequest(IDataWrapper sender, TCommand command) : base(sender)
        => Command = command;
}
