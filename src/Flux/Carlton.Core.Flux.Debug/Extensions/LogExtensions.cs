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

    public static string GetScopeValue(this LogMessage logMessage, string key)
    {
        var valueExists = logMessage.Scopes.TryGetValue(key, out var value);

        if (!valueExists || value == null)
            throw new InvalidOperationException("Scope was not present on log message");

        return value.ToString();
    }

    public static TraceLogMessage MapLogMessageToTraceLogMessage(this LogMessage logMessage)
    {
        return new TraceLogMessage
        {
            Timestamp = logMessage.Timestamp,
            EventId = logMessage.EventId,
            RequestSucceeded = logMessage.Exception == null,
            FluxAction = ParseFluxActionScope(logMessage),
            TypeDisplayName = logMessage.GetScopeValue("FluxAction") == "ViewModelQuery" ?
                logMessage.GetScopeValue("ViewModelType") :
                logMessage.GetScopeValue("MutationCommandType")
        };
    }

    private static FluxActions ParseFluxActionScope(LogMessage logMessage)
    {
        return Enum.Parse<FluxActions>(logMessage.GetScopeValue("FluxAction"));
    }
}