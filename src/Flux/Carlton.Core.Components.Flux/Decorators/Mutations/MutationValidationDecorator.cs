namespace Carlton.Core.Components.Flux.Decorators.Commands;

public class MutationValidationDecorator<TState> : IMutationCommandDispatcher<TState>
{
    private readonly IMutationCommandDispatcher<TState> _decorated;
    private readonly IServiceProvider _provider;
    private readonly ILogger<MutationValidationDecorator<TState>> _logger;

    public MutationValidationDecorator(IMutationCommandDispatcher<TState> decorated, IServiceProvider provider, ILogger<MutationValidationDecorator<TState>> logger)
        => (_decorated, _provider, _logger) = (decorated, provider, logger);

    public async Task<Unit> Dispatch<TCommand>(object sender, TCommand command, CancellationToken cancellationToken)
        where TCommand : MutationCommand
    {
        try
        {
            var displayName = typeof(TCommand).GetDisplayName();
            _logger.MutationValidationStarted(displayName);
            var validator = _provider.GetService<IValidator<TCommand>>();
            validator.ValidateAndThrow(command);
            _logger.MutationValidationCompleted(displayName);
            return await _decorated.Dispatch(sender, command, cancellationToken);
        }
        catch (ValidationException ex)
        {
            _logger.MutationValidationError(ex, typeof(TCommand).GetDisplayName());
            throw MutationCommandFluxException<TState, TCommand>.ValidationError(command, ex);
        }
    }
}
