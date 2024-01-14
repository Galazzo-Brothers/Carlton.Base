//using Carlton.Core.Flux.Exceptions;
//namespace Carlton.Core.Flux.Handlers.Mutations;

//public class MutationLocalStorageDecorator<TState>(
//    IMutationCommandDispatcher<TState> decorated,
//    IFluxState<TState> fluxState,
//    IBrowserStorageService<TState> browserStorage,
//    ILogger<MutationLocalStorageDecorator<TState>> logger) 
//    : IMutationCommandDispatcher<TState>
//{
//    private readonly IMutationCommandDispatcher<TState> _decorated = decorated;
//    private readonly IFluxState<TState> _fluxState = fluxState;
//    private readonly IBrowserStorageService<TState> _browserStorage = browserStorage;
//    private readonly ILogger<MutationLocalStorageDecorator<TState>> _logger = logger;

//    public async Task Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
//    {
//        try
//        {
//            //Save to local storage
//            await _browserStorage.SaveState((IFluxState<TState>)this);

//            //Continue with dispatch and update the state store
//            await _decorated.Dispatch(sender, context, cancellationToken);

//            //Complete the LocalStorage Interception
//            _logger.MutationSaveLocalStorageCompleted(context.CommandTypeName);
//        }
//        catch (JsonException ex)
//        {
//            //Error Serializing JSON
//            _logger.MutationSaveLocalStorageJsonError(ex, typeof(TCommand).GetDisplayName());
//            throw MutationCommandFluxException<TState, TCommand>.LocalStorageJsonError(context, ex);
//        }
//        catch (NotSupportedException ex) when (ex.Message.Contains("Serialization and deserialization"))
//        {
//            //Error Serializing JSON
//            _logger.MutationSaveLocalStorageJsonError(ex, typeof(TCommand).GetDisplayName());
//            throw MutationCommandFluxException<TState, TCommand>.LocalStorageJsonError(context, ex);
//        }
//        catch(Exception ex)
//        {
//            //Error writing to local storage
//            _logger.MutationCommandSaveLocalStorageError(ex, typeof(TCommand).GetDisplayName());
//            throw MutationCommandFluxException<TState, TCommand>.LocalStorageError(context, ex);
//        }
//    }
//}

