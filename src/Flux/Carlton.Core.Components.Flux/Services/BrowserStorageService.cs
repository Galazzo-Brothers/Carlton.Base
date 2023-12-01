using BlazorDB;
using Blazored.LocalStorage;
using Carlton.Core.Utilities.Logging;

namespace Carlton.Core.Components.Flux.Services;

public class BrowserStorageService : IBrowserStorageService
{
    private readonly ILocalStorageService _localStorage;
    private readonly IBlazorDbFactory _dbFactory;
    private readonly InMemoryLogger _memoryLogger;
    private readonly IndexedDbManager _manager;


    public BrowserStorageService(
      ILocalStorageService localStorage,
      IBlazorDbFactory dbFactory,
      InMemoryLogger memoryLogger)
    {
        _localStorage = localStorage;
        _dbFactory = dbFactory;
        _memoryLogger = memoryLogger;
    }

    public async Task CommitLogs()
    {
        try
        {
            //Get IndexDB reference
            var manager = await _dbFactory.GetDbManager("CarltonFlux");

            //Group logs by their starting scope
            var groups = _memoryLogger.GetLogMessages()
                                      .GroupBy(_ => initialScope(_.Scopes));


            manager.ActionCompleted += (sender, args) =>
            {
                //Clear memory logs
                _memoryLogger.ClearLogMessages();
            };

            //Commit Logs to IndexDb
            foreach (var group in groups)
            {
                var ascendingLogs = group.OrderBy(_ => _.Timestamp);
                var carltonMessage = new CarltonFluxLogMessage(group.Key, ascendingLogs);

                var result = await manager.AddRecord(new StoreRecord<CarltonFluxLogMessage>()
                {
                    StoreName = "Logs",
                    Record = carltonMessage
                });
            }


        }
        catch(NullReferenceException)
        {
            var x = 7;
        }

    }

    //Local func to extract initial wrapping scope
    static string initialScope(string scopeString) => scopeString.Split("=>")?.Last().Trim();
}

file class CarltonFluxLogMessage
{
    public CarltonFluxLogMessage(string key, IEnumerable<LogMessage> logMessage)
    {
        Key = key;
        LogMessages = logMessage;
    }

    public string Key { get; set; }
    public IEnumerable<LogMessage> LogMessages { get; set; }
}
