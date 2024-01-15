namespace Carlton.Core.Flux.Handlers.Mutations;

public class MutationCommandHandler<TState>(
    IFluxStateObservable<TState> _observable,
    IMutationResolver<TState> _resolver,
    TState _state)
    : IMutationCommandHandler<TState>
{
    public async Task Handle<TCommand>(MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
    {
        //Find the appropriate mutation
        var mutation = _resolver.Resolve(context.MutationCommand.GetType());
        var stateEvent = mutation.StateEvent;

        //Create the new state using the mutation
        var newState = mutation.Mutate(_state, context.MutationCommand);

        //Update the state
        newState.Adapt(_state);
        context.MarkAsStateMutationApplied(stateEvent);

        //Notify of state changed
        var args = new FluxStateChangedEventArgs(stateEvent, context.RequestID);
        await _observable.OnStateChanged(args);
        context.MarkChildRequestsSucceeded();
    }
}

