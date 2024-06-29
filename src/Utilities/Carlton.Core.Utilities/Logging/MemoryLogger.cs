using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using System.Threading;
namespace Carlton.Core.Utilities.Logging;

/// <summary>
/// Represents a memory logger that stores log messages in a concurrent queue.
/// </summary>
public class MemoryLogger : ILogger
{
	private readonly ConcurrentQueue<LogMessage> _logMessages;
	private readonly AsyncLocal<ConcurrentStack<LogScope>> _currentScopes;
	private readonly string _logCategory;

	internal MemoryLogger(
		string logCategory,
		ConcurrentQueue<LogMessage> logMessages,
		AsyncLocal<ConcurrentStack<LogScope>> currentScopes)
	{
		_logCategory = logCategory;
		_logMessages = logMessages;
		_currentScopes = currentScopes;
	}

	/// <inheritdoc/>
	public IDisposable BeginScope<TState>(TState state)
	{
		var scope = new LogScope(state, PopScope);
		_currentScopes.Value ??= new();
		_currentScopes.Value.Push(scope);
		return scope;
	}

	/// <inheritdoc/>
	public bool IsEnabled(LogLevel logLevel)
	{
		return true; // Log all levels for simplicity.
	}

	/// <inheritdoc/>
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
			Category = _logCategory,
			Scopes = GetCurrentScopes()
		};

		_logMessages.Enqueue(message);
	}

	/// <summary>
	/// Retrieves all the log messages currently stored in the memory logger.
	/// </summary>
	/// <returns>An array of log messages.</returns>
	public IEnumerable<LogMessage> GetLogMessages()
	{
		return _logMessages;
	}

	/// <summary>
	/// Clears all log messages from the memory logger.
	/// </summary>
	public void ClearLogMessages()
	{
		while (_logMessages.TryDequeue(out _))
		{ }
	}

	/// <summary>
	/// Clears all log messages from the memory logger except for the most recent ones, keeping a specified number.
	/// </summary>
	/// <param name="keepCount">The number of log messages to keep.</param>
	public void ClearAllButMostRecent(int keepCount)
	{
		while (_logMessages.Count > keepCount)
			_logMessages.TryDequeue(out _);
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


