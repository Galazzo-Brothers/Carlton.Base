using Carlton.Core.Flux.Exceptions;
namespace Carlton.Core.Flux.Handlers.Mutations;

public class MutationExceptionDecorator<TState>(
    IMutationCommandDispatcher<TState> decorated,
    ILogger<MutationExceptionDecorator<TState>> logger)
    : IMutationCommandDispatcher<TState>
{
    private readonly IMutationCommandDispatcher<TState> _decorated = decorated;
    private readonly ILogger<MutationExceptionDecorator<TState>> _logger = logger;

    public async Task Dispatch<TCommand>(object sender, TCommand command, CancellationToken cancellationToken)
        where TCommand : MutationCommand
    {
        var commandType = typeof(TCommand).GetDisplayName();
        var traceGuid = Guid.NewGuid();
        var commandTraceGuid = $"MutationCommand_{commandType}_{traceGuid}";

        try
        {
            using (_logger.BeginScope(commandTraceGuid))
            {
                _logger.MutationStarted(commandType);
                await _decorated.Dispatch(sender, command, cancellationToken);
                _logger.MutationCompleted(commandType);
            }
        }
        catch(MutationCommandFluxException<TState, TCommand> ex) 
            when (ex.EventID == LogEvents.Mutation_LocalStorage_JSON_Error ||
                 ex.EventID == LogEvents.Mutation_LocalStorage_Unhandled_Error)
        {
            //Exception is logged
            //Swallow here so component will still render
        }
        catch (MutationCommandFluxException<TState, TCommand>)
        {
            //Exception was already caught, logged and wrapped by other middleware decorators
            throw;
        }
        catch (Exception ex)
        {
            using (_logger.BeginScope(commandTraceGuid))
            {
                //Unhandled Exceptions
                _logger.MutationUnhandledError(ex, commandType);
                throw new MutationCommandFluxException<TState, TCommand>(command, ex);
            }
        }
    }
}
