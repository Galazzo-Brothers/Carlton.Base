using Carlton.Core.BrowserStroage;
namespace Carlton.Core.Flux.Debug.Storage;

public class LogsDataAccess(MemoryLogger memoryLogger, IIndexDbService<IndexedLogMessages> indexDb)
    : ILogsDataAccess
{
    private readonly MemoryLogger _memoryLogger = memoryLogger;
    private readonly IIndexDbService<IndexedLogMessages> _indexDb = indexDb;

    public async Task CommitLogs()
    {
        //Group logs by their starting scope
        var dateGroups = _memoryLogger.GetLogMessages()
                                      .GroupBy(_ => _.Timestamp.Date.ToShortDateString());

        var logsToCommit = new List<IndexedLogMessages>();

        //Index by ShortDate string
        foreach (var dateGroup in dateGroups)
        {
            var scopeGroups = dateGroup.GroupBy(_ => InitialScope(_.Scopes));

            //Commit Logs to IndexDb
            foreach (var group in scopeGroups)
            {
                var indexDbKey = group.Key == string.Empty ? $"UnhandledException_{Guid.NewGuid()}" : group.Key;
                var ascendingLogs = group.OrderBy(_ => _.Timestamp).ToList();
                var logMessage = new IndexedLogMessages(indexDbKey, dateGroup.Key, ascendingLogs);
                logsToCommit.Add(logMessage);
            }
        }

        //Save to IndexDb
        await _indexDb.BulkAddRecords(logsToCommit);

        //Clear in-memory logs
        _memoryLogger.ClearLogMessages();


        //Local func to extract initial wrapping scope
        static string InitialScope(string scopeString) => scopeString.Split("=>").Last().Trim();
    }

    public async Task<IEnumerable<IndexedLogMessages>> GetLogs(DateTime dateTime)
    {
        const string IndexDate = "indexDate";
        return await _indexDb.GetRecordsByIndex(IndexDate, dateTime.ToShortDateString());
    }

    public async Task ClearLogs()
    {
        await _indexDb.ClearStore();
    }
}
