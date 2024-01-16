namespace Carlton.Core.Flux.Contracts;

public interface IViewModelMapper<TState>
{
    public TViewModel Map<TViewModel>(TState state);
}
