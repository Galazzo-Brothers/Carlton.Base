namespace Carlton.Core.Flux.Exceptions.ExceptionHandling;

public class ComponentExceptionLoggingService : IComponentExceptionLoggingService
{
    public void LogException(ILogger logger, Exception exception)
    {
        switch (exception)
        {
            case FluxException:
                //Flux Exceptions are already logged
                break;
            case Exception ex:
                //This is not a flux related error.
                //It is an error that has occurred during rendering 
                logger.LogError(ex, "An exception has occured while rendering a flux component");
                break;
        }
    }
}


