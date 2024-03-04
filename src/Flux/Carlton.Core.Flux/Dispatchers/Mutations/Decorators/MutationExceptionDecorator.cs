using Carlton.Core.Flux.Errors;
namespace Carlton.Core.Flux.Dispatchers.Mutations.Decorators;

public class MutationExceptionDecorator<TState>(
    IMutationCommandDispatcher<TState> _decorated,
    ILogger<MutationExceptionDecorator<TState>> _logger)
    : IMutationCommandDispatcher<TState>
{
    public async Task<Result<MutationCommandResult, MutationCommandError>> Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
    {
        using (_logger.BeginMutationCommandRequestLoggingScopes(context))
        {
            try
            {
                var result = await _decorated.Dispatch(sender, context, cancellationToken);
                _logger.MutationCommandCompleted(context.CommandTypeName);
                return result;
            }

            catch (Exception ex)
            {
                return HandleException(ex, context);
            }
        }
    }

    private Result<MutationCommandResult, MutationCommandError> HandleException<TViewModel, TException>(TException ex, MutationCommandContext<TViewModel> context)
          where TException : Exception
    {
        context.MarkAsErrored(ex);
        using (_logger.BeginRequestExceptionLoggingScopes())
            _logger.MutationCommandErrored(context.CommandTypeName, ex);

        // Return a specific error based on the exception type
        return ex switch
        {
            InvalidOperationException when ex.Message.Contains(FluxLogs.InvalidRefreshUrlMsg) => (Result<MutationCommandResult, MutationCommandError>)new MutationCommandErrors.HttpUrlError(typeof(TViewModel)),
            JsonException or HttpRequestException => (Result<MutationCommandResult, MutationCommandError>)new MutationCommandErrors.HttpError(typeof(TViewModel)),
            _ => (Result<MutationCommandResult, MutationCommandError>)new MutationCommandErrors.UnhandledError(typeof(TViewModel)),
        };
    }
}

