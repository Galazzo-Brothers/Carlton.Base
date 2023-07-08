namespace Carlton.Base.State;

public abstract class RequestBase<T> : IRequest<T>
{
    public T WrappedType { get; init; }
    public Guid RequestID { get; init; }
    public string DisplayName { get => $"{GetType().GetDisplayName()}_{typeof(T).GetDisplayName()}"; }
    public IDataWrapper Sender { get; init; }
    public Type SenderWrappedType { get => Sender.WrappedComponentType; }
    public object State { get => Sender.State; }


    public bool IsCompleted { get; private set; }
    public bool HasErrored { get; private set; }
    public bool JsInteropCalled { get; private set; }
    public bool ServerCalled { get; private set; }
    public bool RequestValidated { get; private set; }
    public string ServerUrl { get; private set; }
    public string JsModuleName { get; private set; }
    public string JsFunctionName { get; private set; }

    public IList<EventId> Errors { get; init; } = new List<EventId>();

    public DateTime CreatedDateTime { get; }
    public DateTime CompletedDateTime { get; private set; }
    public DateTime ErroredDateTime { get; private set; }


    protected RequestBase(IDataWrapper sender)
    {
        Sender = sender;
        CreatedDateTime = DateTime.UtcNow;
        RequestID = Guid.NewGuid();
    }

    public void MarkCompleted()
    {
        if(IsCompleted)
            throw new InvalidOperationException("The request has already completed.");

        IsCompleted = true;
        CompletedDateTime = DateTime.Now;
    }

    public void MarkErrored()
    {
        //Totally Unexpected Error, should not happen
        MarkErrored(-1);
    }

    public void MarkErrored(int errorCode)
    {
        if(IsCompleted)
            throw new InvalidOperationException("The request has already completed.");

        HasErrored = true;
        ErroredDateTime = DateTime.Now;
        Errors.Add(errorCode);
    }

    public void MarkAsServerCalled(string serverUrl)
    {
        if(IsCompleted)
            throw new InvalidOperationException("The request has already completed.");

        ServerCalled = true;
        ServerUrl = serverUrl;
    }

    public void MarkAsJsInteropCalled(string module, string function)
    {
        if(IsCompleted)
            throw new InvalidOperationException("The request has already completed.");

        JsInteropCalled = true;
        JsModuleName = module;
        JsFunctionName = function;
    }

    public void MarkAsValidated()
    {
        if(IsCompleted)
            throw new InvalidOperationException("The request has already completed.");

        RequestValidated = true;
    }
}
