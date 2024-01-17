namespace Carlton.Core.Flux.Handlers.Mutations;

public class MutationValidationDecorator<TState>(IMutationCommandDispatcher<TState> _decorated) : IMutationCommandDispatcher<TState>
{
    public async Task Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
    {
        var isValid = context.MutationCommand.TryValidate(out var validationErrors);
        context.MarkAsValidated(validationErrors);

        if (!isValid)
            throw new ValidationException(string.Join(Environment.NewLine, validationErrors.Select(result => result)));

        await _decorated.Dispatch(sender, context, cancellationToken);
    }
}
