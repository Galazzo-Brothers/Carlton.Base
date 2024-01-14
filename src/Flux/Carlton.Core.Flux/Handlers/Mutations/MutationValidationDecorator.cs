using Carlton.Core.Flux.Exceptions;
using Carlton.Core.Flux.Extensions;

namespace Carlton.Core.Flux.Handlers.Mutations;

public class MutationValidationDecorator<TState> : IMutationCommandDispatcher<TState>
{
    private readonly IMutationCommandDispatcher<TState> _decorated;
    private readonly IServiceProvider _provider;

    public MutationValidationDecorator(IMutationCommandDispatcher<TState> decorated, IServiceProvider provider)
        => (_decorated, _provider) = (decorated, provider);

    public async Task Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
    {
        try
        {
            var validatorType = typeof(IValidator<>).MakeGenericType(context.CommandType);
            var validator = (IValidator)_provider.GetService(validatorType);
            validator.ValidateAndThrow(context.MutationCommand);
            context.MarkAsValidated();
            await _decorated.Dispatch(sender, context, cancellationToken);
        }
        catch (ValidationException ex)
        {
            throw MutationCommandFluxException<TState, TCommand>.ValidationError(context, ex);
        }
    }
}
