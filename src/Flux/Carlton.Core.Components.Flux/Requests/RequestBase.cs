using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.InProcessMessaging;

namespace Carlton.Core.Components.Flux;
public abstract class RequestBase<T>
{
    public T WrappedType { get; init; }
    public Guid RequestID { get; init; }
    public string DisplayName { get => $"{GetType().GetDisplayName()}_{typeof(T).GetDisplayName()}"; }
    public object Sender { get; init; }

    //public Type SenderWrappedType { get => Sender.WrappedComponentType; }
    //public object State { get => Sender.State; }

    public DateTime CreatedDateTime { get; }
    public DateTime CompletedDateTime { get; private set; }
    public DateTime ErroredDateTime { get; private set; }



    protected RequestBase(IDataWrapper sender)
    {
        Sender = sender;
        CreatedDateTime = DateTime.UtcNow;
        RequestID = Guid.NewGuid();
    }
}



//public bool IsCompleted { get; private set; }
//public bool HasErrored { get; private set; }
//public bool JsInteropCalled { get; private set; }
//public bool ServerCalled { get; private set; }
//public bool RequestValidated { get; private set; }
//public string ServerUrl { get; private set; }
//public string JsModuleName { get; private set; }
//public string JsFunctionName { get; private set; }

//public IList<EventId> Errors { get; init; } = new List<EventId>();

//public DateTime CreatedDateTime { get; }
//public DateTime CompletedDateTime { get; private set; }
//public DateTime ErroredDateTime { get; private set; }

//object IRequest<T>.Sender => throw new NotImplementedException();

//public void MarkCompleted()
//{
//    if (IsCompleted)
//        throw new InvalidOperationException("The request has already completed.");

//    IsCompleted = true;
//    CompletedDateTime = DateTime.Now;
//}

//public void MarkErrored()
//{
//    //Totally Unexpected Error, should not happen
//    MarkErrored(-1);
//}

//public void MarkErrored(int errorCode)
//{
//    if (IsCompleted)
//        throw new InvalidOperationException("The request has already completed.");

//    HasErrored = true;
//    ErroredDateTime = DateTime.Now;
//    Errors.Add(errorCode);
//}

//public void MarkAsServerCalled(string serverUrl)
//{
//    if (IsCompleted)
//        throw new InvalidOperationException("The request has already completed.");

//    ServerCalled = true;
//    ServerUrl = serverUrl;
//}

//public void MarkAsJsInteropCalled(string module, string function)
//{
//    if (IsCompleted)
//        throw new InvalidOperationException("The request has already completed.");

//    JsInteropCalled = true;
//    JsModuleName = module;
//    JsFunctionName = function;
//}

//public void MarkAsValidated()
//{
//    if (IsCompleted)
//        throw new InvalidOperationException("The request has already completed.");

//    RequestValidated = true;
//}