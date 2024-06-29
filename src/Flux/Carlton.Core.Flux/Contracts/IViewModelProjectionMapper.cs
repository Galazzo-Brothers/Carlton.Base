namespace Carlton.Core.Flux.Contracts;

/// <summary>
/// Represents a mapper that converts a state object of type <typeparamref name="TState"/> to a view model of type <typeparamref name="TViewModel"/>.
/// </summary>
/// <typeparam name="TState">The type of the state object to map from.</typeparam>
public interface IViewModelProjectionMapper<TState>
{
	/// <summary>
	/// Maps a state object to a view model.
	/// </summary>
	/// <typeparam name="TViewModel">The type of the view model to map to.</typeparam>
	/// <param name="state">The state object to map from.</param>
	/// <returns>The view model mapped from the state object.</returns>
	public TViewModel Map<TViewModel>(TState state);
}
