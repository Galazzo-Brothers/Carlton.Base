using Carlton.Core.Flux.Dispatchers;

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

    public static IEnumerable<TraceLogMessageGroup> MapLogMessagesToTraceLogMessage(this IEnumerable<LogMessage> logMessages)
    {
        return
            logMessages
                    .Where(IsFluxActionPredicate)
                    .GroupBy(logs => //Group the requests by the initating requests parentId
                    {
                        //check if request is a parent request
                        var hasParentRequestId = logs.Scopes.Any(kvp => kvp.Key == "FluxParentRequestId");

                        if (hasParentRequestId) //Check for a FluxParentRequestId
                            return logs.Scopes.FirstOrDefault(kvp => kvp.Key == "FluxParentRequestId").Value;
                        else //fallback to RequestId
                            return logs.Scopes.FirstOrDefault(kvp => kvp.Key == "FluxRequestId").Value;
                    })
                    .Select(group =>
                    {
                        //reverse chronological order
                        var orderedMessages = group.OrderByDescending(_ => _.Timestamp).ToList();
                        var parentEntry = orderedMessages.First(); //parent entry is first

                        //subsequent requests are children of the parent request
                        orderedMessages.RemoveAt(0);
                        var orderedChildren = new List<LogMessage>(orderedMessages);

                        //Create a TraceLogMessageGroup object
                        return new TraceLogMessageGroup
                        {
                            ParentEntry = parentEntry.MapLogMessageToTraceLogMessage(),
                            ChildEntries = orderedChildren.Select(_ => _.MapLogMessageToTraceLogMessage())
                        };
                    }).OrderByDescending(_ => _.ParentEntry.Timestamp).ToList();

        static bool IsFluxActionPredicate(LogMessage log) => log.Scopes.Any(kvp => kvp.Key == "FluxAction");
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