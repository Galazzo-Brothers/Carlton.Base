namespace Carlton.Core.Flux.Dispatchers.Mutations.Decorators;

public class MutationExceptionDecorator<TState>(
    IMutationCommandDispatcher<TState> _decorated,
    ILogger<MutationExceptionDecorator<TState>> _logger)
    : IMutationCommandDispatcher<TState>
{
    public async Task<Result<MutationCommandResult, FluxError>> Dispatch<TCommand>(
        object sender, MutationCommandContext<TCommand> context,
        CancellationToken cancellationToken)
    {
        using (_logger.BeginMutationCommandRequestLoggingScopes(context))
        {
            return await ResultExtensions.SafeExecuteAsync
            (
                async () => await _decorated.Dispatch(sender, context, cancellationToken), //Wrapped Func
                success => HandleSuccess(success, context), //Success Handler
                err => HandleError(err, context), //Error Handler
                ex => HandleException(ex, context) //Exception Handler
            );
        }
    }

    private MutationCommandResult HandleSuccess<TCommand>(MutationCommandResult result, MutationCommandContext<TCommand> context)
    {
        _logger.MutationCommandCompleted(context.FluxOperationTypeName);
        return result;
    }

    private FluxError HandleError<TCommand>(FluxError error, MutationCommandContext<TCommand> context)
    {
        context.MarkAsErrored(error);
        using (_logger.BeginRequestErrorLoggingScopes(error.EventId))
            _logger.ViewModelQueryErrored(context.FluxOperationTypeName, error);

        return error;
    }

    private UnhandledFluxError HandleException<TCommand>(Exception ex, MutationCommandContext<TCommand> context)
    {
        context.MarkAsErrored(ex);
        using (_logger.BeginRequestErrorLoggingScopes(FluxLogs.ViewModel_Unhandled_Error))
            _logger.ViewModelQueryErrored(context.FluxOperationTypeName, ex);

        return new UnhandledFluxError(ex, context);
    }
}

