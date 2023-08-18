using Carlton.Core.Components.Flux.Exceptions;

namespace Carlton.Core.Components.Flux.ExceptionHandling;

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
                //This is not a flux related error that has occurred during rendering 
                Log.DataWrapperUnhandledException(logger, erroredComponentType.GetDisplayName(), ex);
                break;
        }
    }
}


