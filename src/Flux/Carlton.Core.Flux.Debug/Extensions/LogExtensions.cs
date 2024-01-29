using Carlton.Core.Flux.Models;

namespace Carlton.Core.Flux.Debug.Extensions;

public static class LogExtensions
{
    public static ExceptionEntry ToExceptionEntry(this Exception ex)
    {
        return new ExceptionEntry
        {
            ExceptionType = ex.GetType().ToString(),
            Message = ex.Message,
            StackTrace = ex.StackTrace
        };
    }

    public static TraceLogMessage MapLogMessageToTraceLogMessage(this LogMessage logMessage)
    {
        var isQuery = logMessage.GetScopeValue<string>("FluxAction") == "ViewModelQuery";

        return new TraceLogMessage
        {
            Timestamp = logMessage.Timestamp,
            EventId = logMessage.EventId,
            RequestSucceeded = logMessage.Exception == null,
            FluxAction = ParseFluxActionScope(logMessage),
            TypeDisplayName = isQuery ?
                logMessage.GetScopeValue<string>("ViewModelType") :
                logMessage.GetScopeValue<string>("MutationCommandType"),
            RequestContext = logMessage.GetScopeValue<BaseRequestContext>("FluxRequestContext")
        };
    }

    public static T GetScopeValue<T>(this LogMessage logMessage, string key)
    {
        var valueExists = logMessage.Scopes.TryGetValue(key, out var value);

        if (!valueExists || value == null)
            throw new InvalidOperationException("Scope was not present on log message");

        return (T)value;
    }

    private static FluxActions ParseFluxActionScope(LogMessage logMessage)
    {
        return Enum.Parse<FluxActions>(logMessage.GetScopeValue<string>("FluxAction"));
    }
}