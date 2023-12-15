using BlazorDB;
using Blazored.LocalStorage;
using Carlton.Core.Utilities.Logging;
using System.Text.Json.Serialization;

namespace Carlton.Core.Components.Flux.Services;

public class BrowserStorageService : IBrowserStorageService
{
    public event Action LogsCleared;
    private readonly ILocalStorageService _localStorage;
    private readonly IBlazorDbFactory _dbFactory;
    private readonly InMemoryLogger _memoryLogger;
    private readonly SemaphoreSlim _semaphore = new(1);

    public BrowserStorageService(
      ILocalStorageService localStorage,
      IBlazorDbFactory dbFactory,
      InMemoryLogger memoryLogger)
    {
        _localStorage = localStorage;
        _dbFactory = dbFactory;
        _memoryLogger = memoryLogger;
    }

    public async Task<IEnumerable<IndexDBLogMessage>> GetLogs(DateTime dateTime)
    {
        try
        {
            var manager = await _dbFactory.GetDbManager("CarltonFlux");
            var carltonMessage = await manager.Where<CarltonFluxLogMessage>("Logs", "indexDate", dateTime.Date.ToShortDateString());
            return carltonMessage.SelectMany(_ => _.LogMessages);
        }
        catch(Exception ex)
        {
            //swallow for now
            throw;
        }
    }

    public async Task CommitLogs()
    {
        await _semaphore.WaitAsync();
        try
        {
            //Get IndexDB reference
            var manager = await _dbFactory.GetDbManager("CarltonFlux");

            //Group logs by their starting scope
            var dateGroups = _memoryLogger.GetLogMessages()
                                          .GroupBy(_ => _.Timestamp.Date.ToShortDateString());

            //Index by ShortDate string
            foreach (var dateGroup in dateGroups)
            {
                var scopeGroups = dateGroup.GroupBy(_ => InitialScope(_.Scopes));

                //Commit Logs to IndexDb
                foreach (var group in scopeGroups)
                {
                    var indexDbKey = group.Key == string.Empty ? $"UnhandledException_{Guid.NewGuid()}" : group.Key;
                    var ascendingLogs = group.OrderBy(_ => _.Timestamp).Select(_ => new IndexDBLogMessage(_)).ToList();
                    var carltonMessage = new CarltonFluxLogMessage(indexDbKey, dateGroup.Key, ascendingLogs);
                   
                    var result = await manager.AddRecord(new StoreRecord<CarltonFluxLogMessage>()
                    {
                        StoreName = "Logs",
                        Record = carltonMessage,
                    });
                }
            }

            //Clear in-memory logs
            _memoryLogger.ClearLogMessages();

        }
        catch (Exception ex)
        {
            //swallow for now
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task ClearLogs()
    {
        try
        {
            var manager = await _dbFactory.GetDbManager("CarltonFlux");
            await manager.ClearTableAsync("Logs");
            _memoryLogger.ClearLogMessages();
            LogsCleared.Invoke();
        }
        catch (Exception ex)
        {
            //swallow for now
        }
    }

    //Local func to extract initial wrapping scope
    static string InitialScope(string scopeString) => scopeString.Split("=>")?.Last().Trim();
}

file class CarltonFluxLogMessage
{
    public CarltonFluxLogMessage(string key, string indexDate, IEnumerable<IndexDBLogMessage> logMessage)
    {
        Key = key;
        IndexDate = indexDate;
        LogMessages = logMessage;
    }

    [JsonConstructor]
    private CarltonFluxLogMessage()
    {
    }

    public string Key { get; set; }
    public string IndexDate { get; set; }
    public IEnumerable<IndexDBLogMessage> LogMessages { get; set; }
}
