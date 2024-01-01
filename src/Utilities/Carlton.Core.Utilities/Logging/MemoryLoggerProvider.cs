namespace Carlton.Core.Utilities.Logging;

public class MemoryLoggerProvider : ILoggerProvider
{
    private readonly MemoryLogger _logger;

    public MemoryLoggerProvider(MemoryLogger logger)
    {
        _logger = logger;
    }

    public MemoryLoggerProvider()
    {
        _logger = new MemoryLogger();
    }

    public ILogger CreateLogger(string categoryName)
    {
        return _logger;
    }

    public void Dispose()
    {
        // Optionally, you can implement Dispose logic if needed.
    }
}