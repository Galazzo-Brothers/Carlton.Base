namespace Carlton.Core.Flux.Contracts;

public interface IBrowserStorageService<TState>
{
    public Task<TState> LoadState();
    public Task SaveState(TState state);
}

public class DummyBrowserStorageService<TState>(TState state) : IBrowserStorageService<TState>
{
    private readonly TState _state = state;

    public Task<TState> LoadState()
        => Task.FromResult(_state);

    public Task SaveState(TState state)
        => Task.CompletedTask;
}