namespace Carlton.Core.Flux.Contracts;

public interface IBrowserStorageService<TState>
{
    public Task<IFluxState<TState>> LoadState();
    public Task SaveState(IFluxState<TState> state);
}

public class DummyBrowserStorageService<TState>(IFluxState<TState> state) : IBrowserStorageService<TState>
{
    private readonly IFluxState<TState> _state = state;

    public Task<IFluxState<TState>> LoadState()
        => Task.FromResult(state);

    public Task SaveState(IFluxState<TState> state)
        => Task.CompletedTask;
}