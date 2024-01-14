namespace Carlton.Core.Flux.Contracts;

public interface IComponentExceptionLoggingService
{
    public void LogException(ILogger logger, Exception exception);
}
