using Carlton.Core.Flux.Extensions;

namespace Carlton.Core.Flux.Handlers.Mutations;

public class MutationValidationDecorator<TState>(
    IMutationCommandDispatcher<TState> _decorated,
    IServiceProvider _provider
    ) : IMutationCommandDispatcher<TState>
{
    public async Task Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
    {
        var validatorType = typeof(IValidator<>).MakeGenericType(context.CommandType);
        var validator = (IValidator)_provider.GetService(validatorType);
        validator.ValidateAndThrow(context.MutationCommand);
        context.MarkAsValidated();
        await _decorated.Dispatch(sender, context, cancellationToken);
    }
}
