namespace Carlton.Core.Flux.Dispatchers.Mutations.Decorators;

public class MutationValidationDecorator<TState>(IMutationCommandDispatcher<TState> _decorated) : IMutationCommandDispatcher<TState>
{
    public async Task<Result<MutationCommandResult, MutationCommandFluxError>> Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
    {
        var isValid = context.MutationCommand.TryValidate(out var validationErrors);
        context.MarkAsValidated(validationErrors);

        if (!isValid)
            throw new ValidationException(string.Join(Environment.NewLine, validationErrors.Select(result => result)));

        return await _decorated.Dispatch(sender, context, cancellationToken);
    }
}
