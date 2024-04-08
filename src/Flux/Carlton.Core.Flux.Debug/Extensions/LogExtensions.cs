namespace Carlton.Core.Flux.Debug.Extensions;

internal static class LogExtensions
{
	public static LogMessageSummary ToLogMessageSummary(this FluxDebugLogMessage logMessage)
	{
		return new LogMessageSummary
		{
			Id = logMessage.Id,
			EventId = logMessage.EventId,
			Message = logMessage.Message,
			LogLevel = logMessage.LogLevel,
			Timestamp = logMessage.Timestamp,
			Exception = logMessage?.Exception
		};
	}

	public static ExceptionSummary ToExceptionSummary(this Exception ex)
	{
		return new ExceptionSummary
		{
			ExceptionType = ex.GetType().ToString(),
			Message = ex.Message,
			StackTrace = ex.StackTrace
		};
	}

	public static TraceLogMessage ToTraceLogMessage(this FluxDebugLogMessage logMessage)
	{
		var isQuery = logMessage.GetScopeValue<string>("FluxAction") == "ViewModelQuery";

		return new TraceLogMessage
		{
			Id = logMessage.Id,
			Timestamp = logMessage.Timestamp,
			EventId = logMessage.EventId,
			RequestSucceeded = logMessage.Exception == null,
			FluxAction = ParseFluxActionScope(logMessage),
			TypeDisplayName = isQuery ?
				logMessage.GetScopeValue<string>("ViewModelType") :
				logMessage.GetScopeValue<string>("MutationCommandType")
		};
	}

	public static IEnumerable<TraceLogMessageGroup> MapLogMessagesToTraceLogMessage(this IEnumerable<FluxDebugLogMessage> logMessages)
	{
		return
			logMessages
					.Where(IsFluxActionPredicate)
					.GroupBy(logs => //Group the requests by the initiating requests parentId
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
						var orderedChildren = new List<FluxDebugLogMessage>(orderedMessages);

						//Create a TraceLogMessageGroup object
						return new TraceLogMessageGroup
						{
							ParentEntry = parentEntry.ToTraceLogMessage(),
							ChildEntries = orderedChildren.Select(o => o.ToTraceLogMessage()).ToList()
						};
					}).OrderByDescending(_ => _.ParentEntry.Timestamp).ToList();

		static bool IsFluxActionPredicate(FluxDebugLogMessage log) => log.Scopes.Any(kvp => kvp.Key == "FluxAction");
	}

	public static T GetScopeValue<T>(this FluxDebugLogMessage logMessage, string key)
	{
		var valueExists = logMessage.Scopes.TryGetValue(key, out var value);

		if (!valueExists || value == null)
			throw new InvalidOperationException("Scope was not present on log message");

		return (T)value;
	}

	private static FluxActions ParseFluxActionScope(FluxDebugLogMessage logMessage)
	{
		return Enum.Parse<FluxActions>(logMessage.GetScopeValue<string>("FluxAction"));
	}
}