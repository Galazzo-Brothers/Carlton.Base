﻿namespace Carlton.Core.Flux.Exceptions.ExceptionHandling;

public class ComponentExceptionLoggingService : IComponentExceptionLoggingService
{
    public void LogException<TComponent>(ILogger logger, Exception exception)
    {
        LogException(logger, exception, typeof(TComponent));
    }

    public void LogException(ILogger logger, Exception exception, Type erroredComponentType)
    {
        switch (exception)
        {
            case FluxException:
                //Do nothing, it has already been logged by the flux library
                break;
            case Exception ex:
                //This is not a flux related error.
                //It is an error that has occurred during rendering 
                logger.DataWrapperUnhandledException(ex, erroredComponentType.GetDisplayName());
                break;
        }
    }
}

