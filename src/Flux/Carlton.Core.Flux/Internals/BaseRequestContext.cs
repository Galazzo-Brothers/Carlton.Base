using System.Diagnostics;
using System.Net;
namespace Carlton.Core.Flux.Internals;

internal abstract class BaseRequestContext
{
	private readonly Stopwatch _stopwatch = new();

	//Common
	public Guid RequestId { get; } = Guid.NewGuid();
	public abstract FluxOperationKind FluxOperationKind { get; }
	public abstract Type FluxOperationType { get; }
	public string FluxOperationTypeName { get => FluxOperationType.GetDisplayName(); }
	protected internal void MarkAsStarted() => _stopwatch.Start();

	//Http Result
	public bool RequiresHttpRefresh { get; private set; }
	public bool HttpCallMade { get; private set; }
	public bool HttpCallSucceeded { get; private set; }
	public string HttpUrl { get; private set; }
	public HttpStatusCode HttpStatus { get; private set; }
	public object HttpResult { get; private set; }
	internal void MarkAsRequiresHttpRefresh()
		=> RequiresHttpRefresh = true;
	internal void MarkAsHttpCallMade(string httpUrl, HttpStatusCode httpStatusCode)
		=> (HttpCallMade, HttpUrl, HttpStatus) = (true, httpUrl, httpStatusCode);
	internal void MarkAsHttpCallSucceeded(object httpResult)
		=> (HttpCallSucceeded, HttpResult) = (true, httpResult);

	//Validation Result
	public bool RequestValidated { get; private set; }
	public bool ValidationPassed { get; private set; }
	public IEnumerable<string> ValidationErrors { get; private set; } = new List<string>();
	internal void MarkAsInvalid(IEnumerable<string> validationErrors)
		=> (RequestValidated, ValidationPassed, ValidationErrors) = (true, false, validationErrors);
	internal void MarkAsValid()
		=> (RequestValidated, ValidationPassed) = (true, true);

	//Request Result
	public bool RequestEnded { get; private set; }
	public ExceptionDescriptor ExceptionDescriptor { get; private set; }
	public DateTimeOffset RequestEndTimestamp { get; private set; }
	public long ElapsedTime { get; private set; }
	public bool RequestSucceeded { get => RequestEnded && ExceptionDescriptor == null; }

	protected internal void MarkAsSucceeded()
		=> EndRequest();

	protected internal void MarkAsErrored(Exception exception)
	{
		ExceptionDescriptor = new ExceptionDescriptor(exception.GetType().Name, exception.Message, exception.StackTrace);
		EndRequest();
	}

	private void EndRequest()
	{
		RequestEnded = true;
		RequestEndTimestamp = DateTimeOffset.UtcNow;
		_stopwatch.Stop();
		ElapsedTime = _stopwatch.ElapsedMilliseconds;
	}
}


internal record ExceptionDescriptor(string ExceptionType, string Message, string StackTrace);