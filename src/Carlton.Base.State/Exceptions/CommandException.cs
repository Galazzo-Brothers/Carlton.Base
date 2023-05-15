namespace Carlton.Base.State;

[Serializable]
public class CommandException<TCommand> : Exception
    where TCommand : ICommand
{
    private const string ErrorMessage = $"An exception occurred during a command of type {nameof(ICommand)}";

    public CommandRequest<TCommand> Command { get; init; }

    public CommandException(CommandRequest<TCommand> command, Exception innerException) : base(ErrorMessage, innerException)
    {
        command.MarkErrored();
        Command = command;
    }

    protected CommandException(SerializationInfo info, StreamingContext context)
    : base(info, context)
    {
    }
}
