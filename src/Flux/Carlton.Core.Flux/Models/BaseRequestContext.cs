using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net;

namespace Carlton.Core.Flux.Models;

public class BaseRequestContext
{
    private readonly Stopwatch _stopwatch = new();
    private readonly ConcurrentBag<ChildRequestContext> _childRequests = [];

    public Guid RequestID { get; } = Guid.NewGuid();

    //Child Contexts
    public bool HasChildRequests { get => ChildRequests.Any(); }
    public IEnumerable<ChildRequestContext> ChildRequests { get => _childRequests; }
    internal void AddChildContext(BaseRequestContext context)
        => _childRequests.Add(new ChildRequestContext(RequestID, context));

    //Http Context
    public bool RequiresHttpRefresh { get; private set; }
    public bool RequestHttRefreshed { get => RequestHttpContext != null; }
    public RequestHttpContext RequestHttpContext { get; private set; }
    internal void MarkAsRequiresHttpRefresh()
       => RequiresHttpRefresh = true;
    internal void MarkAsHttpCallMade(string httpUrl, HttpStatusCode httpStatusCode, object httpResponse)
       => RequestHttpContext = new RequestHttpContext(httpUrl, httpStatusCode, httpResponse);

    //Validation Context
    public bool RequestValidated { get => RequestValidationContext != null; }
    public RequestValidationContext RequestValidationContext { get; private set; }
    internal void MarkAsValidated(IEnumerable<string> ValidationErrors)
       => RequestValidationContext = new RequestValidationContext(ValidationErrors);
    internal void MarkAsValidated()
      => RequestValidationContext = new RequestValidationContext();


    //Completion Context
    public bool RequestInProgress { get => RequestCompletionContext == null; }
    public RequestCompletionContext RequestCompletionContext { get; private set; }
    protected internal void MarkAsSucceeded()
        => RequestCompletionContext = new RequestCompletionContext(_stopwatch);
    protected internal void MarkAsErrored(Exception exception)
        => RequestCompletionContext = new RequestCompletionContext(Stopwatch.StartNew(), exception);
}

public record ChildRequestContext(Guid ParentRequestId, BaseRequestContext ChildRequest);


public record RequestHttpContext(
    string HttpUrl,
    HttpStatusCode HttpStatusCode,
    object HttpResponse);

public record RequestValidationContext(IEnumerable<string> ValidationErrors)
{
    public bool ValidationPassed { get => !ValidationErrors.Any(); }

    public RequestValidationContext() : this(new List<string>())
    { }
}

public record RequestCompletionContext
{
    public bool RequestSucceeded { get => Exception == null; }
    public Exception Exception { get; init; }
    public DateTimeOffset RequestEndTimestamp { get; init; }
    public long ElapsedTime { get; init; }

    public RequestCompletionContext(Stopwatch stopwatch) : this(stopwatch, null)
    {
    }

    public RequestCompletionContext(Stopwatch stopwatch, Exception exception)
    {
        Exception = exception;
        RequestEndTimestamp = DateTimeOffset.UtcNow;
        stopwatch.Stop();
        ElapsedTime = stopwatch.ElapsedMilliseconds;
    }
}