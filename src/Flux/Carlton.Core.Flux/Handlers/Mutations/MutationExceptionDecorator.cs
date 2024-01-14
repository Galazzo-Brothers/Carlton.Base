using Carlton.Core.Flux.Exceptions;
namespace Carlton.Core.Flux.Handlers.Mutations;

public class MutationExceptionDecorator<TState>(
    IMutationCommandDispatcher<TState> decorated)
    : IMutationCommandDispatcher<TState>
{
    private readonly IMutationCommandDispatcher<TState> _decorated = decorated;

    public async Task Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
    {
        try
        {
            await _decorated.Dispatch(sender, context, cancellationToken);
        }
        catch (MutationCommandFluxException<TState, TCommand> ex)
            when (ex.EventId == LogEvents.Mutation_SaveLocalStorage_JSON_Error ||
                  ex.EventId == LogEvents.Mutation_SaveLocalStorage_Error)
        {
            //Swallow here so component will still render
        }
        catch (Exception ex)
        {
            //Unhandled Exceptions
            throw MutationCommandFluxException<TState, TCommand>.UnhandledError(context, ex);
        }
    }
}

