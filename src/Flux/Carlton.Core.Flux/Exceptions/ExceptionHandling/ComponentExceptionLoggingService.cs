namespace Carlton.Core.Flux.Exceptions.ExceptionHandling;

public class ComponentExceptionLoggingService : IComponentExceptionLoggingService
{
    public void LogException(ILogger logger, Exception exception)
    {
        switch (exception)
        {
            case FluxException ex:
                logger.BeginScope(LogEvents.FluxRequestContext, ex.Context);
                logger.LogError(ex.EventId, ex, ex.Message, ex.Context);
                break;
            case Exception ex:
                //This is not a flux related error.
                //It is an error that has occurred during rendering 
                logger.LogError(ex, "An unhandled exception has occured");
                break;
        }
    }
}


