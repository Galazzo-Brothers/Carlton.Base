using Carlton.Core.Flux.Exceptions;
namespace Carlton.Core.Flux.Handlers.Mutations;

public class MutationLocalStorageDecorator<TState>(
    IMutationCommandDispatcher<TState> decorated,
    IFluxState<TState> fluxState,
    IBrowserStorageService<TState> browserStorage,
    ILogger<MutationLocalStorageDecorator<TState>> logger) 
    : IMutationCommandDispatcher<TState>
{
    private readonly IMutationCommandDispatcher<TState> _decorated = decorated;
    private readonly IFluxState<TState> _fluxState = fluxState;
    private readonly IBrowserStorageService<TState> _browserStorage = browserStorage;
    private readonly ILogger<MutationLocalStorageDecorator<TState>> _logger = logger;

    public async Task Dispatch<TCommand>(object sender, TCommand command, CancellationToken cancellationToken)
        where TCommand : MutationCommand
    {
        try
        {
            //Start the LocalStorage Interception
            var commandType = typeof(TCommand).GetDisplayName();
            _logger.MutationLocalStorageStarted(commandType);

            //Save to local storage
            await _browserStorage.SaveState((IFluxState<TState>)this);

            //Continue with dispatch and update the state store
            await _decorated.Dispatch(sender, command, cancellationToken);

            //Complete the LocalStorage Interception
            _logger.MutationLocalStorageStarted(commandType);
        }
        catch (JsonException ex)
        {
            //Error Serializing JSON
            _logger.MutationLocalStorageJsonError(ex, typeof(TCommand).GetDisplayName());
            throw MutationCommandFluxException<TState, TCommand>.LocalStorageJsonError(command, ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Serialization and deserialization"))
        {
            //Error Serializing JSON
            _logger.MutationLocalStorageJsonError(ex, typeof(TCommand).GetDisplayName());
            throw MutationCommandFluxException<TState, TCommand>.LocalStorageJsonError(command, ex);
        }
        catch(Exception ex)
        {
            //Error writing to local storage
            _logger.MutationLocalStorageUnhandledError(ex, typeof(TCommand).GetDisplayName());
            throw MutationCommandFluxException<TState, TCommand>.LocalStorageError(command, ex);
        }
    }
}

