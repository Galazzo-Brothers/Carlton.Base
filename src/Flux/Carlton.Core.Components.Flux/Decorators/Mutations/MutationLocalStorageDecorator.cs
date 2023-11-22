using BlazorDB;
using Blazored.LocalStorage;
using Carlton.Core.Utilities.Logging;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Xml.Linq;

namespace Carlton.Core.Components.Flux.Decorators.Mutations;

public class MutationLocalStorageDecorator<TState> : IMutationCommandDispatcher<TState>
{
    private readonly IMutationCommandDispatcher<TState> _decorated;
    private readonly IFluxState<TState> _fluxState;
    private readonly ILocalStorageService _localStorage;
    private readonly IBlazorDbFactory _dbFactory;
    private readonly ILogger<MutationLocalStorageDecorator<TState>> _logger;
    private readonly InMemoryLogger _memoryLogger;

    public MutationLocalStorageDecorator(
        IMutationCommandDispatcher<TState> decorated,
        IFluxState<TState> fluxState,
        ILocalStorageService localStorage,
        IBlazorDbFactory dbFactory,
        ILogger<MutationLocalStorageDecorator<TState>> logger,
        InMemoryLogger memoryLogger)
    {
        _decorated = decorated;
        _fluxState = fluxState;
        _localStorage = localStorage;
        _dbFactory = dbFactory;
        _logger = logger;
        _memoryLogger = memoryLogger;
    }

    public async Task<Unit> Dispatch<TCommand>(object sender, TCommand command, CancellationToken cancellationToken)
        where TCommand : MutationCommand
    {
        try
        {
            //Start the LocalStorage Interception
            var commandType = typeof(TCommand).GetDisplayName();
            Log.MutationLocalStorageStarted(_logger, commandType);

            //Continue with dispatch and update the state store
            await _decorated.Dispatch(sender, command, cancellationToken);

            //Take top 100 log entries
            _memoryLogger.ClearAllButMostRecent(50);

            var manager = await _dbFactory.GetDbManager("CarltonFlux");
            await manager.AddRecord(new StoreRecord<IEnumerable<LogMessage>>()
            {
                StoreName = "Logs",
                Record = _memoryLogger.GetLogMessages().ToArray()
            });

            //await manager.UpdateRecord<IEnumerable<LogMessage>>(new UpdateRecord<IEnumerable<LogMessage>>
            //{
            //    Key = 1,
            //    StoreName = "Person",
            //    Record = _memoryLogger.GetLogMessages().ToArray()
            //});



            //Update LocalStorage
            //await _localStorage.SetItemAsync("carltonFluxState", _fluxState.State, cancellationToken);
            await _localStorage.SetItemAsync("carltonFluxLogs", _memoryLogger.GetLogMessages(), cancellationToken);

            //Complete the LocalStorage Interception
            Log.MutationLocalStorageStarted(_logger, commandType);

            return Unit.Value;
        }
        catch (JsonException ex)
        {
            //Error Serializing JSON
            Log.MutationJsonError(_logger, ex, typeof(TCommand).GetDisplayName());
            throw MutationCommandFluxException<TState, TCommand>.LocalStorageJsonError(command, ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Serialization and deserialization"))
        {
            //Error Serializing JSON
            Log.MutationJsonError(_logger, ex, typeof(TCommand).GetDisplayName());
            throw MutationCommandFluxException<TState, TCommand>.LocalStorageJsonError(command, ex);
        }
    }
}
