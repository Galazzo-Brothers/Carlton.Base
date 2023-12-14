using System.Text.Json;

namespace Carlton.Core.Components.Flux.Decorators.Mutations;

public class MutationLocalStorageDecorator<TState> : IMutationCommandDispatcher<TState>
{
    private readonly IMutationCommandDispatcher<TState> _decorated;
    private readonly IFluxState<TState> _fluxState;
    private readonly IBrowserStorageService _browserStorage;
    private readonly ILogger<MutationLocalStorageDecorator<TState>> _logger;

    public MutationLocalStorageDecorator(
        IMutationCommandDispatcher<TState> decorated,
        IFluxState<TState> fluxState,
        IBrowserStorageService browserStorage,
        ILogger<MutationLocalStorageDecorator<TState>> logger)
    {
        _decorated = decorated;
        _fluxState = fluxState;
        _browserStorage = browserStorage;
        _logger = logger;
    }

    public async Task<Unit> Dispatch<TCommand>(object sender, TCommand command, CancellationToken cancellationToken)
        where TCommand : MutationCommand
    {
        try
        {
            //Start the LocalStorage Interception
            var commandType = typeof(TCommand).GetDisplayName();
            _logger.MutationLocalStorageStarted(commandType);

            //Continue with dispatch and update the state store
            await _decorated.Dispatch(sender, command, cancellationToken);

            //Complete the LocalStorage Interception
            _logger.MutationLocalStorageStarted(commandType);

            return Unit.Value;
        }
        catch (JsonException ex)
        {
            //Error Serializing JSON
            _logger.MutationJsonError(ex, typeof(TCommand).GetDisplayName());
            throw MutationCommandFluxException<TState, TCommand>.LocalStorageJsonError(command, ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Serialization and deserialization"))
        {
            //Error Serializing JSON
            _logger.MutationJsonError(ex, typeof(TCommand).GetDisplayName());
            throw MutationCommandFluxException<TState, TCommand>.LocalStorageJsonError(command, ex);
        }
    }
}


