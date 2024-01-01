using System.Collections.Concurrent;
using System.Threading;

namespace Carlton.Core.Utilities.Logging;


public class MemoryLogger : ILogger
{
    private readonly ConcurrentQueue<LogMessage> logMessages = new();
    private readonly AsyncLocal<ConcurrentStack<LogScope>> _currentScopes = new();

    public IDisposable BeginScope<TState>(TState state)
    {
        var scope = new LogScope(state, () => PopScope());
        _currentScopes.Value ??= new();
        _currentScopes.Value.Push(scope);
        return scope;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true; // Log all levels for simplicity.
    }

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception exception,
        Func<TState, Exception, string> formatter)
    {
        if(!IsEnabled(logLevel))
            return;

        var message = new LogMessage
        {
            LogLevel = logLevel,
            EventId = eventId.Id,
            EventName = eventId.Name,
            Message = formatter(state, exception),
            Exception = exception,
            Timestamp = DateTime.Now,
            Scopes = GetCurrentScopes()
        };

        logMessages.Enqueue(message);
    }

    // Add methods to access log messages as needed.
    public LogMessage[] GetLogMessages()
    {
        return logMessages.ToArray();
    }

    public void ClearLogMessages()
    {
        while(logMessages.TryDequeue(out _))
        { }
    }

    public void ClearAllButMostRecent(int keepCount)
    {
        while(logMessages.Count > keepCount)
            logMessages.TryDequeue(out _);
    }

    private string GetCurrentScopes()
    {
        return _currentScopes?.Value == null ? string.Empty : string.Join(" => ", _currentScopes?.Value);
    }

    private void PopScope()
    {
         _currentScopes.Value.TryPop(out _);
    }
}


internal class LogScope : IDisposable
{
    public object State { get; }
    private readonly Action _disposeAct;

    public LogScope(object state, Action disposeAct)
    {
        State = state;
        _disposeAct = disposeAct;
    }

    public void Dispose()
    {
        //Remove the scope for the list of scopes
        //as it is being disposed
        _disposeAct();
    }

    public override string ToString()
    {
        return State.ToString();
    }
}

