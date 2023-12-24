namespace Carlton.Core.Flux.Contracts;

public interface IComponentExceptionLoggingService
{
    public void LogException<TComponent>(ILogger logger, Exception exception);
    public void LogException(ILogger logger, Exception exception, Type erroredComponentType);
}
