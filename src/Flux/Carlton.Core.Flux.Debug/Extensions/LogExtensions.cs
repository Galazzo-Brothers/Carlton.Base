namespace Carlton.Core.Flux.Debug.Extensions;

public static class LogExtensions
{
    //public static LogEntry ToLogEntry(this LogMessage log)
    //{
    //    return new LogEntry
    //    {
    //        EventId = log.EventId.Id,
    //        EventName = log.EventId.Name,
    //        LogLevel = log.LogLevel,
    //        Message = log.Message,
    //        Scopes = log.Scopes,
    //        Timestamp = log.Timestamp,
    //        Exception = log.Exception?.ToExceptionEntry()
    //    };
    //}

    public static ExceptionEntry ToExceptionEntry(this Exception ex)
    {
        return new ExceptionEntry
        {
            ExceptionType = ex.GetType().ToString(),
            Message = ex.Message,
            StackTrace = ex.StackTrace
        };
    }
}