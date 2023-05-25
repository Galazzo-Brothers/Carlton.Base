namespace Carlton.Base.State;

[Serializable]
public class CommandException<TCommand> : Exception
{
    private const string ErrorMessage = $"An exception occurred during a command of type {nameof(TCommand)}";

    public CommandRequest<TCommand> Request { get; init; }

    public CommandException(CommandRequest<TCommand> request, Exception innerException) : base(ErrorMessage, innerException)
    {
        if(!request.HasErrored)
            request.MarkErrored();

        Request = request;
    }

    protected CommandException(SerializationInfo info, StreamingContext context)
    : base(info, context)
    {
    }
}
