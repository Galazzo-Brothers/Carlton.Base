namespace Carlton.Base.State;

public class CommandRequest<TStateCommand> : ComponentRequestBase, IRequest
    where TStateCommand : ICommand
{
    public TStateCommand Command { get; init; }

    public CommandRequest(object sender, TStateCommand command) : base(sender)
        => Command = command;
}




