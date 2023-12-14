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
        var commandType = typeof(TCommand).GetDisplayName();
        var traceGuid = Guid.NewGuid();
        var commandTraceGuid = $"MutationCommand_{commandType}_{traceGuid}";

        try
        {
            using(_logger.BeginScope(commandTraceGuid))
            {
                _logger.MutationStarted(commandType);
                await _decorated.Dispatch(sender, command, cancellationToken);
                _logger.MutationCompleted(commandType);
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
                _logger.MutationUnhandledError(ex, commandType);
                throw new MutationCommandFluxException<TState, TCommand>(command, ex);
            }
        }
    }
}
