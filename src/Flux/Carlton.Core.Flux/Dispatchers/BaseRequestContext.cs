using System.Diagnostics;
using System.Net;
namespace Carlton.Core.Flux.Dispatchers;

public abstract class BaseRequestContext
{
	private readonly Stopwatch _stopwatch = new();

	//Common
	public Guid RequestId { get; } = Guid.NewGuid();
	public abstract FluxOperation FluxOperation { get; }
	public abstract Type FluxOperationType { get; }
	public string FluxOperationTypeName { get => FluxOperationType.GetDisplayName(); }
	protected internal void MarkAsStarted()
		=> _stopwatch.Start();

	//Http Context
	public bool RequiresHttpRefresh { get; private set; }
	public bool HttpRefreshOccurred { get => RequestHttpContext != null; }
	public RequestHttpContext RequestHttpContext { get; private set; }
	internal void MarkAsRequiresHttpRefresh()
		=> RequiresHttpRefresh = true;
	internal void MarkAsHttpCallMade(string httpUrl, HttpStatusCode httpStatusCode, object httpResponse)
		=> RequestHttpContext = new RequestHttpContext(httpUrl, httpStatusCode, httpResponse);

	//Validation Result
	public bool RequestValidated { get => ValidationResult != null; }
	public ValidationResult ValidationResult { get; private set; }
	internal void MarkAsValidated(IEnumerable<string> ValidationErrors)
		=> ValidationResult = new ValidationResult(ValidationErrors);
	internal void MarkAsValidated()
		=> ValidationResult = new ValidationResult();


	//Request Result
	public bool RequestInProgress { get => RequestResult == null; }
	public RequestResult RequestResult { get; private set; }
	protected internal void MarkAsSucceeded()
		=> RequestResult = new RequestResult(_stopwatch);
	protected internal void MarkAsErrored(Exception exception)
		=> RequestResult = new RequestResult(_stopwatch, exception);
	protected internal void MarkAsErrored(FluxError error)
		=> RequestResult = new RequestResult(_stopwatch, error);
}


public record RequestHttpContext(string HttpUrl, HttpStatusCode HttpStatusCode, object HttpResponse);

public record ValidationResult(IEnumerable<string> ValidationErrors)
{
	public bool ValidationPassed { get => !ValidationErrors.Any(); }

	internal ValidationResult() : this(new List<string>())
	{ }
}

public record RequestResult
{
	public FluxError FluxError { get; init; }
	public bool RequestSucceeded { get => Exception == null && FluxError == null; }
	public RequestExceptionContext Exception { get; init; }
	public DateTimeOffset RequestEndTimestamp { get; init; }
	public long ElapsedTime { get; init; }

	internal RequestResult(Stopwatch stopwatch, Exception exception) : this(stopwatch)
		=> Exception = new RequestExceptionContext(exception.GetType().Name, exception.Message, exception.StackTrace);

	internal RequestResult(Stopwatch stopwatch, FluxError error) : this(stopwatch)
		=> FluxError = error;

	internal RequestResult(Stopwatch stopwatch)
	{
		RequestEndTimestamp = DateTimeOffset.UtcNow;
		stopwatch.Stop();
		ElapsedTime = stopwatch.ElapsedMilliseconds;
	}
}

public record RequestExceptionContext(string ExceptionType, string Message, string StackTrace);

public enum FluxOperation
{
	ViewModelQuery = 1,
	CommandMutation = 2,
}