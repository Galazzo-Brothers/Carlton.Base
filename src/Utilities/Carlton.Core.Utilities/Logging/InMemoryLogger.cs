using System.Collections.Concurrent;

namespace Carlton.Core.Utilities.Logging;

public class InMemoryLogger : ILogger
{
    private readonly ConcurrentQueue<LogMessage> logMessages = new();
    private readonly object scopeLock = new();
    private readonly List<IDisposable> scopes = new();

    public IDisposable BeginScope<TState>(TState state)
    {
        var scope = new ScopeDisposable<TState>(this, state);
        lock (scopeLock)
        {
            scopes.Add(scope);
        }
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

    internal string GetCurrentScopes()
    {
        lock (scopeLock)
        {
            return string.Join(" => ", scopes);
        }
    }

    internal void PopScope<TState>(TState state)
    {
        lock (scopeLock)
        {
            // Remove the scope from the list of active scopes.
            scopes.RemoveAll(scope => scope.ToString() == state.ToString());
        }
    }
}

file class ScopeDisposable<TState> : IDisposable
{
    private readonly InMemoryLogger logger;
    private readonly TState state;

    public ScopeDisposable(InMemoryLogger logger, TState state)
    {
        this.logger = logger;
        this.state = state;
    }

    public void Dispose()
    {
        logger.PopScope(this.state);
    }

    public override string ToString()
    {
        return state.ToString();
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