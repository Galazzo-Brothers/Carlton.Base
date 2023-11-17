namespace Carlton.Core.Components.Flux.Decorators.Commands;

public class MutationExceptionDecorator<TState> : IMutationCommandDispatcher<TState>
{
    private readonly IMutationCommandDispatcher<TState> _decorated;
    private readonly ILogger<MutationExceptionDecorator<TState>> _logger;

    public MutationExceptionDecorator(IMutationCommandDispatcher<TState> decorated, ILogger<MutationExceptionDecorator<TState>> logger)
        => (_decorated, _logger) = (decorated, logger);

    public async Task<Unit> Dispatch<TCommand>(object sender, TCommand command, CancellationToken cancellationToken)
        where TCommand : MutationCommand
    {
        var displayName = typeof(TCommand).GetDisplayName();
        using (_logger.BeginScope(Log.MutationScopeMessage, displayName, command.CommandID))
        {
            try
            {
                Log.MutationStarted(_logger, displayName, command);
                await _decorated.Dispatch(sender, command, cancellationToken);
                Log.MutationCompleted(_logger, displayName);
                return Unit.Value;
            }
            catch (MutationCommandFluxException<TState, TCommand>)
            {
                //Exception was already caught, logged and wrapped by other middleware decorators
                throw;
            }
            catch (Exception ex)
            {
                //Unhandled Exceptions
                Log.MutationUnhandledError(_logger, ex, displayName);
                throw new MutationCommandFluxException<TState, TCommand>(command, ex);
            }
        }
    }
}
