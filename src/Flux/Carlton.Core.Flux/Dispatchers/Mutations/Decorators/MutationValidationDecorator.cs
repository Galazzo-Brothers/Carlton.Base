namespace Carlton.Core.Flux.Dispatchers.Mutations.Decorators;

public class MutationValidationDecorator<TState>(IMutationCommandDispatcher<TState> _decorated, ILogger<MutationValidationDecorator<TState>> _logger) : IMutationCommandDispatcher<TState>
{
    public async Task<Result<MutationCommandResult, FluxError>> Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
    {
        //Validate command
        var isValid = context.MutationCommand.TryValidate(out var validationErrors);
        context.MarkAsValidated(validationErrors);

        //Continue with valid command
        if (isValid)
            return await _decorated.Dispatch(sender, context, cancellationToken);

        //Log validation failures
        _logger.MutationCommandValidationFailure(context.FluxOperationTypeName);
        return new ValidationError(validationErrors, context).ToResult<MutationCommandResult, FluxError>();
    }
}
