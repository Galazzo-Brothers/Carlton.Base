namespace Carlton.Base.State;
public interface IRequest<T>
{
    public Type RequestType { get => typeof(T); }
    public Guid RequestID { get; init; }
    public abstract string DisplayName { get; }
    public IDataWrapper Sender { get; init; }
    public Type SenderWrappedType { get => Sender.WrappedComponentType; }
    public object State { get => Sender.State; }
    public bool IsCompleted { get; }
    public bool RequestErrored { get; }
    public bool ServerCalled { get; }

    public DateTime CreatedDateTime { get; }
    public DateTime CompletedDateTime { get; }
    public DateTime ErroredDateTime { get; }

    public void MarkCompleted();
    public void MarkErrored();
    public void MarkAsServerCalled();
}
