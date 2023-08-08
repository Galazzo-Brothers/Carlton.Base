namespace Carlton.Core.Components.Flux.Mutations;

public interface IStateMutator
{
    public Task Mutate<TCommand>(object sender, TCommand command);
}