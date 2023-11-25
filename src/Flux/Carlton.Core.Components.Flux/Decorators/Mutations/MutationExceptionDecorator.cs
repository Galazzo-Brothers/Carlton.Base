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
        var traceGuid = Guid.NewGuid();
        var commandType = command.GetType().GetDisplayName();
        var commandTraceGuid = $"{displayName}_Command_{commandType}_{traceGuid}";

        try
        {
            using(_logger.BeginScope(commandTraceGuid))
            {
                Log.MutationStarted(_logger, displayName, command);
                await _decorated.Dispatch(sender, command, cancellationToken);
                Log.MutationCompleted(_logger, displayName);
            }
            return Unit.Value;
        }
        catch(MutationCommandFluxException<TState, TCommand>)
        {
            //Exception was already caught, logged and wrapped by other middleware decorators
            throw;
        }
        catch(Exception ex)
        {
            using(_logger.BeginScope(commandTraceGuid))
            {
                //Unhandled Exceptions
                Log.MutationUnhandledError(_logger, ex, displayName);
                throw new MutationCommandFluxException<TState, TCommand>(command, ex);
            }
        }
    }
}
