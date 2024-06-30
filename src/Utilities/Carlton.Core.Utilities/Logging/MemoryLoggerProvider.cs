using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Threading;
namespace Carlton.Core.Utilities.Logging;

/// <summary>
/// Represents a provider for the <see cref="MemoryLogger"/> class.
/// </summary>
public class MemoryLoggerProvider : ILoggerProvider
{
	private readonly ConcurrentQueue<LogMessage> _logMessagesQueue = new();
	private readonly AsyncLocal<ConcurrentStack<LogScope>> _currentScopes = new();

	/// <inheritdoc/>
	public ILogger CreateLogger(string categoryName)
	{
		return new MemoryLogger(categoryName ?? "Default", _logMessagesQueue, _currentScopes);
	}

	/// <inheritdoc/>
	public void Dispose()
	{
		GC.SuppressFinalize(this);
	}
}