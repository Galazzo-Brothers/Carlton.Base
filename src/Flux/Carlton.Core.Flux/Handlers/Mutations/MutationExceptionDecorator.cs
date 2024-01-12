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
        var scopes = LogEvents.GetMutationCommandRequestLoggingScopes(_logger, context);
        using (scopes)
        {
            try
            {
                await _decorated.Dispatch(sender, context, cancellationToken);
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
                context.MarkAsErrored(ex);
                _logger.MutationError(ex, context.CommandTypeName);
                throw new MutationCommandFluxException<TState, TCommand>(context, ex);
            }
        }
    }
}
