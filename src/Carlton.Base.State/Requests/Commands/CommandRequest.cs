namespace Carlton.Base.State;

public class CommandRequest<TCommand> : RequestBase<TCommand>
    where TCommand : ICommand
{
    public TCommand Command { get; init; }

    public CommandRequest(IDataWrapper sender, TCommand command) : base(sender)
        => Command = command;
}
