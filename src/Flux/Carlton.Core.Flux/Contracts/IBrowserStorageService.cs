namespace Carlton.Core.Flux.Contracts;

/// <summary>
/// Represents a service for loading and saving state to browser storage.
/// </summary>
/// <typeparam name="TState">The type of the state to be loaded and saved.</typeparam>
public interface IBrowserStorageService<TState>
{
	/// <summary>
	/// Asynchronously loads the state from browser storage.
	/// </summary>
	/// <returns>The loaded state.</returns>
	public Task<TState> LoadState();

	/// <summary>
	/// Asynchronously saves the state to browser storage.
	/// </summary>
	/// <param name="state">The state to be saved.</param>
	public Task SaveState(TState state);
}

