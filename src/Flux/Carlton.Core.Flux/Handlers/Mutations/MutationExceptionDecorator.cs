using Carlton.Core.Flux.Exceptions;
namespace Carlton.Core.Flux.Handlers.Mutations;

public class MutationExceptionDecorator<TState>(
    IMutationCommandDispatcher<TState> _decorated,
    ILogger<MutationExceptionDecorator<TState>> _logger)
    : IMutationCommandDispatcher<TState>
{
    public async Task Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
    {
        using(_logger.BeginScope(LogEvents.GetMutationCommandRequestLoggingScopes(_logger, context)))
        {
            try
            {
                await _decorated.Dispatch(sender, context, cancellationToken);
                _logger.MutationCompleted(context.CommandTypeName);
            }
            catch(MutationCommandFluxException<TState, TCommand> ex)
                when(ex.EventId == LogEvents.Mutation_SaveLocalStorage_JSON_Error ||
                      ex.EventId == LogEvents.Mutation_SaveLocalStorage_Error)
            {
                //Swallow here so component will still render
            }
            catch(Exception ex)
            {
                context.MarkAsErrored(ex);
                var wrappedException = WrapException(context, ex);
                _logger.LogError(wrappedException.EventId, wrappedException, wrappedException.Message);
                throw wrappedException;
            }
        }
    }

    private static MutationCommandFluxException<TState, TCommand> WrapException<TCommand>(
       MutationCommandContext<TCommand> context,
       Exception exception)
    {
        return exception switch
        {
            ValidationException ex => MutationCommandFluxException<TState, TCommand>.ValidationError(context, ex), //Validation Error
            InvalidOperationException ex when ex.Message.Contains(LogEvents.InvalidRefreshUrlMsg) => MutationCommandFluxException<TState, TCommand>.HttpUrlError(context, ex),//URL Construction Error
            JsonException ex => MutationCommandFluxException<TState, TCommand>.HttpJsonError(context, ex),//Error Serializing JSON
            NotSupportedException ex when ex.Message.Contains("Serialization and deserialization") => MutationCommandFluxException<TState, TCommand>.HttpJsonError(context, ex),//Error Serializing JSON
            HttpRequestException ex => MutationCommandFluxException<TState, TCommand>.HttpError(context, ex),//Http Exceptions
            _ => MutationCommandFluxException<TState, TCommand>.UnhandledError(context, exception),//Unhandled Exception
        };
    }
}

