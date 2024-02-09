using System.Collections.Concurrent;
using System.Text.RegularExpressions;
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
        if (!IsEnabled(logLevel))
            return;

        var message = new LogMessage
        {
            LogLevel = logLevel,
            EventId = eventId,
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
        return [.. logMessages];
    }

    public void ClearLogMessages()
    {
        while (logMessages.TryDequeue(out _))
        { }
    }

    public void ClearAllButMostRecent(int keepCount)
    {
        while (logMessages.Count > keepCount)
            logMessages.TryDequeue(out _);
    }

    private Dictionary<string, object> GetCurrentScopes()
    {
        var result = new Dictionary<string, object>();

        if (_currentScopes?.Value == null)
            return result;

        foreach (var scope in _currentScopes.Value.ToList())
        {
            //The scope is a structured message 
            //with parameters
            if (scope.State is IReadOnlyList<KeyValuePair<string, object>> castedScopes)
            {
                foreach (var castedScope in castedScopes)
                {
                    //We want to replace the leading @
                    //which is an implementation detail for seq
                    var pattern = @"@\w+";
                    var cleanedKey = Regex.Replace(castedScope.Key, pattern, m => m.Value[1..]);

                    //We will take the most recent version of the scope
                    //and throw the rest away
                    if (result.ContainsKey(cleanedKey))
                        continue;

                    //Add the new scope to the dictionary                   
                    result.Add(cleanedKey, castedScope.Value);
                }
            }
            else
            {
                //The scope is a simple string
                result.Add(scope.State.ToString(), null);
            }
        }

        return result;
    }

    private void PopScope()
    {
        _currentScopes.Value.TryPop(out _);
    }
}


public class LogScope(object state, Action disposeAct) : IDisposable
{
    public object State { get; } = state;
    private readonly Action _disposeAct = disposeAct;

    public void Dispose()
    {
        //Remove the scope for the list of scopes
        //as it is being disposed
        _disposeAct();
    }

    public override string ToString() => State.ToString();
}

