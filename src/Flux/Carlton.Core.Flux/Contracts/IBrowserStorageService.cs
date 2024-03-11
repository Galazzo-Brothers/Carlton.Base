namespace Carlton.Core.Flux.Contracts;

public interface IBrowserStorageService<TState>
{
	public Task<TState> LoadState();
	public Task SaveState(TState state);
}

