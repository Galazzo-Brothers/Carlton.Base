using System.Diagnostics;

namespace Carlton.Core.Flux.Models;

public class BaseRequestContext
{
    private readonly Stopwatch _stopwatch = new();

    public Guid RequestID { get; }
    public bool IsCompleted { get; private set; }
    public bool IsErrored { get; private set; }
    public DateTime? EndTime { get; private set; }
    public long ElapsedTime { get; private set; }

    public bool HttpCallMade { get; private set; }
    public string HttpUrl { get; private set; }
    public object HttpResponse { get; private set; }

    public bool ResourceValidated { get; private set; }
    public bool? ValidationPassed { get; private set; } = null;
    public IEnumerable<string> ValidationErrors { get; private set; }

    public BaseRequestContext()
        => RequestID = Guid.NewGuid();


    public void MarkAsHttpCallMade(string httpRefreshUrl, object response)
    {
        HttpCallMade = true;
        HttpUrl = httpRefreshUrl;
        HttpResponse = response;
    }

    public void MarkAsValidated()
    {
        ResourceValidated = true;
        ValidationPassed = true;
    }

    public void MarkAsValidationFailed(IEnumerable<string> validationErrors)
    {
        ResourceValidated = true;
        ValidationPassed = false;
        ValidationErrors = validationErrors;
    }

    public void MarkAsCompleted()
    {
        IsCompleted = true;
        StopStopWatch();
    }

    public void MarkAsErrored()
    {
        IsErrored = true;
        StopStopWatch();
    }

    private void StopStopWatch()
    {
        _stopwatch.Stop();
        EndTime = DateTime.Now;
        ElapsedTime = _stopwatch.ElapsedMilliseconds;
    }
}
