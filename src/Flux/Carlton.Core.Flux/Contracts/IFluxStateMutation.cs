namespace Carlton.Core.Flux.Contracts;

public interface IFluxStateMutation<TState>
{
    public string StateEvent { get; }
    public TState Mutate(TState state, object command);
}

public interface IFluxStateMutation<TState, in TCommand> : IFluxStateMutation<TState>
    where TCommand : MutationCommand
{
    public TState Mutate(TState state, TCommand command);
}

public abstract class FluxStateMutationBase<TState, TCommand> : IFluxStateMutation<TState, TCommand>
    where TCommand : MutationCommand
{
    public abstract string StateEvent { get; }

    public abstract TState Mutate(TState state, TCommand command);

    TState IFluxStateMutation<TState>.Mutate(TState state, object command)
    {
        if (command is not TCommand typedCommand)
            throw new ArgumentException("Invalid state or command type");
        
        return Mutate(state, typedCommand);
    }
}

