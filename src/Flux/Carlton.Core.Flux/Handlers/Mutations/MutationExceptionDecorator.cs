using Carlton.Core.Flux.Exceptions;
namespace Carlton.Core.Flux.Handlers.Mutations;

public class MutationExceptionDecorator<TState>(
    IMutationCommandDispatcher<TState> decorated,
    ILogger<MutationExceptionDecorator<TState>> logger)
    : IMutationCommandDispatcher<TState>
{
    private readonly IMutationCommandDispatcher<TState> _decorated = decorated;
    private readonly ILogger<MutationExceptionDecorator<TState>> _logger = logger;

    public async Task Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
    {
        using (_logger.BeginScope(LogEvents.FluxAction))
        using (_logger.BeginScope(LogEvents.CommandScope, context))
        {
            try
            {
                await _decorated.Dispatch(sender, context, cancellationToken);
                context.MarkAsCompleted();
                _logger.MutationCompleted(context.CommandTypeName);
            }
            catch (MutationCommandFluxException<TState, TCommand> ex)
                when (ex.EventID == LogEvents.Mutation_SaveLocalStorage_JSON_Error ||
                     ex.EventID == LogEvents.Mutation_SaveLocalStorage_Error)
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
                //Unhandled Exceptions
                context.MarkAsErrored();
                _logger.MutationError(ex, context.CommandTypeName);
                throw new MutationCommandFluxException<TState, TCommand>(context, ex);
            }
        }
    }
}
