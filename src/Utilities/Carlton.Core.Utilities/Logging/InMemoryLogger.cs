using System.Collections.Concurrent;
using System.Threading;

namespace Carlton.Core.Utilities.Logging;


public class InMemoryLogger : ILogger
{
    private readonly ConcurrentQueue<LogMessage> logMessages = new();
    private readonly AsyncLocal<Stack<LogScope>> _currentScopes = new();

    public IDisposable BeginScope<TState>(TState state)
    {
        var scope = new LogScope(state);
        _currentScopes.Value ??= new Stack<LogScope>();
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
        if (!IsEnabled(logLevel))
            return;

        var message = new LogMessage
        {
            LogLevel = logLevel,
            EventId = eventId,
            Message = formatter(state, exception),
            Exception = exception,
            Timestamp = DateTime.UtcNow,
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
        while (logMessages.TryDequeue(out _)) { }
    }

    public void ClearAllButMostRecent(int keepCount)
    {
        while (logMessages.Count > keepCount)
            logMessages.TryDequeue(out _);
    }

    internal string GetCurrentScopes()
    {
        return string.Join(" => ", _currentScopes?.Value);
    }

    private class LogScope : IDisposable
    {
        public object State { get; }

        public LogScope(object state)
        {
            State = state;
        }

        public void Dispose()
        {
            // Clean up any resources if needed
        }

        public override string ToString()
        {
            return State.ToString();
        }
    }
}

public class LogMessage
{
    public LogLevel LogLevel { get; set; }
    public EventId EventId { get; set; }
    public string Message { get; set; }
    public Exception Exception { get; set; }
    public DateTime Timestamp { get; set; }
    public string Scopes { get; set; }
}