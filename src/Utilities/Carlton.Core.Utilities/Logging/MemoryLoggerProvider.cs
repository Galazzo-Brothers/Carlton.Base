using Microsoft.Extensions.Logging;
namespace Carlton.Core.Utilities.Logging;

/// <summary>
/// Represents a provider for the <see cref="MemoryLogger"/> class.
/// </summary>
public class MemoryLoggerProvider : ILoggerProvider
{
    private readonly MemoryLogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryLoggerProvider"/> class with a specified logger.
    /// </summary>
    /// <param name="logger">The memory logger instance.</param>
    public MemoryLoggerProvider(MemoryLogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryLoggerProvider"/> class with a new <see cref="MemoryLogger"/> instance.
    /// </summary>
    public MemoryLoggerProvider()
    {
        _logger = new MemoryLogger();
    }

    /// <inheritdoc/>
    public ILogger CreateLogger(string categoryName)
    {
        return _logger;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}