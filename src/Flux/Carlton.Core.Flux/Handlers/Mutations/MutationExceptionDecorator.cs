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
                when (ex.EventId == LogEvents.Mutation_SaveLocalStorage_JSON_Error ||
                      ex.EventId == LogEvents.Mutation_SaveLocalStorage_Error)
            {
                //Swallow here so component will still render
                context.MarkAsErrored(ex);
            }
            catch (MutationCommandFluxException<TState, TCommand> ex)
            {
                context.MarkAsErrored(ex);
                throw;  //Exception was already caught, logged and wrapped by other middleware decorators
            }
            catch (Exception ex)
            {
                //Unhandled Exceptions
                context.MarkAsErrored(ex);
                throw MutationCommandFluxException<TState, TCommand>.UnhandledError(context, ex);
            }
        }
    }
}
