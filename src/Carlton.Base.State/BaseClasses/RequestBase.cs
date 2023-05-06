namespace Carlton.Base.State;

public abstract class RequestBase
{
    public IDataWrapper Sender { get; init; }
    
    public object State { get => Sender.State; }

    public bool IsCompleted { get; private set; }

    public DateTime CreatedDateTime { get; }

    public DateTime CompletedDateTime { get; private set; }
    public bool ServerCalled { get; private set; }

    protected RequestBase(IDataWrapper sender)
    {
        Sender = sender;
        CreatedDateTime = DateTime.UtcNow;
    }

    public void MarkCompleted()
    {
        IsCompleted = true;
        CompletedDateTime = DateTime.Now;
    }

    public void MarkAsServerCalled()
    {
        ServerCalled = true;
    }
}
