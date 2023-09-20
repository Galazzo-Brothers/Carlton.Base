namespace Carlton.Core.Utilities.Logging;

public class InMemoryLoggerProvider : ILoggerProvider
{
    private readonly InMemoryLogger _logger;

    public InMemoryLoggerProvider(InMemoryLogger logger)
    {
        _logger = logger;
    }

    public InMemoryLoggerProvider()
    {
        _logger = new InMemoryLogger();
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