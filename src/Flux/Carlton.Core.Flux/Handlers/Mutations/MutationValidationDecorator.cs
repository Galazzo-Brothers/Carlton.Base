using Carlton.Core.Flux.Exceptions;
using Carlton.Core.Flux.Extensions;

namespace Carlton.Core.Flux.Handlers.Mutations;

public class MutationValidationDecorator<TState> : IMutationCommandDispatcher<TState>
{
    private readonly IMutationCommandDispatcher<TState> _decorated;
    private readonly IServiceProvider _provider;
    private readonly ILogger<MutationValidationDecorator<TState>> _logger;

    public MutationValidationDecorator(IMutationCommandDispatcher<TState> decorated, IServiceProvider provider, ILogger<MutationValidationDecorator<TState>> logger)
        => (_decorated, _provider, _logger) = (decorated, provider, logger);

    public async Task Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
    {
        try
        {
            var validatorType = typeof(IValidator<>).MakeGenericType(context.CommandType);
            var validator = (IValidator)_provider.GetService(validatorType);
            validator.ValidateAndThrow(context.MutationCommand);
            context.MarkAsValidated();
            _logger.MutationValidationCompleted(context.CommandTypeName);
            await _decorated.Dispatch(sender, context, cancellationToken);
        }
        catch (ValidationException ex)
        {
            context.MarkAsErrored();
            _logger.MutationValidationError(ex, typeof(TCommand).GetDisplayName());
            throw MutationCommandFluxException<TState, TCommand>.ValidationError(context, ex);
        }
    }
}
