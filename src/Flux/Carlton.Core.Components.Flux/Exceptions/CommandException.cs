namespace Carlton.Core.Components.Flux;

[Serializable]
public class CommandException<TCommand> : Exception
{
    private const string ErrorMessage = $"An exception occurred during a command of type {nameof(TCommand)}";

    //public ComponentCommand Command { get; init; }

    //public CommandException(ComponentCommand request, Exception innerException) : base(ErrorMessage, innerException)
    //{
    //    //if(!request.HasErrored)
    //    //    request.MarkErrored();

    //    //Request = request;
    //}

    protected CommandException(SerializationInfo info, StreamingContext context)
    : base(info, context)
    {
    }
}
